using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models
{
	public class NewTrustKeyPersonRole
	{
		public NewTrustKeyPersonRole(KeyPersonRole role, string timeInRole)
		{
			Role = role;
			TimeInRole = timeInRole;
		}
		public KeyPersonRole Role { get; set; }

		/// <summary>
		/// Taken from A2C-SIP - ApplicationKeyPersons object
		/// </summary>
		public string TimeInRole { get; set; }
	}
}
