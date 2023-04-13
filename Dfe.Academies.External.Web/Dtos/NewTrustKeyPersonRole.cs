using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Dtos
{
	public class NewTrustKeyPersonRole
	{
		public NewTrustKeyPersonRole(KeyPersonRole role, string timeInRole)
		{
			Role = role;
			TimeInRole = timeInRole;
		}

		public int Id { get; set; }

		public KeyPersonRole Role { get; set; }

		/// <summary>
		/// Taken from A2C-SIP - ApplicationKeyPersons object
		/// </summary>
		public string TimeInRole { get; set; }

	}
}
