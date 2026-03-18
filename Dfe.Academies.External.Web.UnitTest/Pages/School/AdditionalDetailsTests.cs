using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Primitives;
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

		[Test]
		public void HasError_WhenNoErrors_ReturnsFalse()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());

			Assert.That(model.HasError, Is.False);
		}

		[Test]
		public void HasError_WhenSafeguardingInvestigationsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("SafeguardingDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.SafeguardingInvestigationsError, Is.True);
		}

		[Test]
		public void HasError_WhenDioceseNameError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("DioceseNameNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.DioceseNameError, Is.True);
		}

		[Test]
		public void HasError_WhenDioceseFileNotAddedError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("DioceseFileNotAddedError", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.DioceseFileNotAddedError, Is.True);
		}

		[Test]
		public void HasError_WhenLocalAuthorityReorganisationDetailsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("LocalAuthorityReorganisationDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.LocalAuthorityReorganisationDetailsError, Is.True);
		}

		[Test]
		public void HasError_WhenLocalAuthorityClosurePlanDetailsError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("localAuthorityClosurePlanDetailsNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.LocalAuthorityClosurePlanDetailsError, Is.True);
		}

		[Test]
		public void HasError_WhenSupportedByFoundationTrustOrBodyError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("FoundationTrustOrBodyNameNotAdded", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.SupportedByFoundationTrustOrBodyError, Is.True);
		}

		[Test]
		public void HasError_WhenExemptionEndDateNotEntered_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("ExemptionEndDateNotEntered", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.ExemptionEndDateNotEntered, Is.True);
		}

		[Test]
		public void HasError_WhenEqualityAssessmentError_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("equalitiesImpactAssessmentOptionNoOptionSelected", "Error");

			Assert.That(model.HasError, Is.True);
			Assert.That(model.EqualityAssessmentError, Is.True);
		}

		[Test]
		public void FoundationConsentFileError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("FoundationConsentFileNotAddedError", "Error");

			Assert.That(model.FoundationConsentFileError, Is.True);
		}

		[Test]
		public void FoundationConsentFileSizeError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("FoundationConsentFileSizeError", "Error");

			Assert.That(model.FoundationConsentFileSizeError, Is.True);
		}

		[Test]
		public void ResolutionConsentFileSizeError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("ResolutionConsentFileSizeError", "Error");

			Assert.That(model.ResolutionConsentFileSizeError, Is.True);
		}

		[Test]
		public void DioceseFileNotAddedError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError("DioceseFileNotAddedError", "Error");

			Assert.That(model.DioceseFileNotAddedError, Is.True);
		}

		[Test]
		public void DioceseFileGenericError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError(nameof(AdditionalDetails.DioceseFileGenericError), "Error");

			Assert.That(model.DioceseFileGenericError, Is.True);
		}

		[Test]
		public void FoundationConsentFileGenericError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError(nameof(AdditionalDetails.FoundationConsentFileGenericError), "Error");

			Assert.That(model.FoundationConsentFileGenericError, Is.True);
		}

		[Test]
		public void ResolutionConsentFileGenericError_WhenModelStateContainsKey_ReturnsTrue()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ModelState.AddModelError(nameof(AdditionalDetails.ResolutionConsentFileGenericError), "Error");

			Assert.That(model.ResolutionConsentFileGenericError, Is.True);
		}

		[Test]
		public void RunUiValidation_ExemptionFromSACREYesAndExemptionEndDateMinValue_AddsModelError()
		{
			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ExemptionFromSACRE = SelectOption.Yes;
			model.ExemptionEndDate = DateTimeOffset.MinValue;
			model.ModelState.Clear();

			var isValid = model.RunUiValidation();

			Assert.That(isValid, Is.False);
			Assert.That(model.ModelState.ContainsKey("exemptionFromSACREEndDateNotAdded"), Is.True);
		}

		[Test]
		public async Task OnGetAsync_WhenApplicationAndSchoolExist_LoadsPageAndPopulatesFileNames()
		{
			const int appId = 10;
			const int urn = 200;
			var entityId = System.Guid.NewGuid();
			var application = new ConversionApplication
			{
				ApplicationId = appId,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("Test School", urn, null) { id = 1, EntityId = entityId, TrustBenefitDetails = "Benefits" }
				}
			};

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock.Setup(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.DioceseFilePrefixFieldName))
				.ReturnsAsync(new List<string>());
			fileUploadMock.Setup(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.FoundationConsentFilePrefixFieldName))
				.ReturnsAsync(new List<string>());
			fileUploadMock.Setup(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.ResolutionConsentfilePrefixFieldName))
				.ReturnsAsync(new List<string>());

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				fileUploadMock.Object);

			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new ConversionApplication());

			var result = await model.OnGetAsync(urn, appId);

			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(model.ApplicationId, Is.EqualTo(appId));
			Assert.That(model.Urn, Is.EqualTo(urn));
			Assert.That(model.SchoolName, Is.EqualTo("Test School"));
			Assert.That(model.TrustBenefitDetails, Is.EqualTo("Benefits"));
			fileUploadMock.Verify(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.DioceseFilePrefixFieldName), Times.Once);
			fileUploadMock.Verify(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.FoundationConsentFilePrefixFieldName), Times.Once);
			fileUploadMock.Verify(x => x.GetFiles(It.IsAny<string>(), entityId.ToString(), "APP-REF", FileUploadConstants.ResolutionConsentfilePrefixFieldName), Times.Once);
		}

		[Test]
		public async Task OnPostAsync_WhenValidationFails_ReturnsPage()
		{
			var application = new ConversionApplication
			{
				ApplicationId = 1,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("School", 100, null) { id = 1, EntityId = System.Guid.NewGuid() }
				}
			};
			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(1)).ReturnsAsync(application);

			var formMock = new Mock<IFormCollection>();
			formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());
			model.ApplicationId = 1;
			model.Urn = 100;
			model.EntityId = application.Schools[0].EntityId;
			model.ApplicationReference = "APP-REF";
			model.ExemptionEndDateName = "ExemptionEndDate";
			model.Request.Form = formMock.Object;
			model.TrustBenefitDetails = null;
			model.OfstedInspected = SelectOption.Yes;
			model.OfstedInspectionDetails = null;
			model.DioceseFileNames = new List<string>();
			model.FoundationConsentFileNames = new List<string>();
			model.ResolutionConsentFileNames = new List<string>();

			var result = await model.OnPostAsync();

			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(model.ModelState.ContainsKey("OfstedInspectionDetailsNotAdded"), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenUploadFilesFails_DioceseFile_ReturnsPageAndAddsError()
		{
			var entityId = System.Guid.NewGuid();
			var application = new ConversionApplication
			{
				ApplicationId = 1,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("School", 100, null) { id = 1, EntityId = entityId }
				}
			};
			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(1)).ReturnsAsync(application);

			var formMock = new Mock<IFormCollection>();
			formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.DioceseFilePrefixFieldName, It.IsAny<IFormFile>()))
				.ThrowsAsync(new FileUploadException("Upload failed"));

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				fileUploadMock.Object);
			model.ApplicationId = 1;
			model.Urn = 100;
			model.EntityId = entityId;
			model.ApplicationReference = "APP-REF";
			model.ExemptionEndDateName = "ExemptionEndDate";
			model.Request.Form = formMock.Object;
			model.TrustBenefitDetails = "Benefit";
			model.OfstedInspected = SelectOption.No;
			model.LocalAuthorityReorganisation = SelectOption.No;
			model.LocalAuthorityClosurePlans = SelectOption.No;
			model.LinkedToDiocese = SelectOption.Yes;
			model.DioceseName = "Diocese";
			model.PartOfFederation = SelectOption.No;
			model.SupportedByFoundationTrustOrBody = SelectOption.No;
			model.ExemptionFromSACRE = SelectOption.No;
			model.MainFeederSchools = "Feeder";
			model.EqualityAssessment = SelectOption.No;
			model.FurtherInformation = SelectOption.No;
			model.DioceseFileNames = new List<string>();
			model.FoundationConsentFileNames = new List<string>();
			model.ResolutionConsentFileNames = new List<string>();
			model.DioceseFiles = new List<IFormFile> { new Mock<IFormFile>().Object };
			model.FoundationConsentFiles = new List<IFormFile>();
			model.ResolutionConsentFiles = new List<IFormFile>();
			TempDataHelper.StoreSerialisedValue($"{entityId}-dioceseFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-foundationConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-resolutionConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new ConversionApplication());

			var result = await model.OnPostAsync();

			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(model.ModelState.ContainsKey(nameof(AdditionalDetails.DioceseFileGenericError)), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenUploadFilesFails_FoundationFile_ReturnsPageAndAddsError()
		{
			var entityId = System.Guid.NewGuid();
			var application = new ConversionApplication
			{
				ApplicationId = 1,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("School", 100, null) { id = 1, EntityId = entityId }
				}
			};
			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(1)).ReturnsAsync(application);

			var formMock = new Mock<IFormCollection>();
			formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.DioceseFilePrefixFieldName, It.IsAny<IFormFile>()))
				.ReturnsAsync("diocese-file-id");
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.FoundationConsentFilePrefixFieldName, It.IsAny<IFormFile>()))
				.ThrowsAsync(new FileUploadException("Upload failed"));

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				fileUploadMock.Object);
			model.ApplicationId = 1;
			model.Urn = 100;
			model.EntityId = entityId;
			model.ApplicationReference = "APP-REF";
			model.ExemptionEndDateName = "ExemptionEndDate";
			model.Request.Form = formMock.Object;
			model.TrustBenefitDetails = "Benefit";
			model.OfstedInspected = SelectOption.No;
			model.LocalAuthorityReorganisation = SelectOption.No;
			model.LocalAuthorityClosurePlans = SelectOption.No;
			model.LinkedToDiocese = SelectOption.No;
			model.PartOfFederation = SelectOption.No;
			model.SupportedByFoundationTrustOrBody = SelectOption.No;
			model.ExemptionFromSACRE = SelectOption.No;
			model.MainFeederSchools = "Feeder";
			model.EqualityAssessment = SelectOption.No;
			model.FurtherInformation = SelectOption.No;
			model.DioceseFileNames = new List<string>();
			model.FoundationConsentFileNames = new List<string>();
			model.ResolutionConsentFileNames = new List<string>();
			model.DioceseFiles = new List<IFormFile>();
			model.FoundationConsentFiles = new List<IFormFile> { new Mock<IFormFile>().Object };
			model.ResolutionConsentFiles = new List<IFormFile>();
			TempDataHelper.StoreSerialisedValue($"{entityId}-dioceseFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-foundationConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-resolutionConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new ConversionApplication());

			var result = await model.OnPostAsync();

			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(model.ModelState.ContainsKey(nameof(AdditionalDetails.FoundationConsentFileGenericError)), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenUploadFilesFails_ResolutionFile_ReturnsPageAndAddsError()
		{
			var entityId = System.Guid.NewGuid();
			var application = new ConversionApplication
			{
				ApplicationId = 1,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("School", 100, null) { id = 1, EntityId = entityId }
				}
			};
			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(1)).ReturnsAsync(application);

			var formMock = new Mock<IFormCollection>();
			formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.DioceseFilePrefixFieldName, It.IsAny<IFormFile>()))
				.ReturnsAsync("diocese-file-id");
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.FoundationConsentFilePrefixFieldName, It.IsAny<IFormFile>()))
				.ReturnsAsync("foundation-file-id");
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), FileUploadConstants.ResolutionConsentfilePrefixFieldName, It.IsAny<IFormFile>()))
				.ThrowsAsync(new FileUploadException("Upload failed"));

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				fileUploadMock.Object);
			model.ApplicationId = 1;
			model.Urn = 100;
			model.EntityId = entityId;
			model.ApplicationReference = "APP-REF";
			model.ExemptionEndDateName = "ExemptionEndDate";
			model.Request.Form = formMock.Object;
			model.TrustBenefitDetails = "Benefit";
			model.OfstedInspected = SelectOption.No;
			model.LocalAuthorityReorganisation = SelectOption.No;
			model.LocalAuthorityClosurePlans = SelectOption.No;
			model.LinkedToDiocese = SelectOption.No;
			model.PartOfFederation = SelectOption.No;
			model.SupportedByFoundationTrustOrBody = SelectOption.No;
			model.ExemptionFromSACRE = SelectOption.No;
			model.MainFeederSchools = "Feeder";
			model.EqualityAssessment = SelectOption.No;
			model.FurtherInformation = SelectOption.No;
			model.DioceseFileNames = new List<string>();
			model.FoundationConsentFileNames = new List<string>();
			model.ResolutionConsentFileNames = new List<string>();
			model.DioceseFiles = new List<IFormFile>();
			model.FoundationConsentFiles = new List<IFormFile>();
			model.ResolutionConsentFiles = new List<IFormFile> { new Mock<IFormFile>().Object };
			TempDataHelper.StoreSerialisedValue($"{entityId}-dioceseFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-foundationConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-resolutionConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new ConversionApplication());

			var result = await model.OnPostAsync();

			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(model.ModelState.ContainsKey(nameof(AdditionalDetails.ResolutionConsentFileGenericError)), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenValid_SucceedsAndRedirects()
		{
			var entityId = System.Guid.NewGuid();
			var application = new ConversionApplication
			{
				ApplicationId = 1,
				ApplicationReference = "APP-REF",
				Schools = new List<SchoolApplyingToConvert>
				{
					new SchoolApplyingToConvert("School", 100, null) { id = 1, EntityId = entityId }
				}
			};
			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(1)).ReturnsAsync(application);

			var formMock = new Mock<IFormCollection>();
			formMock.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);

			var fileUploadMock = new Mock<IFileUploadService>();
			fileUploadMock.Setup(x => x.UploadFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IFormFile>()))
				.ReturnsAsync("file-id");

			var conversionAppServiceMock = new Mock<IConversionApplicationService>();
			conversionAppServiceMock.Setup(x => x.SetAdditionalDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<bool>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<bool>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<SchoolEqualitiesProtectedCharacteristics?>(), It.IsAny<string?>()))
				.Returns(Task.CompletedTask);

			var model = SetupAdditionalDetails(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				conversionAppServiceMock.Object,
				fileUploadMock.Object);
			model.ApplicationId = 1;
			model.Urn = 100;
			model.EntityId = entityId;
			model.ApplicationReference = "APP-REF";
			model.ExemptionEndDateName = "ExemptionEndDate";
			model.Request.Form = formMock.Object;
			model.TrustBenefitDetails = "Benefit";
			model.OfstedInspected = SelectOption.No;
			model.LocalAuthorityReorganisation = SelectOption.No;
			model.LocalAuthorityClosurePlans = SelectOption.No;
			model.LinkedToDiocese = SelectOption.No;
			model.PartOfFederation = SelectOption.No;
			model.SupportedByFoundationTrustOrBody = SelectOption.No;
			model.ExemptionFromSACRE = SelectOption.No;
			model.MainFeederSchools = "Feeder";
			model.EqualityAssessment = SelectOption.No;
			model.FurtherInformation = SelectOption.No;
			model.DioceseFileNames = new List<string>();
			model.FoundationConsentFileNames = new List<string>();
			model.ResolutionConsentFileNames = new List<string>();
			model.DioceseFiles = new List<IFormFile>();
			model.FoundationConsentFiles = new List<IFormFile>();
			model.ResolutionConsentFiles = new List<IFormFile>();
			TempDataHelper.StoreSerialisedValue($"{entityId}-dioceseFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-foundationConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue($"{entityId}-resolutionConsentFiles", model.TempData, new List<string>());
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, model.TempData, new ConversionApplication());

			var result = await model.OnPostAsync();

			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.PageName, Is.EqualTo("FurtherInformationSummary"));
			conversionAppServiceMock.Verify(x => x.SetAdditionalDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<bool>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<bool>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<DateTimeOffset?>(), It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<SchoolEqualitiesProtectedCharacteristics?>(), It.IsAny<string?>()), Times.Once);
		}

		[Test]
		public void PopulateUiModel_WhenSectionStartedWithExemptionEndDate_SetsExemptionFromSACREYes()
		{
			var exemptionDate = new DateTimeOffset(2025, 6, 1, 0, 0, 0, TimeSpan.Zero);
			var school = new SchoolApplyingToConvert("School", 300, null)
			{
				TrustBenefitDetails = "Started",
				ExemptionEndDate = exemptionDate
			};

			var model = SetupAdditionalDetails(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				Mock.Of<IFileUploadService>());

			model.PopulateUiModel(school);

			Assert.That(model.ExemptionFromSACRE, Is.EqualTo(SelectOption.Yes));
			Assert.That(model.ExemptionEndDate, Is.EqualTo(exemptionDate));
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
