using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using DfE.CoreLibs.Testing.Authorization;
using DfE.CoreLibs.Testing.Mocks.WebApplicationFactory;
using Microsoft.Extensions.DependencyInjection;
using DfE.CoreLibs.Testing.Authorization.Helpers;
using NUnit.Framework;
using Microsoft.IdentityModel.Tokens;

namespace Dfe.Academies.External.Web.UnitTest.Pages
{
	public class PageSecurityTests
	{
		private readonly AuthorizationTester _validator;
		private static readonly Lazy<IEnumerable<RouteEndpoint>> _endpoints = new(InitializeEndpoints);
		private const bool _globalAuthorizationEnabled = true;

		public PageSecurityTests()
		{
			_validator = new AuthorizationTester(_globalAuthorizationEnabled);
		}

		[Theory]
		[TestCaseSource(nameof(GetPageSecurityTestData))]
		public void ValidatePageSecurity(string route, string expectedSecurity)
		{
			var result = _validator.ValidatePageSecurity(route, expectedSecurity, _endpoints.Value);
			result.Message.IsNullOrEmpty();
		}

		public static IEnumerable<object[]> GetPageSecurityTestData()
		{
			var configFilePath = "ExpectedSecurityConfig.json";
			var pages = EndpointTestDataProvider.GetPageSecurityTestDataFromFile(configFilePath, _endpoints.Value, _globalAuthorizationEnabled);
			return pages;
		}

		private static IEnumerable<RouteEndpoint> InitializeEndpoints()
		{
			// Using a temporary factory to access the EndpointDataSource for lazy initialization
			var factory = new CustomWebApplicationFactory<Startup>();
			var endpointDataSource = factory.Services.GetRequiredService<EndpointDataSource>();

			var endpoints = endpointDataSource.Endpoints
			   .OfType<RouteEndpoint>()
			   .Where(x => !x.RoutePattern.RawText!.Equals("/") &&
						   !x.DisplayName!.Contains("Dfe.Academies.External.Web"));

			return endpoints;
		}
	}
}
