
using Serilog;
using Microsoft.ApplicationInsights.Extensibility;

namespace Dfe.Academies.External.Web;
public class Program
{
	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			})
			.UseSerilog((context, services, loggerConfiguration) =>
				loggerConfiguration.WriteTo.ApplicationInsights(
					services.GetRequiredService<TelemetryConfiguration>(),
					TelemetryConverter.Traces
				));
}
