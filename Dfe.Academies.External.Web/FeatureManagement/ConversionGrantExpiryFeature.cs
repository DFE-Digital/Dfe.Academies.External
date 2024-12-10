using System.Globalization;
using Microsoft.FeatureManagement;

namespace Dfe.Academies.External.Web.FeatureManagement
{
	public interface IConversionGrantExpiryFeature
	{
		public bool IsNewApplication(DateTime? applicationCreatedOn);
		public Task<bool> IsEnabledAsync();
	}
	public class ConversionGrantExpiryFeature(IConfiguration configuration, IFeatureManager featureManager) : IConversionGrantExpiryFeature
	{
		private const string _forceToEnableFeatureName = "IsConversionGrantExpired";
		public async Task<bool> IsEnabledAsync()
		{
			if(await featureManager.IsEnabledAsync(_forceToEnableFeatureName))
			{
				return true;
			}

			if (DateTime.TryParse(configuration["FeatureManagement:ConversionGrantExpiry"], new CultureInfo("en-GB"), out DateTime activationDate))
			{
				return DateTime.UtcNow >= activationDate;
			}
			return false;
		}

		public bool IsNewApplication(DateTime? applicationCreatedOn)
		{
			if (DateTime.TryParse(configuration["FeatureManagement:ConversionGrantExpiry"], new CultureInfo("en-GB"), out DateTime activationDate))
			{
				return applicationCreatedOn >= activationDate;
			}
			return false;
		}
	}
}
