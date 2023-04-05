using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Controllers;

public class CookiesController : Controller
{
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
				Response.Cookies.Delete("_ga");
				Response.Cookies.Delete("_gid");
				var gaCookie = Request.Cookies.Keys.FirstOrDefault(key => key.StartsWith("_gat"));
				if (!string.IsNullOrEmpty(gaCookie))
				{
					Response.Cookies.Delete(gaCookie);
				}
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
			new CookieOptions { Expires = DateTimeOffset.Now + TimeSpan.FromDays(365), Path = "/", Secure = true, SameSite = SameSiteMode.Lax, IsEssential = true }
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
