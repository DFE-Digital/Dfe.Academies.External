using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models.EmailTemplates
{
	public interface IContributorTemplate : IEmailTemplate
	{
		public SchoolRoles SchoolRole { get; }
		public ApplicationTypes ApplicationType { get; }
	}
}
