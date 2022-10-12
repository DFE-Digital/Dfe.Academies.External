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
			context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
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

		// MR:- below breaks JS imports currently - 11/10/2022
		//if (!existingHeaders.ContainsKey("content-security-policy"))
		//{
		//	context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
		//}
		
		//
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
