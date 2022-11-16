using System;
using System.Collections.Generic;
using Dfe.Academies.External.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Extensions;

[Parallelizable(ParallelScope.All)]
internal sealed class StartupExtensionTests
{
	[Test]
	public void AddAcademiesApi___ConfigurationInvalid___ThrowsException()
	{
		// arrange
		var serviceCollection = new ServiceCollection();
		var configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>())
			.Build();

		// act
		Assert.Throws<Exception>(() => serviceCollection.AddAcademiesApi(configuration));
	}

	[Test]
	public void AddAcademiesApi___ConfigurationInvalid___ExceptionThrown()
	{
		// arrange
		var serviceCollection = new ServiceCollection();

		// act
		serviceCollection.AddAcademiesApi(new ConfigurationBuilder()
				   .AddInMemoryCollection(new List<KeyValuePair<string, string>>
				   {
						   new("academies_api:endpoint", "1"),
						   new("academies_api:key", "2")
				   })
			.Build());

		// assert
		Assert.That(serviceCollection, Is.Not.Null);
	}
	[Test]
	public void AddAcademisationApi___ConfigurationInvalid___ThrowsException()
	{
		// arrange
		var serviceCollection = new ServiceCollection();
		var configuration = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>())
			.Build();

		// act
		Assert.Throws<Exception>(() => serviceCollection.AddAcademisationApi(configuration));
	}

	[Test]
	public void AddAcademisationApi___ConfigurationInvalid___ExceptionThrown()
	{
		// arrange
		var serviceCollection = new ServiceCollection();

		// act
		serviceCollection.AddAcademisationApi(new ConfigurationBuilder()
			.AddInMemoryCollection(new List<KeyValuePair<string, string>>
			{
				new("academisation_api:endpoint", "1"),
				new("academisation_api:key", "2")
			})
			.Build());

		// assert
		Assert.That(serviceCollection, Is.Not.Null);
	}
}
