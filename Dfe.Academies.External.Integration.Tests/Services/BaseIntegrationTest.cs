using Dfe.Academies.External.Integration.Tests.Factory;
using Dfe.Academies.External.Shared.Tests.Factory;
using Microsoft.Extensions.Configuration;

namespace Dfe.Academies.External.Integration.Tests.Services;

internal abstract class BaseIntegrationTest
{
	public readonly WebAppFactory _factory;

	protected BaseIntegrationTest()
	{
		var configuration = new ConfigurationBuilder().ConfigurationUserSecretsBuilder().Build();
		_factory = new WebAppFactory(configuration);
	}
}