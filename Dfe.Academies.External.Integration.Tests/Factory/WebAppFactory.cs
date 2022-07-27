//// https://khalidabuhakmeh.com/testing-aspnet-core-6-apps

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Dfe.Academies.External.Integration.Tests.Factory;

//// internal is important as it's the same access level as `Program`
internal class WebAppFactory : WebApplicationFactory<Program>
{
	private IConfigurationRoot Configuration { get; }

	public WebAppFactory(IConfigurationRoot configuration)
	{
		Configuration = configuration;
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.ConfigureServices(s =>
		{
			// MR:- can do something like below to spoof a response, same as below
			//s.AddScoped<IMessageService, TestMessageService>(
			//	_ => new TestMessageService
			//	{
			//		Message = Message
			//	});
		});

		return base.CreateHost(builder);
	}

	// MR:- below from concerns casework
	//protected override void ConfigureWebHost(IWebHostBuilder builder)
	//{
	//	builder.UseContentRoot(".");
	//	base.ConfigureWebHost(builder);
	//	builder.UseConfiguration(Configuration);
	//}
}