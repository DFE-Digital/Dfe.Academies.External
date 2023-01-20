using System.Dynamic;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.Extensions.Options;

namespace Dfe.Academies.External.Web.Models.EmailTemplates
{
	public class JoinAMatNonChairContributor : IContributorTemplate
	{
		public ApplicationTypes ApplicationType => ApplicationTypes.JoinAMat;
		public SchoolRoles SchoolRole => SchoolRoles.Other;
		public string TemplateId { get; }

		public JoinAMatNonChairContributor(IOptions<NotifyTemplateSettings> notifyTemplateSettings)
		{
			TemplateId = notifyTemplateSettings.Value.JamNonChairTemplateId;
		}
	}
}
