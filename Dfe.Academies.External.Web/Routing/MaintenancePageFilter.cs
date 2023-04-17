using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Dfe.Academies.External.Web.Routing
{
	public class MaintenancePageFilter : IAsyncPageFilter
	{
		private readonly IConfiguration _config;

		public MaintenancePageFilter(IConfiguration config)
		{
			_config = config;
		}

		public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
		{
			return Task.CompletedTask;
		}

		public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
													  PageHandlerExecutionDelegate next)
		{
			bool maintenanceMode = bool.Parse(_config["MaintenanceMode"]);

			if (maintenanceMode && !context.ActionDescriptor.DisplayName.Contains("Maintenance")) {
				context.Result = new RedirectToPageResult("Terms");
				return;
			}

			// Do post work.
			await next.Invoke();
		}
	}
}
