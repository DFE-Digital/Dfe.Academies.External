using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Pages.School
{
    public class DeclarationSummaryModel : BaseSchoolSummaryPageModel
	{
	    public List<DeclarationSummaryHeadingViewModel> ViewModel { get; set; } = new();

		public DeclarationSummaryModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			DeclarationSummaryHeadingViewModel heading1 = new(DeclarationSummaryHeadingViewModel.Heading,
				"/school/Declaration")
			{
				Status = selectedSchool.DeclarationBodyAgree.HasValue ?
					SchoolConversionComponentStatus.Complete
					: SchoolConversionComponentStatus.NotStarted
			};

			// MR:- NO sub questions shown here, just yes / no for answer!
			heading1.Sections.Add(new(
				DeclarationSummaryHeadingViewModel.HeadingDetails,
				selectedSchool.DeclarationIAmTheChairOrHeadteacher.GetStringDescription()
			));

			var vm = new List<DeclarationSummaryHeadingViewModel> { heading1 };

			ViewModel = vm;
		}
	}
}
