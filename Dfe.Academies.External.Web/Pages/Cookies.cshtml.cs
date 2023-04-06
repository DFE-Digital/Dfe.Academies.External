using System.Net;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
	public class CookiesModel : PageModel
	{
		private readonly ILogger<CookiesModel> logger;

		public CookiesModel(ILogger<CookiesModel> logger)
		{
			this.logger = logger;
		}
		public void OnGet(string returnUrl)
		{ 
			if(!string.IsNullOrWhiteSpace(returnUrl))
				TempData["returnUrl"] = returnUrl;
			
		}

		public async Task<IActionResult> OnPostAsync(CookieConsent cookies, string redirectPath, string returnPath)
		{
			var uri = Request.GetUri();

			this.logger.LogInformation($"Host: {uri.Host} | Uri.ToString() : {uri.ToString} | Absolute Uri : {uri.AbsoluteUri} | Authority: {uri.Authority}");
			switch (cookies)
			{
				case CookieConsent.Accept:
					HttpContext.Session.Remove("cookiesRejected");
					SetConsentCookie("yes");
					break;
				case CookieConsent.Reject:
					HttpContext.Session.SetInt32("cookiesRejected", 1);
					SetConsentCookie("no");
					Response.Cookies.Delete("_ga");
					Response.Cookies.Delete("_gid");
					var gatCookie = Request.Cookies.Keys.FirstOrDefault(key => key.StartsWith("_gat_"));
					if (!string.IsNullOrEmpty(gatCookie))
						Response.Cookies.Delete(gatCookie);

					var gaCookie = Request.Cookies.FirstOrDefault(cookie => cookie.Key.StartsWith("_ga_"));
					if (gaCookie.Key != null)
						Response.Cookies.Delete(gaCookie.Key, new CookieOptions { Domain = Request.GetUri().Host, Path = "/" });
					break;
					// No default because if we get a value out of range then we can just ignore it
			}

			TempData["cookiePreferenceSaved"] = true;
			TempData["returnPath"] = returnPath;
			return Redirect(redirectPath);
		}

		private void SetConsentCookie(string value) =>
			Response.Cookies.Append(".AspNet.Consent", value,
				new CookieOptions { Expires = DateTime.Now + TimeSpan.FromDays(365), Path = "/", Secure = true, SameSite = SameSiteMode.Lax, IsEssential = true });
	}

	public enum CookieConsent
	{
		Unknown,
		Accept,
		Reject
	}
}
