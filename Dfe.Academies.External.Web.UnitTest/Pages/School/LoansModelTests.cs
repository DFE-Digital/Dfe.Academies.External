using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
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

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	[Parallelizable(ParallelScope.All)]
	internal sealed class LoansModelTests
	{
	[Test]
	public void LoadLoansFromDatabase_WhenSchoolHasLoans_PopulatesViewModels()
	{
		// Arrange
		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			id = 1,
			HasLoans = true,
			Loans = new List<SchoolLoan>
			{
				new(1, 10000, "Equipment", "Bank A", 5.5m, "Monthly")
			}
		};

		var pageModel = SetupLoansModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		// Act - Use reflection to call the private method or make it internal in the actual code
		var method = typeof(Loans).GetMethod("LoadLoansFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		method?.Invoke(pageModel, new object[] { school });

		// Assert
		Assert.That(pageModel.HasLoans, Is.True);
		Assert.That(pageModel.LoanViewModels, Has.Count.EqualTo(1));
		Assert.That(pageModel.LoanViewModels.First().Provider, Is.EqualTo("Bank A"));
	}

	[Test]
	public async Task OnGetAsync_WhenApplicationNotFound_HandlesGracefully()
	{
		// Arrange
		const int appId = 5;
		const int urn = 100;

		var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
		retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync((ConversionApplication)null);

		var pageModel = SetupLoansModel(
			retrievalMock.Object,
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		// Act - This should handle null gracefully 
		var result = await pageModel.OnGetAsync(urn, appId);

		// Assert - Should either redirect or return page, not throw
		Assert.That(result, Is.Not.Null);
	}

	[Test]
	public void LoadLoansFromDatabase_WhenSchoolHasNoLoans_SetsCorrectProperties()
	{
		// Arrange
		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			id = 1,
			HasLoans = false,
			Loans = new List<SchoolLoan>()
		};

		var pageModel = SetupLoansModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		// Act - Use reflection to call the private method
		var method = typeof(Loans).GetMethod("LoadLoansFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		method?.Invoke(pageModel, new object[] { school });

		// Assert
		Assert.That(pageModel.HasLoans, Is.False);
		Assert.That(pageModel.LoanViewModels, Is.Empty);
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
				new("Test School", urn, null) { id = 1, HasLoans = false, Loans = new List<SchoolLoan>() }
			};

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var pageModel = SetupLoansModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLoans = SelectOption.Yes; // Set to Yes but no loans added - should fail validation

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<PageResult>());
			Assert.That(pageModel.ModelState.ContainsKey("AddedLoansButEmptyCollectionError"), Is.True);
		}

		[Test]
		public async Task OnPostAsync_WhenAnyLoansIsNo_DeletesAllLoansAndRedirects()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const int schoolId = 1;
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			var school = new SchoolApplyingToConvert("Test School", urn, null)
			{
				id = schoolId,
				HasLoans = true,
				Loans = new List<SchoolLoan>
				{
					new(1, 10000, "Equipment", "Bank A", 5.0m, "Monthly"),
					new(2, 15000, "Facilities", "Bank B", 4.5m, "Annual")
				}
			};
			application.Schools = new List<SchoolApplyingToConvert> { school };

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var conversionAppServiceMock = new Mock<IConversionApplicationService>();
			conversionAppServiceMock.Setup(x => x.DeleteLoan(appId, schoolId, It.IsAny<int>()))
				.Returns(Task.CompletedTask);
			conversionAppServiceMock.Setup(x => x.PutSchoolApplicationDetails(appId, urn, It.IsAny<Dictionary<string, dynamic>>()))
				.Returns(Task.CompletedTask);

			var pageModel = SetupLoansModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				conversionAppServiceMock.Object);

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLoans = SelectOption.No;

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			
			// Verify all loans were deleted
			conversionAppServiceMock.Verify(x => x.DeleteLoan(appId, schoolId, 1), Times.Once);
			conversionAppServiceMock.Verify(x => x.DeleteLoan(appId, schoolId, 2), Times.Once);
			
			// Verify school details were updated
			conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(appId, urn, It.IsAny<Dictionary<string, dynamic>>()), Times.Once);
				
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.RouteValues["urn"], Is.EqualTo(urn));
			Assert.That(redirect.RouteValues["appId"], Is.EqualTo(appId));
		}

		[Test]
		public async Task OnPostAsync_WhenAnyLoansIsYesWithValidLoans_RedirectsWithoutDeletion()
		{
			// Arrange
			const int appId = 5;
			const int urn = 100;
			const int schoolId = 1;
			var application = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();
			var school = new SchoolApplyingToConvert("Test School", urn, null)
			{
				id = schoolId,
				HasLoans = true,
				Loans = new List<SchoolLoan>
				{
					new(1, 10000, "Equipment", "Bank A", 5.0m, "Monthly")
				}
			};
			application.Schools = new List<SchoolApplyingToConvert> { school };

			var retrievalMock = new Mock<IConversionApplicationRetrievalService>();
			retrievalMock.Setup(x => x.GetApplication(appId)).ReturnsAsync(application);

			var conversionAppServiceMock = new Mock<IConversionApplicationService>();

			var pageModel = SetupLoansModel(
				retrievalMock.Object,
				Mock.Of<IReferenceDataRetrievalService>(),
				conversionAppServiceMock.Object);

			pageModel.ApplicationId = appId;
			pageModel.Urn = urn;
			pageModel.AnyLoans = SelectOption.Yes;

			// Act
			var result = await pageModel.OnPostAsync();

			// Assert
			Assert.That(result, Is.InstanceOf<RedirectToPageResult>());
			
			// Verify no loans were deleted
			conversionAppServiceMock.Verify(x => x.DeleteLoan(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
			
			// Verify no school details update was called
			conversionAppServiceMock.Verify(x => x.PutSchoolApplicationDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Dictionary<string, dynamic>>()), Times.Never);
				
			var redirect = (RedirectToPageResult)result;
			Assert.That(redirect.RouteValues["urn"], Is.EqualTo(urn));
			Assert.That(redirect.RouteValues["appId"], Is.EqualTo(appId));
		}

		[Test]
		public void RunUiValidation_WhenAnyLoansYesButNoLoansAdded_ReturnsFalseWithError()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLoans = SelectOption.Yes;
			pageModel.LoanViewModels = new List<LoanViewModel>(); // Empty list
			pageModel.ModelState.Clear();

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.False);
			Assert.That(pageModel.ModelState.ContainsKey("AddedLoansButEmptyCollectionError"), Is.True);
		}

		[Test]
		public void RunUiValidation_WhenAnyLoansYesAndLoansExist_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLoans = SelectOption.Yes;
			pageModel.LoanViewModels = new List<LoanViewModel>
			{
				new() { Id = 1, Provider = "Bank A" }
			};
			pageModel.ModelState.Clear();

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.True);
		}

		[Test]
		public void RunUiValidation_WhenAnyLoansNoAndNoLoans_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLoans = SelectOption.No;
			pageModel.LoanViewModels = new List<LoanViewModel>();
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
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ModelState.AddModelError("AnyLoans", "This field is required");

			// Act
			var result = pageModel.RunUiValidation();

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void PopulateUpdateDictionary_WhenAnyLoansIsYes_ReturnsCorrectDictionary()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLoans = SelectOption.Yes;

			// Act
			var result = pageModel.PopulateUpdateDictionary();

			// Assert
			Assert.That(result, Contains.Key("HasLoans"));
			Assert.That(result["HasLoans"], Is.EqualTo(true));
		}

		[Test]
		public void PopulateUpdateDictionary_WhenAnyLoansIsNo_ReturnsCorrectDictionary()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.AnyLoans = SelectOption.No;

			// Act
			var result = pageModel.PopulateUpdateDictionary();

			// Assert
			Assert.That(result, Contains.Key("HasLoans"));
			Assert.That(result["HasLoans"], Is.EqualTo(false));
		}

	[Test]
	public void PopulateUiModel_WhenSchoolHasLoans_SetsAnyLoansToYes()
	{
		// Arrange
		var pageModel = SetupLoansModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			HasLoans = true,
			Loans = new List<SchoolLoan>
			{
				new(1, 10000, "Equipment", "Bank A", 5.0m, "Monthly")
			}
		};

		// Act - Call both methods to simulate the full flow
		var loadMethod = typeof(Loans).GetMethod("LoadLoansFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		loadMethod?.Invoke(pageModel, new object[] { school });
		
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.AnyLoans, Is.EqualTo(SelectOption.Yes));
		Assert.That(pageModel.HasLoans, Is.True);
	}

	[Test]
	public void PopulateUiModel_WhenSchoolHasNoLoans_SetsAnyLoansToNo()
	{
		// Arrange
		var pageModel = SetupLoansModel(
			Mock.Of<IConversionApplicationRetrievalService>(),
			Mock.Of<IReferenceDataRetrievalService>(),
			Mock.Of<IConversionApplicationService>());

		var school = new SchoolApplyingToConvert("Test School", 100, null)
		{
			HasLoans = false,
			Loans = new List<SchoolLoan>()
		};

		// Act - Call both methods to simulate the full flow
		var loadMethod = typeof(Loans).GetMethod("LoadLoansFromDatabase", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		loadMethod?.Invoke(pageModel, new object[] { school });
		
		pageModel.PopulateUiModel(school);

		// Assert
		Assert.That(pageModel.AnyLoans, Is.EqualTo(SelectOption.No));
		Assert.That(pageModel.HasLoans, Is.False);
	}

		[Test]
		public void HasError_WhenAddedLoansButEmptyCollectionError_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			pageModel.ModelState.AddModelError("AddedLoansButEmptyCollectionError", "Error");

			// Act & Assert
			Assert.That(pageModel.HasError, Is.True);
			Assert.That(pageModel.AddedLoansButEmptyCollectionError, Is.True);
		}

		[Test]
		public void HasError_WhenInvalidSelectOptionError_ReturnsTrue()
		{
			// Arrange
			var pageModel = SetupLoansModel(
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
			var pageModel = SetupLoansModel(
				Mock.Of<IConversionApplicationRetrievalService>(),
				Mock.Of<IReferenceDataRetrievalService>(),
				Mock.Of<IConversionApplicationService>());

			// Act & Assert
			Assert.That(pageModel.HasError, Is.False);
		}

		private static Loans SetupLoansModel(
			IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService conversionApplicationService,
			bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new Loans(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationService)
			{
				PageContext = pageContext,
				TempData = tempData,
				Url = new UrlHelper(actionContext),
				MetadataProvider = pageContext.ViewData.ModelMetadata
			};
		}
	}
}