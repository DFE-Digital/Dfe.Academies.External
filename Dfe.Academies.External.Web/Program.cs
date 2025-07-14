using System.Globalization;
using Azure.Identity;
using Dfe.Academies.External.Web.AutoMapper;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Factories;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models.EmailTemplates;
using Dfe.Academies.External.Web.Routing;
using Dfe.Academies.External.Web.Security;
using Dfe.Academies.External.Web.Services;
using Dfe.Academisation.CorrelationIdMiddleware;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using NetEscapades.AspNetCore.SecurityHeaders;
using Notify.Client;
using Notify.Interfaces;
using Polly;
using Polly.Extensions.Http;
using Quartz;
using Serilog;
using StackExchange.Redis;
using Microsoft.FeatureManagement;
using Dfe.Academies.External.Web.FeatureManagement;

namespace Dfe.Academies.External.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			ConfigurationManager configuration = builder.Configuration;

			//https://github.com/gunndabad/govuk-frontend-aspnetcore
			builder.Services.AddGovUkFrontend(options =>
			{
				options.GetCspNonceForRequest = context => context.GetNonce();
			});

			builder.Services
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

			builder.Services.AddFeatureManagement();

			builder.Services.AddAuthentication(options =>
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

			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("AcademiesExternalPolicy", policy =>
				{
					policy.RequireAuthenticatedUser();
				});
			});

			// Academies API
			builder.Services.AddAcademiesApi(configuration);
			builder.Services.AddAcademisationApi(configuration);

			// Internal Service
			builder.Services.AddInternalServices();

			builder.Services.AddDistributedMemoryCache();
			builder.Services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
				options.Secure = CookieSecurePolicy.Always;
			});

			// Configure Redis Based Distributed Session
			var redisConfigurationOptions = ConfigurationOptions.Parse(builder.Configuration["ConnectionStrings:RedisCache"]);
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

			builder.Services.AddStackExchangeRedisCache(redisCacheConfig =>
			{
				redisCacheConfig.ConfigurationOptions = redisConfigurationOptions;
				redisCacheConfig.ConnectionMultiplexerFactory = () => Task.FromResult(redisConnectionMultiplexer);
				redisCacheConfig.InstanceName = "redis-master";
			});

			builder.Services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.SameSite = SameSiteMode.Strict;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
				options.Cookie.IsEssential = true;
			}
			);
			builder.Services.AddSingleton<IAadAuthorisationHelper, AadAuthorisationHelper>();
			builder.Services.AddSingleton<IConversionGrantExpiryFeature, ConversionGrantExpiryFeature>();
			builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
			builder.Services.AddTransient<IAsyncNotificationClient, NotificationClient>(x => new NotificationClient(builder.Configuration["emailnotifications:key"]));
			builder.Services.Configure<NotifyTemplateSettings>(builder.Configuration.GetSection("govuk-notify"));
			builder.Services.AddSingleton<IContributorTemplate, FormAMatChairContributor>(x => new FormAMatChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			builder.Services.AddSingleton<IContributorTemplate, FormAMatNonChairContributor>(x => new FormAMatNonChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			builder.Services.AddSingleton<IContributorTemplate, JoinAMatChairContributor>(x => new JoinAMatChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			builder.Services.AddSingleton<IContributorTemplate, JoinAMatNonChairContributor>(x => new JoinAMatNonChairContributor(x.GetRequiredService<IOptions<NotifyTemplateSettings>>()));
			builder.Services.AddSingleton<IContributorNotifyTemplateFactory, ContributorNotifyTemplateFactory>();
			builder.Services.AddSingleton<IEmailNotificationService, EmailNotificationService>();

			builder.Services.AddScoped<ICorrelationContext, CorrelationContext>();

			builder.Services.AddHttpClient<IFileUploadService, FileUploadService>(client =>
			{
				client.BaseAddress = new Uri(configuration["Sharepoint:ApiUrl"]);
				client.DefaultRequestHeaders.Add("User-Agent", "ApplyToBecome/1.0");
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
			builder.Services.Configure<RequestLocalizationOptions>(options =>
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

			builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
			//var aiOptions = new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions();

			//// Disables adaptive sampling.
			//aiOptions.EnableAdaptiveSampling = false;

			//// Disables QuickPulse (Live Metrics stream).
			//aiOptions.EnableQuickPulseMetricStream = false;
			//aiOptions.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
			//builder.Services.AddApplicationInsightsTelemetry(aiOptions);

			builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
							.WriteTo.ApplicationInsights(
						services.GetRequiredService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>(),
						TelemetryConverter.Traces));

			var localDevelopment = builder.Configuration.GetValue<bool>("local_development");
			if (!localDevelopment)
			{
				// Setup basic Data Protection and persist keys.xml to local file system
				var dp = builder.Services.AddDataProtection()
					.PersistKeysToFileSystem(new DirectoryInfo(@"/srv/app/storage"));

				// If a Key Vault Key URI is defined, expect to encrypt the keys.xml
				string? kvProtectionKeyUri = builder.Configuration.GetValue<string>("DataProtection:KeyVaultKey");
				if (!string.IsNullOrEmpty(kvProtectionKeyUri))
				{
					var credentials = new DefaultAzureCredential();
					dp.ProtectKeysWithAzureKeyVault(
						new Uri(kvProtectionKeyUri),
						credentials
					);
				}
			}

			builder.Services.AddQuartz(q => { q.UseMicrosoftDependencyInjectionJobFactory(); });
			builder.Services.AddQuartzHostedService(opt => { opt.WaitForJobsToComplete = true; });

			// Enforce HTTPS in ASP.NET Core
			// @link https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?
			builder.Services.AddHsts(options =>
			{
				options.Preload = true;
				options.IncludeSubDomains = true;
				options.MaxAge = TimeSpan.FromDays(365);
			});

			builder.Services.AddHealthChecks();

			var app = builder.Build();

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

			if (!app.Environment.IsDevelopment())
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

			app.MapRazorPages();

			// MR:- need below because search methods are in a controller !
			app.MapControllers();

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

			// Just for thesame of the PR
			var message = "Test message";
			Console.WriteLine(message);

			app.UseHealthChecks("/health");

			app.Run();
		}
	}
}
