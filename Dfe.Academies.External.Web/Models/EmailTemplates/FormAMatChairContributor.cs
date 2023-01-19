using System.Dynamic;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Microsoft.Extensions.Options;

namespace Dfe.Academies.External.Web.Models.EmailTemplates
{
	public class FormAMatChairContributor : IContributorTemplate
	{
		public ApplicationTypes ApplicationType => ApplicationTypes.FormAMat;
		public SchoolRoles SchoolRole => SchoolRoles.ChairOfGovernors;
		public string TemplateId { get; }

		public FormAMatChairContributor(IOptions<NotifyTemplateSettings> notifyTemplateSettings)
		{
			TemplateId = notifyTemplateSettings.Value.FamChairTemplateId;
		}
	}
}
