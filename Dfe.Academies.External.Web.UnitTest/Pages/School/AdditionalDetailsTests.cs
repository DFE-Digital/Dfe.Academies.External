using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class AdditionalDetailsTests
    {
		[Test]
		public void RunUiValidation_ResolutionConsentFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("ResolutionConsentFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_FoundationConsentFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { };
			model.FoundationConsentFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("FoundationConsentFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_DioceseFileTooLarge_AddsModelError()
		{
			var fileUploadServiceMock = new Mock<IFileUploadService>();
			var conversionAppRetrievalServiceMock = new Mock<IConversionApplicationRetrievalService>();
			var referenceDataRetrievalServiceMock = new Mock<IReferenceDataRetrievalService>();
			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var model = SetupAdditionalDetails(
				conversionAppRetrievalServiceMock.Object,
				referenceDataRetrievalServiceMock.Object,
				conversionAppServiceMock.Object,
				fileUploadServiceMock.Object
			);

			// Mock a file that is too large
			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.Length).Returns(FileUploadConstants.MaxFileUploadSizeInBytes);
			fileMock.Setup(f => f.FileName).Returns("toolarge.pdf");

			model.ResolutionConsentFiles = new List<IFormFile> { };
			model.FoundationConsentFiles = new List<IFormFile> { };
			model.DioceseFiles = new List<IFormFile> { fileMock.Object };

			// ModelState must be valid before file size check
			model.ModelState.Clear();

			// Act
			var isValid = model.RunUiValidation();

			// Assert
			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("DioceseFileSizeError"), Is.True);
		}

		[Test]
		public void RunUiValidation_OfstedInspectedYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.OfstedInspected = SelectOption.Yes;
			model.OfstedInspectionDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("OfstedInspectionDetailsNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_LocalAuthorityReorganisationYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.LocalAuthorityReorganisation = SelectOption.Yes;
			model.LocalAuthorityReorganisationDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("LocalAuthorityReorganisationDetailsNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_LinkedToDioceseYesAndDioceseNameEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.LinkedToDiocese = SelectOption.Yes;
			model.DioceseName = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("DioceseNameNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_SupportedByFoundationYesAndNameEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.SupportedByFoundationTrustOrBody = SelectOption.Yes;
			model.FoundationTrustOrBodyName = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("FoundationTrustOrBodyNameNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_ExemptionFromSACREYesAndNoDate_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ExemptionFromSACRE = SelectOption.Yes;
			model.ExemptionEndDate = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("exemptionFromSACREEndDateNotAdded"), Is.True);
		}

		[Test]
		public void RunUiValidation_EqualityAssessmentYesAndNoCharacteristicsSelected_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.EqualityAssessment = SelectOption.Yes;
			model.DisproportionateProtectedCharacteristics = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("equalitiesImpactAssessmentOptionNoOptionSelected"), Is.True);
		}

		[Test]
		public void RunUiValidation_FurtherInformationYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.FurtherInformation = SelectOption.Yes;
			model.FurtherInformationDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("furtherInformationDetailsNotAdded"), Is.True);
		}

		[Test]
		public void HasError_WhenTrustBenefitDetailsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("TrustBenefitDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.TrustBenefitDetailsError, Is.True);
		}

		[Test]
		public void RunUiValidation_WhenAllConditionalValidationsPass_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.TrustBenefitDetails = "Some benefit";
			model.OfstedInspected = SelectOption.No;
			model.LocalAuthorityReorganisation = SelectOption.No;
			model.LocalAuthorityClosurePlans = SelectOption.No;
			model.LinkedToDiocese = SelectOption.No;
			model.SupportedByFoundationTrustOrBody = SelectOption.No;
			model.ExemptionFromSACRE = SelectOption.No;
			model.EqualityAssessment = SelectOption.No;
			model.FurtherInformation = SelectOption.No;
			model.MainFeederSchools = "Feeder school list";
			model.ResolutionConsentFiles = new List<IFormFile>();
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.True);
		}

		[Test]
		public void RunUiValidation_LocalAuthorityClosurePlansYesAndDetailsEmpty_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.LocalAuthorityClosurePlans = SelectOption.Yes;
			model.LocalAuthorityClosurePlanDetails = null;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("localAuthorityClosurePlanDetailsNotAdded"), Is.True);
		}

		[Test]
		public void PopulateUpdateDictionary_ThrowsNotImplementedException()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());

			Assert.That(() => model.PopulateUpdateDictionary(), Throws.TypeOf<System.NotImplementedException>());
		}

		[Test]
		public async Task OnGetRemoveFileAsync_CallsDeleteFileAndRedirects()
		{
			const int appId = 5;
			const int urn = 100;
			var entityId = System.Guid.NewGuid().ToString();
			var applicationReference = "APP-001";
			var section = "diocese";
			var fileName = "doc.pdf";

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock
				.Setup(x => x.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName))
				.Returns(Task.CompletedTask);

			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				fileUploadMock.Object);

			var result = await model.OnGetRemoveFileAsync(appId, urn, entityId, applicationReference, section, fileName);

			fileUploadMock.Verify(
				x => x.DeleteFile(FileUploadConstants.TopLevelSchoolFolderName, entityId, applicationReference, section, fileName),
				Times.Once);
			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.PageName, Is.EqualTo("AdditionalDetails"));
			var routeValues = redirect.RouteValues!;
			Assert.That(routeValues["Urn"], Is.Not.Null);
			Assert.That(routeValues["Urn"], Is.EqualTo(urn));
			Assert.That(routeValues["AppId"], Is.EqualTo(appId));
		}

		[Test]
		public void PopulateUiModel_WhenSchoolHasData_PopulatesModel()
		{
			var entityId = System.Guid.NewGuid();
			var school = new SchoolApplyingToConvert("Test School", 200, null)
			{
				EntityId = entityId,
				TrustBenefitDetails = "Trust benefits",
				OfstedInspectionDetails = "Ofsted details",
				Safeguarding = true,
				LocalAuthorityReorganisationDetails = "LA reorg",
				LocalAuthorityClosurePlanDetails = "Closure plan",
				DioceseName = "Diocese A",
				PartOfFederation = false,
				FoundationTrustOrBodyName = null,
				ExemptionEndDate = null,
				MainFeederSchools = "Feeder 1, Feeder 2",
				ProtectedCharacteristics = SchoolEqualitiesProtectedCharacteristics.Unlikely,
				FurtherInformation = "More info"
			};

			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());

			model.PopulateUiModel(school);

			Assert.That(model.SchoolName, Is.EqualTo("Test School"));
			Assert.That(model.TrustBenefitDetails, Is.EqualTo("Trust benefits"));
			Assert.That(model.OfstedInspectionDetails, Is.EqualTo("Ofsted details"));
			Assert.That(model.OfstedInspected, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.SafeguardingInvestigations, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.LocalAuthorityReorganisationDetails, Is.EqualTo("LA reorg"));
			Assert.That(model.LocalAuthorityClosurePlans, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.LocalAuthorityClosurePlanDetails, Is.EqualTo("Closure plan"));
			Assert.That(model.DioceseName, Is.EqualTo("Diocese A"));
			Assert.That(model.LinkedToDiocese, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.PartOfFederation, Is.EqualTo(SelectOption.No));
			Assert.That(model.MainFeederSchools, Is.EqualTo("Feeder 1, Feeder 2"));
			Assert.That(model.DisproportionateProtectedCharacteristics, Is.EqualTo(SchoolEqualitiesProtectedCharacteristics.Unlikely));
			Assert.That(model.EqualityAssessment, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.FurtherInformation, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.FurtherInformationDetails, Is.EqualTo("More info"));
			Assert.That(model.EntityId, Is.EqualTo(entityId));
		}

		[Test]
		public void PopulateUiModel_WhenSchoolSectionNotStarted_SetsNullsAndNoForDefaults()
		{
			var school = new SchoolApplyingToConvert("New School", 300, null)
			{
				TrustBenefitDetails = null!,
				OfstedInspectionDetails = null,
				Safeguarding = null,
				LocalAuthorityReorganisationDetails = null,
				DioceseName = null,
				FoundationTrustOrBodyName = null,
				FurtherInformation = null
			};

			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());

			model.PopulateUiModel(school);

			Assert.That(model.SchoolName, Is.EqualTo("New School"));
			Assert.That(model.TrustBenefitDetails, Is.Null);
			Assert.That(model.OfstedInspected, Is.Null);
			Assert.That(model.SafeguardingInvestigations, Is.Null);
			Assert.That(model.LinkedToDiocese, Is.Null);
			Assert.That(model.SupportedByFoundationTrustOrBody, Is.Null);
			Assert.That(model.FurtherInformation, Is.Null);
		}

		[Test]
		public void HasError_WhenOfstedInspectedDetailsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("OfstedInspectionDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.OfstedInspectedDetailsError, Is.True);
		}

		[Test]
		public void HasError_WhenExemptionFromSACREError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("exemptionFromSACREEndDateNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.ExemptionFromSACREError, Is.True);
		}

		[Test]
		public void HasError_WhenFurtherInformationError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("furtherInformationDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.FurtherInformationError, Is.True);
		}

		[Test]
		public void DioceseFileSizeError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("DioceseFileSizeError", "File too large");

			Assert.That(model.DioceseFileSizeError, Is.True);
		}

		[Test]
		public void MainFeederSchoolsError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("MainFeederSchoolsDetailsNotAdded", "Error");

			Assert.That(model.MainFeederSchoolsError, Is.True);
		}

		[Test]
		public void PopulateValidationMessages_CallsPopulateViewDataErrorsWithModelStateErrors()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("TrustBenefitDetails", "Required");

			model.PopulateValidationMessages();

			Assert.That(model.ViewData["Errors"], Is.Not.Null);
		}

		private static AdditionalDetails SetupAdditionalDetails(
			IConversionApplicationRetrievalService conversionAppRetrievalServiceMock,
			IReferenceDataRetrievalService referenceDataRetrievalServiceMock,
			IConversionApplicationService conversionAppServiceMock,
			IFileUploadService fileUploadServiceMock,
			bool isAuthenticated = false
		)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(false);

			var model = new AdditionalDetails(
				fileUploadServiceMock,
				conversionAppRetrievalServiceMock,
				referenceDataRetrievalServiceMock,
				conversionAppServiceMock
				
			)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};

			return model;
		}
	}
}
