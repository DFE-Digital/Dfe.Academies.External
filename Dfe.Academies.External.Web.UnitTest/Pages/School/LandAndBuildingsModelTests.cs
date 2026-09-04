using System;
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
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class LandAndBuildingsModelTests
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
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		int urn = 101934;
		int applicationId = int.MaxValue;

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		ClassicAssert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedExplainedNotEntered___SchoolBuildLandWorksPlannedErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.SchoolBuildLandWorksPlannedError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedDateNotEntered___SchoolBuildLandWorksPlannedDateErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.SchoolBuildLandWorksPlannedDateError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandSharedFacilitiesExplainedNotEntered___SchoolBuildLandSharedFacilitiesExplainedErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.SchoolBuildLandSharedFacilitiesExplainedError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandGrantsBodiesNotEntered___SchoolBuildLandGrantsBodiesErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.SchoolBuildLandGrantsBodiesError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandPFISchemeTypeNotEntered___SchoolBuildLandPFISchemeTypeErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.SchoolBuildLandPFISchemeTypeError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedExplainedNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandWorksPlannedDateNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandWorksPlannedDateNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandSharedFacilitiesExplainedNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandSharedFacilitiesExplainedNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandGrantsBodiesNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandGrantsBodiesNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task ModelState___SchoolBuildLandPFISchemeTypeNotEntered___HasErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";
		var mockForm = new Mock<IFormCollection>();

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		pageModel.Request.Form = mockForm.Object;
		pageModel.ModelState.AddModelError("SchoolBuildLandPFISchemeTypeNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		Dictionary<string, IEnumerable<string>?> errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		ClassicAssert.AreEqual(1, errors.Count);
		ClassicAssert.AreEqual(true, pageModel.HasError);
	}

	[Test]
	public async Task OnPostAsync_WhenWorksPlannedYesButNoDetails_ReturnsPageWithValidationError()
	{
		// Arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { id = 1 }
		};
		
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(It.IsAny<int>()))
			.ReturnsAsync(application);

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		var mockForm = new Mock<IFormCollection>();
		pageModel.Request.Form = mockForm.Object;
		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		
		// Set up scenario where works are planned but no explanation given
		pageModel.SchoolBuildLandOwnerExplained = "Test owner";
		pageModel.SchoolBuildLandWorksPlanned = SelectOption.Yes;
		pageModel.SchoolBuildLandWorksPlannedExplained = ""; // Empty - should cause validation error
		pageModel.SchoolBuildLandSharedFacilities = SelectOption.No;
		pageModel.SchoolBuildLandGrants = SelectOption.No;
		pageModel.SchoolBuildLandPFIScheme = SelectOption.No;
		pageModel.SchoolBuildLandPriorityBuildingProgramme = SelectOption.No;
		pageModel.SchoolBuildLandFutureProgramme = SelectOption.No;

		// Act
		var result = await pageModel.OnPostAsync();

		// Assert
		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.HasError, Is.True);
		Assert.That(pageModel.SchoolBuildLandWorksPlannedError, Is.True);
	}

	[Test]
	public async Task OnPostAsync_WhenAllValidationPasses_RedirectsToNextPage()
	{
		// Arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { id = 1 }
		};
		
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(It.IsAny<int>()))
			.ReturnsAsync(application);

		mockConversionApplicationCreationService.Setup(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()))
			.Returns(Task.CompletedTask);

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		SetupValidLandAndBuildingsModel(pageModel);

		// Act
		var result = await pageModel.OnPostAsync();

		// Assert
		Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
		
		var redirect = (RedirectToPageResult)result;
		Assert.That(redirect.RouteValues["urn"], Is.EqualTo(100));
		Assert.That(redirect.RouteValues["appId"], Is.EqualTo(1));
		
		// Verify the service was called to update the school details
		mockConversionApplicationCreationService.Verify(x => x.PutSchoolApplicationDetails(1, 100, It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
	}

	[Test]
	public async Task OnPostAsync_WhenSharedFacilitiesYesButNoExplanation_ReturnsPageWithValidationError()
	{
		// Arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		
		var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		application.Schools = new List<SchoolApplyingToConvert>
		{
			new("Test School", 100, null) { id = 1 }
		};
		
		mockConversionApplicationRetrievalService.Setup(x => x.GetApplication(It.IsAny<int>()))
			.ReturnsAsync(application);

		var pageModel = SetupLandAndBuildingsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);

		var mockForm = new Mock<IFormCollection>();
		pageModel.Request.Form = mockForm.Object;
		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		
		// Set up scenario where shared facilities is Yes but no explanation given
		pageModel.SchoolBuildLandOwnerExplained = "Test owner";
		pageModel.SchoolBuildLandWorksPlanned = SelectOption.No;
		pageModel.SchoolBuildLandSharedFacilities = SelectOption.Yes;
		pageModel.SchoolBuildLandSharedFacilitiesExplained = ""; // Empty - should cause validation error
		pageModel.SchoolBuildLandGrants = SelectOption.No;
		pageModel.SchoolBuildLandPFIScheme = SelectOption.No;
		pageModel.SchoolBuildLandPriorityBuildingProgramme = SelectOption.No;
		pageModel.SchoolBuildLandFutureProgramme = SelectOption.No;

		// Act
		var result = await pageModel.OnPostAsync();

		// Assert
		Assert.That(result, Is.InstanceOf<PageResult>());
		Assert.That(pageModel.HasError, Is.True);
		Assert.That(pageModel.SchoolBuildLandSharedFacilitiesExplainedError, Is.True);
	}

	private static void SetupValidLandAndBuildingsModel(LandAndBuildingsModel pageModel)
	{
		var mockForm = new Mock<IFormCollection>();
		// Setup date form values (not needed for basic validation)
		mockForm.Setup(x => x.TryGetValue(It.IsAny<string>(), out It.Ref<StringValues>.IsAny!)).Returns(false);
		
		pageModel.Request.Form = mockForm.Object;
		pageModel.ApplicationId = 1;
		pageModel.Urn = 100;
		pageModel.PlannedDateFormInputName = "sip_lbworksplanneddate";
		
		// Set all required fields
		pageModel.SchoolBuildLandOwnerExplained = "School owns the land and buildings";
		pageModel.SchoolBuildLandWorksPlanned = SelectOption.No;
		pageModel.SchoolBuildLandSharedFacilities = SelectOption.No;
		pageModel.SchoolBuildLandGrants = SelectOption.No;
		pageModel.SchoolBuildLandPFIScheme = SelectOption.No;
		pageModel.SchoolBuildLandPriorityBuildingProgramme = SelectOption.No;
		pageModel.SchoolBuildLandFutureProgramme = SelectOption.No;
		
		// Set up TempData
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, new ConversionApplication());
	}

	private static LandAndBuildingsModel SetupLandAndBuildingsModel(
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new LandAndBuildingsModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasLandAndBuildingsData_PopulatesModel()
	{
		// Arrange
		var pageModel = SetupLandAndBuildingsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var worksPlannedDate = new DateTime(2025, 9, 15);
		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			LandAndBuildings = new SchoolLandAndBuildings(
				OwnerExplained: "Local Authority owns the land",
				WorksPlanned: true,
				WorksPlannedExplained: "New science block construction",
				WorksPlannedDate: worksPlannedDate,
				FacilitiesShared: false,
				FacilitiesSharedExplained: "No shared facilities",
				Grants: true,
				GrantsAwardingBodies: "DfE Capital Grant",
				PartOfPFIScheme: false,
				PartOfPFISchemeType: "Not applicable",
				PartOfPrioritySchoolsBuildingProgramme: false,
				PartOfBuildingSchoolsForFutureProgramme: true
			)
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolBuildLandOwnerExplained, Is.EqualTo("Local Authority owns the land"));
		Assert.That(pageModel.SchoolBuildLandWorksPlanned, Is.EqualTo(SelectOption.Yes));
		Assert.That(pageModel.SchoolBuildLandWorksPlannedExplained, Is.EqualTo("New science block construction"));
		Assert.That(pageModel.WorksPlannedDate, Is.EqualTo("15/09/2025"));
		Assert.That(pageModel.SchoolBuildLandSharedFacilities, Is.EqualTo(SelectOption.No));
		Assert.That(pageModel.SchoolBuildLandSharedFacilitiesExplained, Is.EqualTo("No shared facilities"));
		Assert.That(pageModel.SchoolBuildLandGrants, Is.EqualTo(SelectOption.Yes));
		Assert.That(pageModel.SchoolBuildLandGrantsBodies, Is.EqualTo("DfE Capital Grant"));
		Assert.That(pageModel.SchoolBuildLandPFIScheme, Is.EqualTo(SelectOption.No));
		Assert.That(pageModel.SchoolBuildLandPFISchemeType, Is.EqualTo("Not applicable"));
		Assert.That(pageModel.SchoolBuildLandPriorityBuildingProgramme, Is.EqualTo(SelectOption.No));
		Assert.That(pageModel.SchoolBuildLandFutureProgramme, Is.EqualTo(SelectOption.Yes));
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasEmptyLandAndBuildingsData_SetsDefaults()
	{
		// Arrange
		var pageModel = SetupLandAndBuildingsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		var school = new SchoolApplyingToConvert("Test School", 200, null)
		{
			LandAndBuildings = new SchoolLandAndBuildings(
				OwnerExplained: null,
				WorksPlanned: null,
				WorksPlannedExplained: null,
				WorksPlannedDate: null,
				FacilitiesShared: null,
				FacilitiesSharedExplained: null,
				Grants: null,
				GrantsAwardingBodies: null,
				PartOfPFIScheme: null,
				PartOfPFISchemeType: null,
				PartOfPrioritySchoolsBuildingProgramme: null,
				PartOfBuildingSchoolsForFutureProgramme: null
			)
		};

		// Act
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.SchoolBuildLandOwnerExplained, Is.EqualTo(string.Empty));
		Assert.That(pageModel.SchoolBuildLandWorksPlanned, Is.Null);
		Assert.That(pageModel.SchoolBuildLandWorksPlannedExplained, Is.Null);
		Assert.That(pageModel.WorksPlannedDate, Is.EqualTo(string.Empty));
		Assert.That(pageModel.SchoolBuildLandSharedFacilities, Is.Null);
		Assert.That(pageModel.SchoolBuildLandSharedFacilitiesExplained, Is.Null);
		Assert.That(pageModel.SchoolBuildLandGrants, Is.Null);
		Assert.That(pageModel.SchoolBuildLandGrantsBodies, Is.Null);
		Assert.That(pageModel.SchoolBuildLandPFIScheme, Is.Null);
		Assert.That(pageModel.SchoolBuildLandPFISchemeType, Is.Null);
		Assert.That(pageModel.SchoolBuildLandPriorityBuildingProgramme, Is.Null);
		Assert.That(pageModel.SchoolBuildLandFutureProgramme, Is.Null);
	}
}
