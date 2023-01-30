using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationJoinTrustReasonsModel : BaseSchoolPageEditModel
	{
		// MR:- VM props to capture pupil numbers data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public string? ApplicationJoinTrustReason { get; set; } = string.Empty;

		public ApplicationJoinTrustReasonsModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationCreationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, 
				academisationCreationService, "ApplicationChangeSchoolName")
		{}
		
		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}
			else
			{
				return true;
			}
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new Dictionary<string, dynamic>
			{
				{ nameof(SchoolApplyingToConvert.ApplicationJoinTrustReason), ApplicationJoinTrustReason! }
			};
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			ApplicationJoinTrustReason = selectedSchool.ApplicationJoinTrustReason;
		}
	}
}
