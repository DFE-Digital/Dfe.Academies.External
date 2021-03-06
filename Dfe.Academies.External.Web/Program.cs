using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Routing;
using GovUk.Frontend.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

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
			.AllowAnonymousToPage("/WhatYouWillNeed")
			// TODO :- below is temporary config UNTIL auth is sorted - just for demo reasons !!
			.AllowAnonymousToPage("/WhatAreYouApplyingToDo")
			.AllowAnonymousToPage("/YourApplications")
			.AllowAnonymousToPage("/ApplicationOverview")
			.AllowAnonymousToPage("/WhatIsYourRole")
			.AllowAnonymousToPage("/SchoolOverview")
			;
	})
	.AddRazorPagesOptions(options =>
	{
		options.Conventions.Add(new PageRouteTransformerConvention(new HyphenateRouteParameterTransformer()));
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.Run();

