using System.Dynamic;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.Extensions.Options;

namespace Dfe.Academies.External.Web.Models.EmailTemplates
{
	public class JoinAMatChairContributor : IContributorTemplate
	{
		public ApplicationTypes ApplicationType => ApplicationTypes.JoinAMat;
		public SchoolRoles SchoolRole => SchoolRoles.ChairOfGovernors;
		public string TemplateId { get; }

		public JoinAMatChairContributor(IOptions<NotifyTemplateSettings> notifyTemplateSettings)
		{
			TemplateId = notifyTemplateSettings.Value.JamChairTemplateId;
		}
	}
}
