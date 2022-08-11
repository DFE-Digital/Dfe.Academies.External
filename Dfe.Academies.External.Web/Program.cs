using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Routing;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
			.AllowAnonymousToPage("/WhatYouWillNeed")
			// TODO :- below is temporary config UNTIL auth is sorted - just for demo reasons !!
			.AllowAnonymousToPage("/WhatAreYouApplyingToDo")
			.AllowAnonymousToPage("/YourApplications")
			.AllowAnonymousToPage("/ApplicationOverview")
			.AllowAnonymousToPage("/WhatIsYourRole")
			.AllowAnonymousToPage("/school/SchoolOverview")
			.AllowAnonymousToPage("/school/ApplicationSelectSchool")
			.AllowAnonymousToPage("/school/PupilNumbers")
			.AllowAnonymousToPage("/school/ApplicationJoinTrustReasons")
			.AllowAnonymousToPage("/school/ApplicationChangeSchoolName")
			.AllowAnonymousToPage("/school/ApplicationConversionTargetDate")
			.AllowAnonymousToPage("/school/SchoolConversionKeyDetails")
			.AllowAnonymousToPage("/trust/ApplicationSelectTrust");
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
	});

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

// Internal Service
builder.Services.AddInternalServices();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
	{
		options.Cookie.HttpOnly = true;
		options.Cookie.SameSite = SameSiteMode.Strict;
		options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
		options.Cookie.IsEssential = true;
	}
);

//
//webBuilder.UseSentry(o =>
//{
//	o.Dsn = "https://4d1ecd28676d4b06b3784f427733c754@o1042804.ingest.sentry.io/6047969";
//	// When configuring for the first time, to see what the SDK is doing:
//	o.Debug = true;
//	// Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
//	// We recommend adjusting this value in production.
//	o.TracesSampleRate = 1.0;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

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

app.Run();
