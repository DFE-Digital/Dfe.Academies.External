using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Models;

public record CreateApplicationApiModel(ApplicationTypes ApplicationType,
	ConversionApplicationContributor Contributor);
