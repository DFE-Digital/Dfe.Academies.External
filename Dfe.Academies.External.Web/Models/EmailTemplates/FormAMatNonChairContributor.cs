using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.Extensions.Options;

namespace Dfe.Academies.External.Web.Models.EmailTemplates
{
	public class FormAMatNonChairContributor : IContributorTemplate
	{
		public ApplicationTypes ApplicationType => ApplicationTypes.FormAMat;
		public SchoolRoles SchoolRole => SchoolRoles.Other;
		public string TemplateId { get; }

		public FormAMatNonChairContributor(IOptions<NotifyTemplateSettings> notifyTemplateSettings)
		{
			TemplateId = notifyTemplateSettings.Value.FamNonChairTemplateId;
		}
	}
}
