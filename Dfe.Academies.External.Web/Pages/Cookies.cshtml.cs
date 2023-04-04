using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dfe.Academies.External.Web.Pages
{
	public class CookiesModel : PageModel
	{
		public void OnGet(string returnUrl)
		{ 
			if(!string.IsNullOrWhiteSpace(returnUrl))
				TempData["returnUrl"] = returnUrl;
			
		}

		public async Task<IActionResult> OnPostAsync(CookieConsent cookies, string redirectPath, string returnUrl)
		{
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
					var gaCookie = Request.Cookies.Keys.FirstOrDefault(key => key.StartsWith("_gat_"));
					if (!string.IsNullOrEmpty(gaCookie))
						Response.Cookies.Delete(gaCookie);
					break;
					// No default because if we get a value out of range then we can just ignore it
			}

			TempData["cookiePreferenceSaved"] = true;
			TempData["returnUrl"] = returnUrl;
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
