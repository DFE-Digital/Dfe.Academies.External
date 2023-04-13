using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models.EmailTemplates;

namespace Dfe.Academies.External.Web.Factories
{
	public interface IContributorNotifyTemplateFactory
	{
		IContributorTemplate Get(ApplicationTypes applicationType, SchoolRoles schoolRole);
	}
}
