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

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class ApplicationConversionTargetDateModelTests
	{
		// MR:- test calculated props

		[Test]
		public async Task HasError_TargetDateExplainedError_Property__True()
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
			var pageModel = SetupApplicationConversionTargetDateModel(mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);

			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);
			pageModel.ModelState.AddModelError("TargetDateExplainedNotEntered", "You must provide details");

			// assert
			Assert.That(pageModel.TargetDateExplainedError, Is.EqualTo(true));
			Assert.That(pageModel.HasError, Is.EqualTo(true));
		}

		[Test]
		public async Task HasError_SchoolConversionTargetDateError_Property__True()
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
			var pageModel = SetupApplicationConversionTargetDateModel(mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);

			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);
			pageModel.ModelState.AddModelError("SchoolConversionTargetDateNotEntered", "You must provide details");

			// assert
			Assert.That(pageModel.SchoolConversionTargetDateError, Is.EqualTo(true));
			Assert.That(pageModel.HasError, Is.EqualTo(true));
		}

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
			var pageModel = SetupApplicationConversionTargetDateModel(mockConversionApplicationCreationService.Object,
				mockConversionApplicationRetrievalService.Object,
				mockReferenceDataRetrievalService.Object);
			TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

			// act
			await pageModel.OnGetAsync(urn, applicationId);

			// assert
			Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
		}

		[Test]
		public void RunUiValidation_TargetDateDifferentYesAndTargetDateLocalMinValue_AddsModelErrorAndReturnsFalse()
		{
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.Yes;
			pageModel.TargetDateLocal = System.DateTime.MinValue;
			pageModel.TargetDateExplained = "Some reason";
			pageModel.ModelState.Clear();

			var isValid = pageModel.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(pageModel.ModelState.ContainsKey("SchoolConversionTargetDateNotEntered"), Is.True);
		}

		[Test]
		public void RunUiValidation_TargetDateDifferentYesAndTargetDateExplainedEmpty_AddsModelErrorAndReturnsFalse()
		{
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.Yes;
			pageModel.TargetDateLocal = new System.DateTime(2025, 6, 1);
			pageModel.TargetDateExplained = null;
			pageModel.ModelState.Clear();

			var isValid = pageModel.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(pageModel.ModelState.ContainsKey("TargetDateExplainedNotEntered"), Is.True);
		}

		[Test]
		public void RunUiValidation_ValidTargetDateAndExplained_ReturnsTrue()
		{
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.Yes;
			pageModel.TargetDateLocal = new System.DateTime(2025, 6, 1);
			pageModel.TargetDateExplained = "Reason for this date";
			pageModel.ModelState.Clear();

			var isValid = pageModel.RunUiValidation();

			Assert.That(isValid, Is.True);
		}

		[Test]
		public void RunUiValidation_TargetDateDifferentNo_ReturnsTrue()
		{
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.No;
			pageModel.ModelState.Clear();

			var isValid = pageModel.RunUiValidation();

			Assert.That(isValid, Is.True);
		}

		[Test]
		public void PopulateUpdateDictionary_TargetDateDifferentNo_ReturnsDictionaryWithNullDateAndExplained()
		{
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.No;

			var result = pageModel.PopulateUpdateDictionary();

			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDateSpecified)], Is.False);
			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDate)], Is.Null);
			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained)], Is.Null);
		}

		[Test]
		public void PopulateUpdateDictionary_TargetDateDifferentYes_ReturnsDictionaryWithDateAndExplained()
		{
			var targetDate = new System.DateTime(2025, 6, 15);
			var pageModel = SetupApplicationConversionTargetDateModel(
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>());
			pageModel.TargetDateDifferent = SelectOption.Yes;
			pageModel.TargetDateLocal = targetDate;
			pageModel.TargetDateExplained = "Conversion date reason";

			var result = pageModel.PopulateUpdateDictionary();

			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDateSpecified)], Is.True);
			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDate)], Is.EqualTo(targetDate));
			Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionTargetDateExplained)], Is.EqualTo("Conversion date reason"));
		}

		// TODO :- OnPostAsync___ModelIsValid___Invalid
		// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

		// TODO :- OnPostAsync___ModelIsValid___Valid
		// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

		private static ApplicationConversionTargetDateModel SetupApplicationConversionTargetDateModel(
			IConversionApplicationService mockConversionApplicationCreationService,
			IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
			IReferenceDataRetrievalService mockReferenceDataRetrievalService,
			bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new ApplicationConversionTargetDateModel(mockConversionApplicationRetrievalService,
				mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}
