using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using StackExchange.Redis;
using Polly;
using Polly.Extensions.Http;
using Dfe.Academies.External.Web.Services;
using GovUk.Frontend.AspNetCore;
using NetEscapades.AspNetCore.SecurityHeaders;
using Dfe.Academies.External.Web.Routing;
using Dfe.Academies.External.Web.Security;
using Dfe.Academisation.CorrelationIdMiddleware;
using Microsoft.AspNetCore.HttpOverrides;
using Quartz;
using Dfe.Academies.External.Web.AutoMapper;
using Dfe.Academies.External.Web.Factories;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models.EmailTemplates;
using Microsoft.Extensions.Options;
using Notify.Client;
using Notify.Interfaces;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.DataProtection;
using Dfe.Academies.External.Web.Extensions;

namespace Dfe.Academies.External.Web
{ 
	public class Startup(IConfiguration configuration)
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddGovUkFrontend(options =>
			{
				options.GetCspNonceForRequest = context => context.GetNonce();
			});

			services
				.AddRazorPages(options =>
				{
					options.Conventions
						.AuthorizeFolder("/", "AcademiesExternalPolicy")
						.AllowAnonymousToPage("/Index")
						.AllowAnonymousToPage("/Accessibility-Statement")
						.AllowAnonymousToPage("/Cookies")
						.AllowAnonymousToPage("/Terms")
						.AllowAnonymousToPage("/Privacy")
						.AllowAnonymousToPage("/Error")
						.AllowAnonymousToPage("/NotFound")
						.AllowAnonymousToPage("/WhatYouWillNeed")
						.AllowAnonymousToPage("/Maintenance");
					options.Conventions.AddPageRoute("/notfound", "/error/404");
					options.Conventions.AddPageRoute("/notfound", "/error/{code:int}");
				})
				.AddViewOptions(options =>
				{
					options.HtmlHelperOptions.ClientValidationEnabled = true;
				})
				.AddRazorPagesOptions(options =>
				{
					options.Conventions.Add(new PageRouteTransformerConvention(new HyphenateRouteParameterTransformer()));
				})
				.AddMvcOptions(options =>
				{
					options.MaxModelValidationErrors = 50;
					options.Filters.Add(new MaintenancePageFilter(configuration));
				})
				.AddSessionStateTempDataProvider();

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
			})
				.AddCookie()
				.AddOpenIdConnect(options =>
				{
					options.ClientId = configuration["SignIn:OneloginOpenIdConnectClientId"];
					options.ClientSecret = configuration["SignIn:OneloginOpenIdConnectClientSecret"];
					options.RequireHttpsMetadata = true;
					options.ResponseType = "code";

					options.Authority = configuration["SignIn:OneLoginUrl"];
					options.GetClaimsFromUserInfoEndpoint = true;
					options.TokenValidationParameters.NameClaimType = "email";
					options.SaveTokens = true;
					options.Scope.Add("openid");
					options.Scope.Add("email");
					options.Scope.Add("given");
					options.Scope.Add("surname");
					options.Scope.Add("organisation");

					options.UseTokenLifetime = true;
					options.SaveTokens = true;
					options.GetClaimsFromUserInfoEndpoint = true;

					options.Events.OnRedirectToIdentityProvider = context =>
					{
						// check for a redirect uri override
						string? redirectUri = configuration["SignIn:RedirectUri"];
						if (!string.IsNullOrEmpty(redirectUri))
						{
							context.ProtocolMessage.RedirectUri = redirectUri;
						}

						context.ProtocolMessage.Prompt = "login";
						return Task.CompletedTask;
					};
				}
				);

			services.AddAuthorization(options =>
			{
				options.AddPolicy("AcademiesExternalPolicy", policy =>
				{
					policy.RequireAuthenticatedUser();
				});
			});

			// Academies API
			services.AddAcademiesApi(configuration);
			services.AddAcademisationApi(configuration);

			// Internal Service
			services.AddInternalServices();

			services.AddDistributedMemoryCache();
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
				options.Secure = CookieSecurePolicy.Always;
			});

			// Configure Redis Based Distributed Session
			var redisConfigurationOptions = ConfigurationOptions.Parse(configuration["ConnectionStrings:RedisCache"]!);
			redisConfigurationOptions.AsyncTimeout = 15000;
			redisConfigurationOptions.SyncTimeout = 15000;


			//cofig from concerns
			//var redisConfigurationOptions = new ConfigurationOptions { Password = password, EndPoints = { $"{host}:{port}" }, Ssl = tls, AsyncTimeout = 15000, SyncTimeout = 15000 };

			// https://stackexchange.github.io/StackExchange.Redis/ThreadTheft.html
			ConnectionMultiplexer.SetFeatureFlag("preventthreadtheft", true);


			IConnectionMultiplexer redisConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConfigurationOptions);
			//services.AddDataProtection().PersistKeysToStackExchangeRedis(redisConnectionMultiplexer, "DataProtectionKeys");

			//services.AddStackExchangeRedisCache(
			//	options =>
			//	{
			//		options.ConfigurationOptions = redisConfigurationOptions;
			//		options.InstanceName = $"Redis-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
			//		options.ConnectionMultiplexerFactory = () => Task.FromResult(_redisConnectionMultiplexer);
			//	})

			services.AddStackExchangeRedisCache(redisCacheConfig =>
			{
				redisCacheConfig.ConfigurationOptions = redisConfigurationOptions;
				redisCacheConfig.ConnectionMultiplexerFactory = () => Task.FromResult(redisConnectionMultiplexer);
				redisCacheConfig.InstanceName = "redis-master";
			});

			services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.SameSite = SameSiteMode.Strict;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.IsEssential = true;
			}
			);
			services.AddSingleton<IAadAuthorisationHelper, AadAuthorisationHelper>();

			services.AddAutoMapper(typeof(AutoMapperProfile));
			services.AddTransient<IAsyncNotificationClient, NotificationClient>(x => new NotificationClient(configuration["emailnotifications:key"]));
			services.Configure<NotifyTemplateSettings>(configuration.GetSection("govuk-notify"));
			services.AddSingleton<IContributorTemplate, FormAMatChairContributor>(x => new FormAMatChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			services.AddSingleton<IContributorTemplate, FormAMatNonChairContributor>(x => new FormAMatNonChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			services.AddSingleton<IContributorTemplate, JoinAMatChairContributor>(x => new JoinAMatChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			services.AddSingleton<IContributorTemplate, JoinAMatNonChairContributor>(x => new JoinAMatNonChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			services.AddSingleton<IContributorNotifyTemplateFactory, ContributorNotifyTemplateFactory>();
			services.AddSingleton<IEmailNotificationService, EmailNotificationService>();

			services.AddScoped<ICorrelationContext, CorrelationContext>();

			services.AddHttpClient<IFileUploadService, FileUploadService>(client =>
			{
				client.BaseAddress = new Uri(configuration["Sharepoint:ApiUrl"]!);
			})
				.AddPolicyHandler(GetRetryPolicy());

			static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
			{
				return HttpPolicyExtensions
					.HandleTransientHttpError()
					.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
					.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
						retryAttempt)));
			}

			// culture - https://dotnetcoretutorials.com/2017/06/22/request-culture-asp-net-core/
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo>
				{
	new ("en-GB")
				};
				options.DefaultRequestCulture = new RequestCulture("en-GB");
				// By default the below will be set to whatever the server culture is.
				options.SupportedCultures = supportedCultures;
				// Supported cultures is a list of cultures that your web app will be able to run under. By default this is set to a the culture of the machine.
				options.SupportedUICultures = supportedCultures;
			});


			services.AddApplicationInsightsTelemetry(configuration);
			//var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();

			//// Disables adaptive sampling.
			//aiOptions.EnableAdaptiveSampling = false;

			//// Disables QuickPulse (Live Metrics stream).
			//aiOptions.EnableQuickPulseMetricStream = false;
			//aiOptions.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
			//services.AddApplicationInsightsTelemetry(aiOptions);

			var localDevelopment = configuration.GetValue<bool>("local_development");
			if (!localDevelopment)
			{
				// Setup basic Data Protection and persist keys.xml to local file system
				var dp = services.AddDataProtection()
					.PersistKeysToFileSystem(new DirectoryInfo(@"/srv/app/storage"));

				// If a Key Vault Key URI is defined, expect to encrypt the keys.xml
				string? kvProtectionKeyUri = configuration.GetValue<string>("DataProtection:KeyVaultKey");
				if (!string.IsNullOrEmpty(kvProtectionKeyUri))
				{
					var credentials = new DefaultAzureCredential();
					dp.ProtectKeysWithAzureKeyVault(
						new Uri(kvProtectionKeyUri),
						credentials
					);
				}
			}

			services.AddQuartz(q => { q.UseMicrosoftDependencyInjectionJobFactory(); });
			services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });

			// Enforce HTTPS in ASP.NET Core
			// @link https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?
			services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});
		}

		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
		{
			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Configure the HTTP request pipeline.

			// Ensure we do not lose X-Forwarded-* Headers when behind a Proxy
			var forwardOptions = new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.All,
				RequireHeaderSymmetry = false
			};
			forwardOptions.KnownNetworks.Clear();
			forwardOptions.KnownProxies.Clear();
			app.UseForwardedHeaders(forwardOptions);

			if (!env.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
			}
			else
			{
				app.UseDeveloperExceptionPage();
			}

			// trying this to see if it resolves cookie problem
			app.UseCookiePolicy();

			// Combined with razor routing 404 display custom page NotFound
			app.UseStatusCodePagesWithReExecute("/error/{0}");

			//
			//app.UseBespokeExceptionHandling(app.Environment);
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			// Enable automatic tracing integration.
			// If running with .NET 5 or below, make sure to put this middleware
			// right after `UseRouting()`.

			//app.UseSerilogRequestLogging();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSession();
			app.UseCookiePolicy();

			// add OWASP top 10 response headers
			app.UseSecurityHeaders(SecureHeadersDefinitions.GetHeaderPolicyCollection());
			app.UseHsts();

			// Add Content-Language response header
			app.UseRequestLocalization(new RequestLocalizationOptions
			{
				ApplyCurrentCultureToResponseHeaders = true
			});

			app.UseMiddleware<CorrelationIdMiddleware>();

			// possible redis fix
			ThreadPool.SetMinThreads(400, 400);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});
		}
	}

}
