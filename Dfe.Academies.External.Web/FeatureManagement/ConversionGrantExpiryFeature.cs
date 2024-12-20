using System.Globalization;
using Microsoft.FeatureManagement;

namespace Dfe.Academies.External.Web.FeatureManagement
{
	public interface IConversionGrantExpiryFeature
	{
		public bool IsNewApplication(DateTime? applicationCreatedOn);
		public Task<bool> IsEnabledAsync();
	}
	public class ConversionGrantExpiryFeature : IConversionGrantExpiryFeature
	{
		public async Task<bool> IsEnabledAsync()
		{
			return DateTime.UtcNow >= GetActivateDate();
		}

		public bool IsNewApplication(DateTime? applicationCreatedOn)
		{
			return applicationCreatedOn >= GetActivateDate();
		}

		private static DateTime GetActivateDate()
		{
			return DateTime.Parse("2024-12-21T00:00:00Z");
		}
	}
}
