namespace Dfe.Academies.External.Web.ViewModels;

public class SchoolConversionComponentSectionViewModel : SchoolQuestionAndAnswerViewModel
{
	// section 1 - school joining trust
	public const string NameOfSchoolSectionName = "The name of the school";
	// section 2 - contact details section
	public const string ContactDetailsHeadteacherNameSectionName = "Name of headteacher";
	public const string ContactDetailsHeadteacherEmailSectionName = "Headteacher's email address";
	public const string ContactDetailsHeadteacherTelNoSectionName = "Headteacher's telephone number";
	public const string ContactDetailsChairNameSectionName = "Name of the chair of the governing body";
	public const string ContactDetailsChairEmailSectionName = "Chair's email address";
	public const string ContactDetailsChairTelNoSectionName = "Chair's telephone number";
	public const string ContactDetailsMainContactRoleSectionName = "Who is the main contact for the conversion?";
	public const string ContactDetailsMainContactOtherNameSectionName = "Main contact name";
	public const string ContactDetailsMainContactOtherEmailSectionName = "Main contact email";
	public const string ContactDetailsMainContactOtherTelephoneSectionName = "Main contact telephone number";
	public const string ContactDetailsApproversFullNameSectionName = "Approver's full name";
	public const string ContactDetailsApproversEmailSectionName = "Approver's email address";
	// section 3 - conversion date section
	public const string ApplicationConversionTargetDateSectionName = "Do you want the conversion to happen on a particular date?";
	// section 4 - reasons for joining trust section
	public const string ReasonsForJoiningTrustSectionName = "Why does the school want to join this trust in particular?";
	// section 5 - school changing name section
	public const string NameOfSchoolChangingSectionName = "Is the school planning to change its name when it becomes an academy?";

	public SchoolConversionComponentSectionViewModel(string name, string answer) : base(name, answer)
	{
	}
}
