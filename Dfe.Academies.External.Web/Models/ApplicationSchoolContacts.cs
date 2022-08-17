namespace Dfe.Academies.External.Web.Models;

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
	string ContactRole, // "headteacher", "chair of governing body", "someone else"
	string? MainContactOtherName,
	string? MainContactOtherEmail,
	string? MainContactOtherTelephone,
	string? MainContactOtherRole,
	string? ApproverContactName,
	string? ApproverContactEmail);
