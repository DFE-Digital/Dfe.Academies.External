namespace Dfe.Academies.External.Web.ViewModels;

public abstract class SchoolQuestionAndAnswerViewModel
{
	protected SchoolQuestionAndAnswerViewModel(string name, string answer)
	{
		Name = name;
		Answer = answer;
	}

	/// <summary>
	/// Name of section e.g. 'name of headteacher'
	/// </summary>
	public string Name { get; set; }

	//// MR:- below sets text to public const string NoInfo = "You have not added any information";
	//// Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherName]).DisplayNoInfoIfNullOrEmpty(),
	public string Answer { get; set; }

	// FieldName = FieldConstants.SipSchoolConversionMainContactOtherName // MR:- ??
}