using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models.EmailTemplates;

namespace Dfe.Academies.External.Web.Factories;

public class ContributorNotifyTemplateFactory : IContributorNotifyTemplateFactory
{
	private readonly List<IContributorTemplate> _emailTemplates;

	public ContributorNotifyTemplateFactory(IEnumerable<IContributorTemplate> emailTemplates)
	{
		_emailTemplates = emailTemplates.ToList();
	}
	public IContributorTemplate Get(ApplicationTypes applicationType, SchoolRoles schoolRole)
	{
		return _emailTemplates.First(x =>
			x.ApplicationType == applicationType && x.SchoolRole == schoolRole);
	}
}
