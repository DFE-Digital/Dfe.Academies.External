namespace Dfe.Academies.External.Web.ViewModels;

public abstract class SchoolQuestionAndAnswerViewModel
{
	protected SchoolQuestionAndAnswerViewModel(string name, string answer)
	{
		Name = name;
		Answer = answer;
		SubQuestionAndAnswers = new();
	}

	/// <summary>
	/// Name of section e.g. 'name of headteacher'
	/// </summary>
	public string Name { get; set; }

	//// MR:- below sets text to public const string NoInfo = "You have not added any information";
	//// Answer = ((string) ViewData[FieldConstants.SipSchoolConversionMainContactOtherName]).DisplayNoInfoIfNullOrEmpty(),
	public string Answer { get; set; }

	// Question can have sub-questions e.g. conditional radio
	// i.e. 'Are there any current or planned building works?'. This has 2 sub questions:- 1) provide details, 2) When is scheduled completion date
	public List<SchoolQuestionAndAnswerViewModel> SubQuestionAndAnswers { get; set; }
}