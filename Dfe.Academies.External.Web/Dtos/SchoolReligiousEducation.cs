namespace Dfe.Academies.External.Web.Dtos;

public record SchoolReligiousEducation(
	bool? FaithSchool = null, // linked to a diocese yes/no?
	string? FaithSchoolDioceseName = null,
	string? DiocesePermissionEvidenceDocumentLink = null,
	bool? HasSACREException = null,
	DateTime? SACREExemptionEndDate = null
);
