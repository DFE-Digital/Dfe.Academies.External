using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Dfe.Academies.External.Web.UnitTest.MockSetUp
{
	public class ConfigurationMock
	{
		public static IConfiguration GetMockedConfiguration(Dictionary<string, string> settings)
		{
			var configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(settings!)
				.Build();
			return configuration;
		}
	}
}
