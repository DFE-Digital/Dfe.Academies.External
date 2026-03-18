using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationSchoolConsultationModelTests
{
	/// <summary>
	/// "draftConversionApplication" in temp storage
	/// from previous step in the new application wizard
	/// </summary>
	/// <returns></returns>
	[Test]
	public async Task OnGetAsync___Valid___NullErrors()
	{
		// arrange
		var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationSchoolConsultationModel(mockConversionApplicationRetrievalService.Object,
															mockReferenceDataRetrievalService.Object,
															mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public void RunUiValidation_SchoolConsultationStakeholdersNoAndConsultEmpty_AddsModelErrorAndReturnsFalse()
	{
		var pageModel = SetupApplicationSchoolConsultationModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SchoolConsultationStakeholders = SelectOption.No;
		pageModel.SchoolConsultationStakeholdersConsult = null;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("SchoolConsultationStakeholdersConsultNotEntered"), Is.True);
		Assert.That(pageModel.SchoolConsultationStakeholdersConsultError, Is.True);
		Assert.That(pageModel.HasError, Is.True);
	}

	[Test]
	public void RunUiValidation_ValidModelState_ReturnsTrue()
	{
		var pageModel = SetupApplicationSchoolConsultationModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SchoolConsultationStakeholders = SelectOption.Yes;
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void RunUiValidation_StakeholdersNoAndConsultProvided_ReturnsTrue()
	{
		var pageModel = SetupApplicationSchoolConsultationModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SchoolConsultationStakeholders = SelectOption.No;
		pageModel.SchoolConsultationStakeholdersConsult = "We will consult in January";
		pageModel.ModelState.Clear();

		var isValid = pageModel.RunUiValidation();

		Assert.That(isValid, Is.True);
	}

	[Test]
	public void PopulateUpdateDictionary_SchoolConsultationStakeholdersNo_ReturnsDictionaryWithPlanToConsult()
	{
		var pageModel = SetupApplicationSchoolConsultationModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SchoolConsultationStakeholders = SelectOption.No;
		pageModel.SchoolConsultationStakeholdersConsult = "Consultation details";

		var result = pageModel.PopulateUpdateDictionary();

		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders)], Is.EqualTo(false));
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders)], Is.EqualTo("Consultation details"));
	}

	[Test]
	public void PopulateUpdateDictionary_SchoolConsultationStakeholdersYes_ReturnsDictionaryWithNullPlanToConsult()
	{
		var pageModel = SetupApplicationSchoolConsultationModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());
		pageModel.SchoolConsultationStakeholders = SelectOption.Yes;

		var result = pageModel.PopulateUpdateDictionary();

		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolHasConsultedStakeholders)], Is.EqualTo(true));
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolPlanToConsultStakeholders)], Is.Null);
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static ApplicationSchoolConsultationModel SetupApplicationSchoolConsultationModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationService academisationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationSchoolConsultationModel(mockConversionApplicationRetrievalService,
													mockReferenceDataRetrievalService,
													academisationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
