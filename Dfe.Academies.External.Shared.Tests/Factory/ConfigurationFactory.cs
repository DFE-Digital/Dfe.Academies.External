using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Dfe.Academies.External.Shared.Tests.Factory
{
	public static class ConfigurationFactory
	{
		public static IConfigurationBuilder ConfigurationInMemoryBuilder(this IConfigurationBuilder configurationBuilder, Dictionary<string, string> initialData)
		{
			return configurationBuilder.AddInMemoryCollection(initialData);
		}
		
		public static IConfigurationBuilder ConfigurationUserSecretsBuilder(this IConfigurationBuilder configurationBuilder)
		{
			// TODO MR:- check this re: dot net core 6 !!
			return configurationBuilder.AddUserSecrets<Program>();
		}
		
		public static IConfigurationBuilder ConfigurationJsonFile(this IConfigurationBuilder configurationBuilder)
		{
			return configurationBuilder.AddJsonFile("appsettings.json");
		}
	}
}