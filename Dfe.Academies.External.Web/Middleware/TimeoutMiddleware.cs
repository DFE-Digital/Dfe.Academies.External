namespace Dfe.Academies.External.Web.Middleware
{
	public class TimeoutMiddleware
	{
		private readonly RequestDelegate _next;

		public TimeoutMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			//app.Use(async (context, next) =>
			//{
			if (context.User.Identity != null && string.IsNullOrEmpty(context.Session.GetString("a2b_username")) && context.User.Identity.IsAuthenticated)
			{
				context.Response.Redirect("../index");
			}
			else
			{
				await _next(context);
			}
			//});
		}
	}
}
