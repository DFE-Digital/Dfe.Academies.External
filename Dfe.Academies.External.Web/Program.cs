using System.Globalization;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Middleware;
using Dfe.Academies.External.Web.Routing;
using Dfe.Academies.External.Web.Services;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Polly;
using Polly.Extensions.Http;

//using Serilog;
//using Serilog.Events;
//using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add sentry to the container.
builder.WebHost.UseSentry();

//// builder.Services.UseSerilog();
//builder.Host.UseSerilog((ctx, lc) => lc
//	.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//	.Enrich.FromLogContext()
//	.WriteTo.Console(new RenderedCompactJsonFormatter())
//	.WriteTo.Sentry());

//https://github.com/gunndabad/govuk-frontend-aspnetcore  
builder.Services.AddGovUkFrontend();

builder.Services
	.AddRazorPages(options =>
	{
		options.Conventions
			.AuthorizeFolder("/", "AcademiesExternalPolicy")
			.AllowAnonymousToPage("/Index")
			.AllowAnonymousToPage("/Accessibility")
			.AllowAnonymousToPage("/Cookies")
			.AllowAnonymousToPage("/Terms")
			.AllowAnonymousToPage("/Privacy")
			.AllowAnonymousToPage("/Error")
			.AllowAnonymousToPage("/NotFound")
			.AllowAnonymousToPage("/WhatYouWillNeed");
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
	})
	.AddSessionStateTempDataProvider();

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
});
builder.Services.AddSession(options =>
	{
		options.Cookie.HttpOnly = true;
		options.Cookie.SameSite = SameSiteMode.Strict;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		options.Cookie.IsEssential = true;
	}
);
builder.Services.AddScoped<IAadAuthorisationHelper, AadAuthorisationHelper>();

builder.Services.AddHttpClient<IFileUploadService, FileUploadService>(client =>
	{
		client.BaseAddress = new Uri(configuration["Sharepoint:ApiUrl"]);
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

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
}

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
app.UseSentryTracing();

//app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// MR:- need below because search methods are in a controller !
app.MapControllers();

app.UseSession();
app.UseCookiePolicy();

// culture
app.UseRequestLocalization(new RequestLocalizationOptions
{
	ApplyCurrentCultureToResponseHeaders = true
});

// add OWASP top 10 response headers
app.UseResponseMiddleware();

app.Run();
