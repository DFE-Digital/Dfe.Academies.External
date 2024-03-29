﻿using Dfe.Academies.External.Web.Enums;

namespace Dfe.Academies.External.Web.ViewModels;

public abstract class SchoolQuestionAndAnswerSectionViewModel
{
	protected SchoolQuestionAndAnswerSectionViewModel(string title, string uRi)
	{
		Title = title;
		URI = uRi;
	}

	/// <summary>
	/// e.g. Title = "The school joining the trust"
	/// </summary>
	public string Title { get; set; }

	// Answer = ((string) ViewData[FieldConstants.SchoolName]).DisplayNoInfoIfNullOrEmpty(),

	//// Status = (Enums.SchoolConversionComponentStatus) ViewData[$"status-{reviewSectionMainContact.ChangeReference}"];

	/// <summary>
	/// MR:- this is overall status i.e. if headteacher && chair && main contact not filled in !!
	/// </summary>
	public SchoolConversionComponentStatus Status { get; set; } = SchoolConversionComponentStatus.NotStarted;

	// MR:- need a URI similar to ApplicationComponentViewModel - to render 'start section' button
	public string URI { get; set; }

}
