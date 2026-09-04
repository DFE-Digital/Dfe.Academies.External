using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class LeasesModelTests
	{
		[Test]
		public async Task OnGetAsync_WhenUserHasAccess_ReturnsPageResult()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const string testUserEmail = "test.user@test.com";
			
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			// Set a known contributor email that matches our test user
			application.Contributors.First().EmailAddress = testUserEmail;
			application.Schools = new List<SchoolApplyingToConvert>
			{
				new("Test School", urn, null)
				{
					id = 1,
					HasLeases = true,
					Leases = new List<SchoolLease>
					{
						new(1, "5 years", 5000, 3.5m, 10000, "Equipment lease", "25000", "School")
					}
				}
			};

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var pageModel = SetupLeasesModelWithEmail(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				testUserEmail);

			// Store application in TempData for permission check
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, application);

			// Act
			var result = await pageModel.OnGetAsync(urn, appId);


			// Assert
			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(pageModel.ApplicationId, Is.EqualTo(appId));
			Assert.That(pageModel.Urn, Is.EqualTo(urn));
			Assert.That(pageModel.HasLeases, Is.True);
			Assert.That(pageModel.LeaseViewModels, Has.Count.EqualTo(1));
			Assert.That(pageModel.LeaseViewModels.First().LeaseTerm, Is.EqualTo("5 years"));
			Assert.That(pageModel.LeaseViewModels.First().Purpose, Is.EqualTo("Equipment lease"));
			Assert.That(pageModel.AnyLeases, Is.EqualTo(SelectOption.Yes));
		}
		
		[Test]
		public async Task OnGetAsync_WhenSchoolHasNoLeases_SetsCorrectProperties()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const string testUserEmail = "test.user@test.com";
			
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			// Set a known contributor email that matches our test user
			application.Contributors.First().EmailAddress = testUserEmail;
			application.Schools = new List<SchoolApplyingToConvert>
			{
				new("Test School", urn, null)
				{
					id = 1,
					HasLeases = false,
					Leases = new List<SchoolLease>()
				}
			};

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var pageModel = SetupLeasesModelWithEmail(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>(),
				testUserEmail);

			// Store application in TempData for permission check
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, pageModel.TempData, application);

			// Act
			var result = await pageModel.OnGetAsync(urn, appId);


			// Assert
			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(pageModel.HasLeases, Is.False);
			Assert.That(pageModel.LeaseViewModels, Is.Empty);
			Assert.That(pageModel.AnyLeases, Is.EqualTo(SelectOption.No));
		}

		[Test]
		public async Task OnPostAsync_WhenValidationFails_ReturnsPageResult()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			application.Schools = new List<SchoolApplyingToConvert>
			{
				new("Test School", urn, null) { id = 1, HasLeases = false, Leases = new List<SchoolLease>() }
			};

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var pageModel = SetupLeasesModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLeases = SelectOption.Yes; // Set to Yes but no leases added - should fail validation

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(pageModel.ModelState.ContainsKey("AddedLeasesButEmptyCollectionError"), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenAnyLeasesIsNo_DeletesAllLeasesAndRedirects()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const int schoolId = 1;
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			var school = new SchoolApplyingToConvert("Test School", urn, null)
			{
				id = schoolId,
				HasLeases = true,
				Leases = new List<SchoolLease>
				{
					new(1, "3 years", 3000, 2.5m, 5000, "Equipment A", "15000", "School"),
					new(2, "5 years", 4000, 3.0m, 8000, "Equipment B", "20000", "Trust")
				}
			};
			application.Schools = new List<SchoolApplyingToConvert> { school };

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var conversionAppServiceMock = new Mock<IConversionApplicationService>();
			conversionAppServiceMock.Setup(x => x.DeleteLease(appId, schoolId, It.IsAny<int>()))
				.Returns(Task.CompletedTask);
			conversionAppServiceMock.Setup(x => x.PutSchoolApplicationDetails(appId, urn, It.IsAny<Dictionary<string, dynamic>>()))
				.Returns(Task.CompletedTask);

			var pageModel = SetupLeasesModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				conversionAppServiceMock.Object);

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLeases = SelectOption.No;

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			
			// Verify all leases were deleted
			conversionAppServiceMock.Verify(x => x.DeleteLease(appId, schoolId, 1), Times.Once);
			conversionAppServiceMock.Verify(x => x.DeleteLease(appId, schoolId, 2), Times.Once);
			
			// Verify school details were updated
			conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(appId, urn, It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
				
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.RouteValues["urn"], Is.EqualTo(urn));
			Assert.That(redirect.RouteValues["appId"], Is.EqualTo(appId));
		}

		[Test]
		public async Task OnPostAsync_WhenAnyLeasesIsYesWithValidLeases_RedirectsWithoutDeletion()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const int schoolId = 1;
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			var school = new SchoolApplyingToConvert("Test School", urn, null)
			{
				id = schoolId,
				HasLeases = true,
				Leases = new List<SchoolLease>
				{
					new(1, "3 years", 3000, 2.5m, 5000, "Equipment A", "15000", "School")
				}
			};
			application.Schools = new List<SchoolApplyingToConvert> { school };

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var pageModel = SetupLeasesModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				conversionAppServiceMock.Object);

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLeases = SelectOption.Yes;

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			
			// Verify no leases were deleted
			conversionAppServiceMock.Verify(x => x.DeleteLease(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
			
			// Verify no school details update was called
			conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()), Times.Never);
				
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.RouteValues["urn"], Is.EqualTo(urn));
			Assert.That(redirect.RouteValues["appId"], Is.EqualTo(appId));
		}

		[Test]
		public void RunUiValidation_WhenAnyLeasesYesButNoLeasesAdded_ReturnsFalseWithError()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLeases = SelectOption.Yes;
			pageModel.LeaseViewModels = new List<LeaseViewModel>(); // Empty list
			pageModel.ModelState.Clear();

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.False);
			Assert.That(pageModel.ModelState.ContainsKey("AddedLeasesButEmptyCollectionError"), Is.True);
		}

		[Test]
		public void RunUiValidation_WhenAnyLeasesYesAndLeasesExist_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLeases = SelectOption.Yes;
			pageModel.LeaseViewModels = new List<LeaseViewModel>
			{
				new() { Id = 1, Purpose = "Equipment A" }
			};
			pageModel.ModelState.Clear();

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void RunUiValidation_WhenAnyLeasesNoAndNoLeases_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLeases = SelectOption.No;
			pageModel.LeaseViewModels = new List<LeaseViewModel>();
			pageModel.ModelState.Clear();

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void RunUiValidation_WhenModelStateInvalid_ReturnsFalse()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ModelState.AddModelError("AnyLeases", "This field is required");

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void PopulateUpdateDictionary_WhenAnyLeasesIsYes_ReturnsCorrectDictionary()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLeases = SelectOption.Yes;

			// Act
			var result = pageModel.PopulateUpdateDictionary();

			// Assert
			Assert.That(result, Contains.Key("HasLeases"));
			Assert.That(result["HasLeases"], Is.EqualTo(true));
		}

		[Test]
		public void PopulateUpdateDictionary_WhenAnyLeasesIsNo_ReturnsCorrectDictionary()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLeases = SelectOption.No;

			// Act
			var result = pageModel.PopulateUpdateDictionary();

			// Assert
			Assert.That(result, Contains.Key("HasLeases"));
			Assert.That(result["HasLeases"], Is.EqualTo(false));
		}

	[Test]
	public void PopulateUiModel_WhenSchoolHasLeases_SetsAnyLeasesToYes()
	{
		// Arrange
		var pageModel = SetupLeasesModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			HasLeases = true,
			Leases = new List<SchoolLease>
			{
				new(1, "3 years", 3000, 2.5m, 5000, "Equipment A", "15000", "School")
			}
		};

		// Act - Call both methods to simulate the full flow
		var loadMethod = typeof(Leases).GetMethod("LoadLeasesFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		loadMethod?.Invoke(pageModel, new object[] { school });
		
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.AnyLeases, Is.EqualTo(SelectOption.Yes));
		Assert.That(pageModel.HasLeases, Is.True);
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoLeases_SetsAnyLeasesToNo()
	{
		// Arrange
		var pageModel = SetupLeasesModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			HasLeases = false,
			Leases = new List<SchoolLease>()
		};

		// Act - Call both methods to simulate the full flow
		var loadMethod = typeof(Leases).GetMethod("LoadLeasesFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		loadMethod?.Invoke(pageModel, new object[] { school });
		
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.AnyLeases, Is.EqualTo(SelectOption.No));
		Assert.That(pageModel.HasLeases, Is.False);
	}

		[Test]
		public void HasError_WhenAddedLeasesButEmptyCollectionError_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ModelState.AddModelError("AddedLeasesButEmptyCollectionError", "Error");

			// Act & Assert
			Assert.That(pageModel.HasError, Is.True);
			Assert.That(pageModel.AddedLeasesButEmptyCollectionError, Is.True);
		}

		[Test]
		public void HasError_WhenInvalidSelectOptionError_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ModelState.AddModelError("InvalidSelectOptionError", "Error");

			// Act & Assert
			Assert.That(pageModel.HasError, Is.True);
			Assert.That(pageModel.InvalidSelectOptionError, Is.True);
		}

		[Test]
		public void HasError_WhenNoErrors_ReturnsFalse()
		{
			// Arrange
			var pageModel = SetupLeasesModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			// Act & Assert
			Assert.That(pageModel.HasError, Is.False);
		}

		private static Leases SetupLeasesModel(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationService,
			bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new Leases(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationService)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}

		private static Leases SetupLeasesModelWithEmail(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationService,
			string userEmail)
		{
			// Create custom PageContext with email claim
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, "Test User"),
				new Claim(ClaimTypes.NameIdentifier, "1"),
				new Claim(ClaimTypes.Email, userEmail)
			};
			var identity = new ClaimsIdentity(claims, "test");
			var principal = new ClaimsPrincipal(identity);

			var httpContext = new DefaultHttpContext
			{
				User = principal
			};

			var modelState = new ModelStateDictionary();
			var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
			var modelMetadataProvider = new EmptyModelMetadataProvider();
			var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
			var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
			var pageContext = new PageContext(actionContext) { ViewData = viewData };

			return new Leases(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationService)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}
