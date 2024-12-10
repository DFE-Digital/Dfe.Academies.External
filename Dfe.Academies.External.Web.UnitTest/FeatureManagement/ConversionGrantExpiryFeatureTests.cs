using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.Academies.External.Web.FeatureManagement;
using Dfe.Academies.External.Web.UnitTest.MockSetUp;
using Microsoft.FeatureManagement;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.FeatureManagement
{
	[TestFixture]
	public class ConversionGrantExpiryFeatureTests
	{
		private Mock<IFeatureManager> _mockFeatureManager = null!;
		private static readonly Fixture Fixture = new();
		[SetUp]
		public void SetUp()
		{
			_mockFeatureManager = new Mock<IFeatureManager>();
		}

		[TestCase(false, "3024-11-10T09:13:00Z")]
		[TestCase(false, "")]
		public async Task IsEnabledAsync_NotExpired_ReturnsFalseAsync(bool isConversionGrantExpired,string conversionGrantExpiry)
		{
			//Arrange
			var inMemorySettings = new Dictionary<string, string>
			{
				{ "FeatureManagement:IsConversionGrantExpired", isConversionGrantExpired.ToString() },
				{ "FeatureManagement:ConversionGrantExpiry", conversionGrantExpiry }
			};
			_mockFeatureManager.Setup(x => x.IsEnabledAsync("IsConversionGrantExpired")).ReturnsAsync(isConversionGrantExpired);

			var feature = new ConversionGrantExpiryFeature(ConfigurationMock.GetMockedConfiguration(inMemorySettings), _mockFeatureManager.Object);

			//Action
			var result = await feature.IsEnabledAsync();

			//Assert
			Assert.That(result, Is.False);
		}

		[TestCase(true, "3024-11-10T09:13:00Z")]
		[TestCase(false, "2024-11-10T09:13:00Z")]
		public async Task IsEnabledAsync_Expired_ReturnsTrueAsync(bool isConversionGrantExpired, string conversionGrantExpiry)
		{
			//Arrange
			var inMemorySettings = new Dictionary<string, string>
			{
				{ "FeatureManagement:IsConversionGrantExpired", isConversionGrantExpired.ToString() },
				{ "FeatureManagement:ConversionGrantExpiry", conversionGrantExpiry }
			};
			_mockFeatureManager.Setup(x => x.IsEnabledAsync("IsConversionGrantExpired")).ReturnsAsync(isConversionGrantExpired);

			var feature = new ConversionGrantExpiryFeature(ConfigurationMock.GetMockedConfiguration(inMemorySettings), _mockFeatureManager.Object);

			//Action
			var result = await feature.IsEnabledAsync();

			//Assert
			Assert.That(result, Is.True);
		}

		[TestCase("2024-11-13T09:13:00Z", "2024-11-10T09:13:00Z", true)]
		[TestCase("2024-11-09T09:13:00Z", "2024-11-10T09:13:00Z", false)]
		[TestCase("2024-11-09T09:13:00Z", "", false)]
		public void IsNewApplication_Expired_Returns_CorrectResponse(string applicationCreatedOn, string conversionGrantExpiry, bool expectedResult)
		{
			//Arrange
			var inMemorySettings = new Dictionary<string, string>
			{
				{ "FeatureManagement:ConversionGrantExpiry", conversionGrantExpiry }
			};

			var feature = new ConversionGrantExpiryFeature(ConfigurationMock.GetMockedConfiguration(inMemorySettings), _mockFeatureManager.Object);
			DateTime.TryParse(applicationCreatedOn, out DateTime applicationCreatedDateTime);

			//Action
			var result = feature.IsNewApplication(applicationCreatedDateTime);

			//Assert
			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}
