using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	/// <summary>
	/// MR:- clone of ConversionKeyDetailsReview.cshtml - A2C-SIP
	/// </summary>
	public class SchoolConversionKeyDetailsModel : BasePageEditModel
	{
		private readonly ILogger<PupilNumbersModel> _logger;

		//// MR:- selected school props for UI rendering
		[BindProperty] public int ApplicationId { get; set; }

		[BindProperty] public int Urn { get; private set; }

		public string SchoolName { get; private set; } = string.Empty;

		//// MR:- VM props to capture pupil numbers data

		// TODO MR:- some representation of section & answers
		// e.g. 'contact details' -> 'name of headteacher' / 'name of chair'

		public SchoolConversionKeyDetailsModel(ILogger<PupilNumbersModel> logger,
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_logger = logger;
		}

		public async Task OnGetAsync()
		{
			try
			{
				// TODO MR:-
			}
			catch (Exception ex)
			{
				_logger.LogError("School::SchoolConversionKeyDetailsModel::OnGetAsync::Exception - {Message}", ex.Message);
			}
		}

		public override void PopulateValidationMessages()
        {
	        ViewData["Errors"] = ConvertModelStateToDictionary();

	        if (!ModelState.IsValid)
	        {
		        foreach (var modelStateError in ConvertModelStateToDictionary())
		        {
			        // MR:- add friendly message for validation summary
			        if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
			        {
				        this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
			        }
		        }
	        }
        }

        private void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
        {
	        ApplicationId = selectedSchool.ApplicationId;
	        Urn = selectedSchool.URN;
	        SchoolName = selectedSchool.SchoolName;
			// TODO MR:- sort out sections
        }
    }
}
