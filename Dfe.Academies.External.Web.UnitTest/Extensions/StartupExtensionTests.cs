using System;
using System.Collections.Generic;
using Dfe.Academies.External.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Extensions;

	[Parallelizable(ParallelScope.All)]
	public class StartupExtensionTests
	{
        [Test]
		public void WhenAddTramsApi_MissingConfiguration_ThrowException()
		{
			// arrange
			var serviceCollection = new ServiceCollection();
			var configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>()).Build();
			
			// act
			Assert.Throws<Exception>(() => serviceCollection.AddAcademiesApi(configuration));
		}

		[Test]
		public void WhenAddTramsApi_Configuration_Success()
		{
			// arrange
			var serviceCollection = new ServiceCollection();
			
			// act
			serviceCollection.AddAcademiesApi(new ConfigurationBuilder().AddInMemoryCollection().Build());
			
			// assert
			Assert.That(serviceCollection, Is.Not.Null);
		}
	}
