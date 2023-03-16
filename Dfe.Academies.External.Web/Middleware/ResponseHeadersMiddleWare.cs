using Polly;

namespace Dfe.Academies.External.Web.Middleware;

public class ResponseHeadersMiddleWare
{
	private readonly RequestDelegate _next;
	public ResponseHeadersMiddleWare(RequestDelegate next)
	{
		_next = next;
	}
	public Task Invoke(HttpContext context)
	{
		// add OWASP top 10 defense headers!!
		var existingHeaders = context.Response.Headers.ToDictionary(l => l.Key.ToLower(), k => k.Value);
		
		if (!existingHeaders.ContainsKey("x-frame-options"))
		{
			context.Response.Headers.Add("X-Frame-Options", "DENY");
		}

		if (!existingHeaders.ContainsKey("x-xss-protection"))
		{
			context.Response.Headers.Add("X-Xss-Protection", "0");
		}

		if (!existingHeaders.ContainsKey("x-content-type-options"))
		{
			context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
		}

		if (!existingHeaders.ContainsKey("referrer-policy"))
		{
			context.Response.Headers.Add("Referrer-Policy", "no-referrer");
		}

		if (!existingHeaders.ContainsKey("x-permitted-cross-domain-policies"))
		{
			context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
		}

		if (!existingHeaders.ContainsKey("content-security-policy"))
		{
			context.Response.Headers.Add("Content-Security-Policy", "default-src 'self' wss://localhost:44365/Dfe.Academies.External.Web/; script-src 'self' 'sha256-qL+CKdDo+s+wbAVlMRNaKTthlML5CHI7jaNN8xIHquM=' 'sha256-oJB7VN5D3FsVWp4IBkMG5wPNDs4/Yf73/2mCN7Va9ao=' 'sha256-mmu7ufJkx6yK/dAWH2qN/k0kRhIj7O1GP53WoweDgVw=' 'sha256-YXeAP6J7c5mHporqs1+yXBn3qwau95EZrnniBY+4bpQ=' 'sha256-l1eTVSK8DTnK8+yloud7wZUqFrI0atVo6VlC6PJvYaQ=' https://www.googletagmanager.com/gtm.js; style-src 'self'; font-src 'self'; img-src 'self'; frame-src 'self'");
		}

		if (!existingHeaders.ContainsKey("permissions-policy"))
		{
			context.Response.Headers.Add("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
		}

		return _next(context);
	}
}

public static class ResponseHeadersMiddleWareExtensions
{
	public static IApplicationBuilder UseResponseMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ResponseHeadersMiddleWare>();
	}
}
