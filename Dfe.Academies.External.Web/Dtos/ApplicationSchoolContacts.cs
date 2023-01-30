using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.Dtos;

public record ApplicationSchoolContacts(
	int ApplicationId,
	int Urn,
	// contact details
	string ContactHeadName,
	string ContactHeadEmail,
	string ContactHeadTel,
	string ContactChairName,
	string ContactChairEmail,
	string ContactChairTel,
	MainConversionContact ContactRole, // "headteacher", "chair of governing body", "someone else"
	string? MainContactOtherName,
	string? MainContactOtherEmail,
	string? MainContactOtherTelephone,
	string? ApproverContactName,
	string? ApproverContactEmail);
