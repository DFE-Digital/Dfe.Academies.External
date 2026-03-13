using System.Collections.Generic;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolMainContactsModelTests
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
		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(urn, applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	[Test]
	public async Task ModelState___MainContactOtherNameNotEntered___OtherNameErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);

		pageModel.ModelState.AddModelError("MainContactOtherNameNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		var errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.That(errors.Count, Is.EqualTo(2));
		Assert.That(pageModel.OtherNameError, Is.True);
	}

	[Test]
	public async Task ModelState___MainContactOtherEmailNotEntered___OtherEmailErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);

		pageModel.ModelState.AddModelError("MainContactOtherEmailNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		var errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.That(errors.Count, Is.EqualTo(2));
		Assert.That(pageModel.OtherEmailError, Is.True);
	}

	[Test]
	public async Task ModelState___MainContactOtherTelephoneNotEntered___OtherTelephoneErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);

		pageModel.ModelState.AddModelError("MainContactOtherTelephoneNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		var errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.That(errors.Count, Is.EqualTo(3));
	}

	[Test]
	public async Task ModelState___MainContactOtherNameNotEntered___OtherContactErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);

		pageModel.ModelState.AddModelError("MainContactOtherNameNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		var errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.That(errors.Count, Is.EqualTo(2));
		Assert.That(pageModel.OtherContactError, Is.True);
	}

	[Test]
	public async Task ModelState___MainContactOtherEmailNotEntered___OtherContactErrorTrue()
	{
		// arrange
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var expectedErrorText = "You must provide details";

		var pageModel = SetupSchoolMainContactsModel(mockConversionApplicationCreationService.Object,
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object);
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);

		pageModel.ModelState.AddModelError("MainContactOtherEmailNotEntered", expectedErrorText);

		// act
		await pageModel.OnPostAsync();

		var errors = (Dictionary<string, IEnumerable<string>?>)pageModel.ViewData["Errors"]!;

		// assert
		Assert.That(errors.Count, Is.EqualTo(2));
		Assert.That(pageModel.OtherContactError, Is.True);
	}

	[Test]
	public void RunUiValidation_ModelStateInvalid_ReturnsFalse()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100);
		pageModel.ModelState.AddModelError("ContactHeadName", "Required");

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.False);
	}

	[Test]
	public void RunUiValidation_ContactRoleOtherAndMainContactOtherNameEmpty_AddsErrorAndReturnsFalse()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = null,
			MainContactOtherEmail = "other@school.com"
		};
		pageModel.ModelState.Clear();

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("MainContactOtherNameNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_ContactRoleOtherAndMainContactOtherEmailEmpty_AddsErrorAndReturnsFalse()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other Name",
			MainContactOtherEmail = null
		};
		pageModel.ModelState.Clear();

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("MainContactOtherEmailNotEntered"), Is.True);
	}

	[Test]
	public void RunUiValidation_ContactRoleOtherAndInvalidEmailFormat_AddsErrorAndReturnsFalse()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other Name",
			MainContactOtherEmail = "not-a-valid-email"
		};
		pageModel.ModelState.Clear();

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.False);
		Assert.That(pageModel.ModelState.ContainsKey("MainContactOtherEmailInvalid"), Is.True);
	}

	[Test]
	public void RunUiValidation_ContactRoleHeadTeacherAndValid_ReturnsTrue()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher
		};
		pageModel.ModelState.Clear();

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.True);
	}

	[Test]
	public void RunUiValidation_ContactRoleOtherAndAllValid_ReturnsTrue()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other Contact",
			MainContactOtherEmail = "other@school.com"
		};
		pageModel.ModelState.Clear();

		var result = pageModel.RunUiValidation();

		Assert.That(result, Is.True);
	}

	[Test]
	public void PopulateUpdateDictionary_ContactRoleNotOther_NullsMainContactOtherFieldsAndReturnsDictionary()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.HeadTeacher,
			MainContactOtherName = "Should be cleared",
			MainContactOtherEmail = "other@school.com",
			ApproverContactName = "Approver",
			ApproverContactEmail = "approver@school.com"
		};

		var result = pageModel.PopulateUpdateDictionary();

		Assert.That(pageModel.ViewModel.MainContactOtherName, Is.Null);
		Assert.That(pageModel.ViewModel.MainContactOtherEmail, Is.Null);
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionContactHeadName)], Is.EqualTo("Head"));
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionContactRole)], Is.EqualTo(MainConversionContact.HeadTeacher.ToString()));
	}

	[Test]
	public void PopulateUpdateDictionary_ContactRoleOther_ReturnsDictionaryWithMainContactOtherValues()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ViewModel = new ApplicationSchoolContactsViewModel(1, 100)
		{
			ContactHeadName = "Head",
			ContactHeadEmail = "head@school.com",
			ContactChairName = "Chair",
			ContactChairEmail = "chair@school.com",
			ContactRole = MainConversionContact.Other,
			MainContactOtherName = "Other Contact",
			MainContactOtherEmail = "other@school.com",
			ApproverContactName = "Approver",
			ApproverContactEmail = "approver@school.com"
		};

		var result = pageModel.PopulateUpdateDictionary();

		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherName)], Is.EqualTo("Other Contact"));
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionMainContactOtherEmail)], Is.EqualTo("other@school.com"));
		Assert.That(result[nameof(SchoolApplyingToConvert.SchoolConversionApproverContactName)], Is.EqualTo("Approver"));
	}

	[Test]
	public void OtherEmailInvalidError_WhenModelStateContainsKey_ReturnsTrue()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ModelState.AddModelError("MainContactOtherEmailInvalid", "Invalid email");

		Assert.That(pageModel.OtherEmailInvalidError, Is.True);
	} 

	[Test]
	public void OtherContactError_WhenOtherEmailInvalidError_ReturnsTrue()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		pageModel.ModelState.AddModelError("MainContactOtherEmailInvalid", "Invalid");

		Assert.That(pageModel.OtherContactError, Is.True);
	}

	[Test]
	public void SigninApproverQuestionText_ReturnsExpectedDefault()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());

		Assert.That(pageModel.SigninApproverQuestionText, Does.Contain("DfE sign-in account"));
	}

	[Test]
	public void PopulateUiModel_SingleParameter_ThrowsNotImplementedException()
	{
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>());
		var school = new SchoolApplyingToConvert("Test School", 100, null);

		Assert.That(() => pageModel.PopulateUiModel(school), Throws.TypeOf<System.NotImplementedException>());
	}

	[Test]
	public async Task OnGetAsync_GetApplicationReturnsNull_InitializesViewModelWithApplicationIdAndUrn()
	{
		const int appId = 42;
		const int urn = 100;
		var mockRetrieval = new Mock<IConversionApplicationRetrievalService>();
		mockRetrieval.Setup(x => x.GetApplication(appId)).ReturnsAsync((ConversionApplication?)null);
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			mockRetrieval.Object,
			Mock.Of<IReferenceDataRetrievalService>());
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole());

		await pageModel.OnGetAsync(urn, appId);

		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.ApplicationId, Is.EqualTo(appId));
		Assert.That(pageModel.ViewModel.Urn, Is.EqualTo(urn));
	}

	[Test]
	public async Task OnGetAsync_GetApplicationReturnsApplicationWithMatchingSchoolAndDraftInTempData_PopulatesViewModelFromSchool()
	{
		const int appId = 99;
		const int urn = 101934;
		var school = new SchoolApplyingToConvert("Test School", urn, null)
		{
			SchoolConversionContactHeadName = "Head Name",
			SchoolConversionContactHeadEmail = "head@test.school",
			SchoolConversionContactChairName = "Chair Name",
			SchoolConversionContactChairEmail = "chair@test.school",
			SchoolConversionContactRole = MainConversionContact.Other.ToString(),
			SchoolConversionMainContactOtherName = "Other Name",
			SchoolConversionMainContactOtherEmail = "other@test.school",
			SchoolConversionApproverContactName = "Approver",
			SchoolConversionApproverContactEmail = "approver@test.school"
		};
		var application = new ConversionApplication
		{
			ApplicationId = appId,
			ApplicationType = ApplicationTypes.JoinAMat,
			Schools = new List<SchoolApplyingToConvert> { school },
			Contributors = new List<ConversionApplicationContributor>
			{
				new ConversionApplicationContributor("Test", "User", "", SchoolRoles.ChairOfGovernors, null)
			}
		};
		var draftApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
		draftApplication.ApplicationId = appId;
		draftApplication.ApplicationType = ApplicationTypes.JoinAMat;

		var mockRetrieval = new Mock<IConversionApplicationRetrievalService>();
		mockRetrieval.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			mockRetrieval.Object,
			Mock.Of<IReferenceDataRetrievalService>());
		TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, draftApplication);

		await pageModel.OnGetAsync(urn, appId);

		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.ContactHeadName, Is.EqualTo("Head Name"));
		Assert.That(pageModel.ViewModel.ContactHeadEmail, Is.EqualTo("head@test.school"));
		Assert.That(pageModel.ViewModel.ContactChairName, Is.EqualTo("Chair Name"));
		Assert.That(pageModel.ViewModel.ContactChairEmail, Is.EqualTo("chair@test.school"));
		Assert.That(pageModel.ViewModel.ContactRole, Is.EqualTo(MainConversionContact.Other));
		Assert.That(pageModel.ViewModel.MainContactOtherName, Is.EqualTo("Other Name"));
		Assert.That(pageModel.ViewModel.MainContactOtherEmail, Is.EqualTo("other@test.school"));
		Assert.That(pageModel.ViewModel.ApproverContactName, Is.EqualTo("Approver"));
		Assert.That(pageModel.ViewModel.ApproverContactEmail, Is.EqualTo("approver@test.school"));
	}

	[Test]
	public async Task OnGetAsync_GetApplicationReturnsApplicationWithSchoolButNoDraftInTempData_InitializesEmptyViewModel()
	{
		const int appId = 88;
		const int urn = 200;
		var school = new SchoolApplyingToConvert("Another School", urn, null);
		var application = new ConversionApplication
		{
			ApplicationId = appId,
			ApplicationType = ApplicationTypes.JoinAMat,
			Schools = new List<SchoolApplyingToConvert> { school },
			Contributors = new List<ConversionApplicationContributor>
			{
				new ConversionApplicationContributor("Test", "User", "", SchoolRoles.ChairOfGovernors, null)
			}
		};

		var mockRetrieval = new Mock<IConversionApplicationRetrievalService>();
		mockRetrieval.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);
		var pageModel = SetupSchoolMainContactsModel(
			Mock.Of<IConversionApplicationService>(),
			mockRetrieval.Object,
			Mock.Of<IReferenceDataRetrievalService>());
		// No draft in TempData initially - CheckApplicationPermission will store application from GetApplication

		await pageModel.OnGetAsync(urn, appId);

		Assert.That(pageModel.ViewModel, Is.Not.Null);
		Assert.That(pageModel.ViewModel.ApplicationId, Is.EqualTo(appId));
		Assert.That(pageModel.ViewModel.Urn, Is.EqualTo(urn));
		Assert.That(pageModel.ViewModel.ContactHeadName, Is.Null);
	}

	// TODO :- OnPostAsync___ModelIsValid___Invalid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	// TODO :- OnPostAsync___ModelIsValid___Valid
	// when academisation API is implemented, will need to mock ResilientRequestProvider for http client API responses

	private static SchoolMainContactsModel SetupSchoolMainContactsModel(
		IConversionApplicationService mockConversionApplicationCreationService,
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new SchoolMainContactsModel(mockConversionApplicationRetrievalService,
			mockReferenceDataRetrievalService, mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}
}
