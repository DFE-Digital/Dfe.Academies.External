namespace Dfe.Academies.External.Web.Dtos;

public record ApplicationSchoolSharepointServiceModel(
	int Id,
	string ApplicationReference,
	List<SchoolSharepointServiceModel> SchoolSharepointServiceModels);
