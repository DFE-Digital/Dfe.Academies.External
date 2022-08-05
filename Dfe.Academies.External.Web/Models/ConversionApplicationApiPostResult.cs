using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public class ConversionApplicationApiPostResult
{
	public int ApplicationId { get; set; }
	public ApplicationTypes ApplicationType { get; set; }
}