using Dfe.Academies.External.Web.Pages;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers;

public class CookiesController : Controller
{
	private readonly string cookieDomain;

	public CookiesController(IConfiguration configuration)
	{
		this.cookieDomain = configuration["Google:CookieDomain"];
	}

	[AllowAnonymous]
	[HttpPost]
	[Route(nameof(SetConsent))]
	public IActionResult SetConsent(CookiesConsent cookies, string redirectPath)
	{
		switch (cookies)
		{
			case CookiesConsent.Accept:
				HttpContext.Session.Remove("cookiesRejected");
				SetConsentCookie("yes");
				break;
			case CookiesConsent.Reject:
				HttpContext.Session.SetInt32("cookiesRejected", 1);
				SetConsentCookie("no");
				Response.Cookies.Delete("_ga", new CookieOptions { Domain = this.cookieDomain, Path = "/" });
				Response.Cookies.Delete("_gid", new CookieOptions { Domain = this.cookieDomain, Path = "/" });
				var gatCookie = Request.Cookies.Keys.FirstOrDefault(key => key.StartsWith("_gat_"));
				if (!string.IsNullOrEmpty(gatCookie))
					Response.Cookies.Delete(gatCookie, new CookieOptions { Domain = this.cookieDomain, Path = "/" });

				var gaCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key.StartsWith("_ga_"));
				if (gaCookie.Key != null)
					Response.Cookies.Delete(gaCookie.Key, new CookieOptions { Domain = this.cookieDomain, Path = "/" });
				break;
		}

		TempData["cookiePreferenceSaved"] = true;
		TempData["redirectPath"] = redirectPath;
		return LocalRedirect(redirectPath);
	}

	private void SetConsentCookie(string value)
	{
		Response.Cookies.Append(
			".AspNet.Consent",
			value,
			new CookieOptions { Expires = DateTimeOffset.Now + TimeSpan.FromDays(365), Path = "/", Secure = true, HttpOnly = true, SameSite = SameSiteMode.Lax, IsEssential = true }
			);
	}

	[AllowAnonymous]
	[HttpPost]
	[Route(nameof(HideCookieMessage))]
	public IActionResult HideCookieMessage(string redirectPath)
	{
		TempData["cookiePreferenceSaved"] = false;
		return LocalRedirect(redirectPath);
	}

	[AllowAnonymous]
	[HttpGet]
	public string KeepAlive()
	{
		return "ok";
	}

	[AllowAnonymous]
	[HttpGet]
	public IActionResult LogOut()
	{
		try
		{
			foreach (var cookie in Request.Cookies.Keys)
				Response.Cookies.Delete(cookie);

			return View("LogOut");
		}
		catch (Exception ex)
		{
			//return CatchErrorAndRedirect(ex);
			return LocalRedirect("/");
		}
	}
}


public enum CookiesConsent
{
	Unknown,
	Accept,
	Reject
}
