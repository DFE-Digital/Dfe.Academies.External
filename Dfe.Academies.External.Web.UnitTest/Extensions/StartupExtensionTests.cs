using Dfe.Academies.External.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Dfe.Academies.External.Web.UnitTest.Extensions;

[Parallelizable(ParallelScope.All)]
internal sealed class StartupExtensionTests
{
    [Test]
    public void AddAcademiesApi___Configuration___InValid()
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
    public void AddAcademiesApi___Configuration___Valid()
    {
        // arrange
        var serviceCollection = new ServiceCollection();

        // act
        serviceCollection.AddAcademiesApi(new ConfigurationBuilder()
                   .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                   {
                           new("academies-api:endpoint", "1"),
                           new("academies-api:key", "2")
                   })
            .Build());

        // assert
        Assert.That(serviceCollection, Is.Not.Null);
    }
    [Test]
    public void AddAcademisationApi___Configuration___InValid()
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
    public void AddAcademisationApi___Configuration___Valid()
    {
	    // arrange
	    var serviceCollection = new ServiceCollection();

	    // act
	    serviceCollection.AddAcademisationApi(new ConfigurationBuilder()
		    .AddInMemoryCollection(new List<KeyValuePair<string, string>>
		    {
			    new("academisation-api:endpoint", "1"),
			    new("academisation-api:key", "2")
		    })
		    .Build());

	    // assert
	    Assert.That(serviceCollection, Is.Not.Null);
    }
}
