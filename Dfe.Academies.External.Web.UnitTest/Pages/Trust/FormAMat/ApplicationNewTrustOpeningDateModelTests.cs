using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoFixture;
using Dfe.Academies.External.Web.Pages.Trust.FormAMat;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages.Trust.FormAMat;

[Parallelizable(ParallelScope.All)]
internal sealed class ApplicationNewTrustOpeningDateModelTests
{
	private static readonly Fixture Fixture = new();

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
		int applicationId = Fixture.Create<int>();

		var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

		// act
		var pageModel = SetupApplicationNewTrustOpeningDateModel(mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object);
		TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

		// act
		await pageModel.OnGetAsync(applicationId);

		// assert
		Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
	}

	private static ApplicationNewTrustOpeningDateModel SetupApplicationNewTrustOpeningDateModel(
		IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
		IReferenceDataRetrievalService mockReferenceDataRetrievalService,
		IConversionApplicationService mockConversionApplicationCreationService,
		bool isAuthenticated = false)
	{
		(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

		return new ApplicationNewTrustOpeningDateModel(mockConversionApplicationRetrievalService, mockReferenceDataRetrievalService,
			mockConversionApplicationCreationService)
		{
			PageContext = pageContext,
			TempData = tempData,
			Url = new UrlHelper(actionContext),
			MetadataProvider = pageContext.ViewData.ModelMetadata
		};
	}

	[TestCase("John", "john@example.com")]
	[TestCase("Mary Jane", "mary.jane@example.com")]
	[TestCase("Jean-Luc Picard", "jean-luc.picard@starfleet.com")]
	[TestCase("O'Connor", "oconnor@example.com")]
	[TestCase("Élodie", "elodie@example.fr")]
	[TestCase("Anne-Marie O'Neill", "anne-marie.oneill@example.com")]
	public void TrustApprover_ValidNamesAndEmails_ShouldPass(string validName, string validEmail)
	{
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

		// Act
		var pageModel = SetupApplicationNewTrustOpeningDateModel(
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object
		);

		pageModel.TrustApproverName = validName;
		pageModel.TrustApproverEmail = validEmail;

		var results = ValidateModel(pageModel);

		// Assert
		Assert.That(results, Is.Empty, $"Expected no validation errors for Name: {validName}, Email: {validEmail}");
	}
	 
	[TestCase(null, "john@example.com")]           // Name missing
	[TestCase("", "john@example.com")]             // Name empty
	[TestCase("john", "john@example.com")]        // Name lowercase start
	[TestCase(" Mary", "mary@example.com")]       // Name leading space
	[TestCase("John3", "john3@example.com")]      // Name number
	[TestCase("Jean--Luc", "")]   // Name double hyphen
	[TestCase("O''Connor", "invalid-email")]// Name double apostrophe
	[TestCase("John", null)]                      // Email missing
	[TestCase("Mary Jane", "")]                   // Email empty
	[TestCase("Jean-Luc Picard", "invalid-email")]// Email invalid
	public void TrustApprover_InvalidNamesOrEmails_ShouldFail(string name, string email)
	{
		var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
		var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
		var mockConversionApplicationCreationService = new Mock<IConversionApplicationService>();

		// Act
		var pageModel = SetupApplicationNewTrustOpeningDateModel(
			mockConversionApplicationRetrievalService.Object,
			mockReferenceDataRetrievalService.Object,
			mockConversionApplicationCreationService.Object
		);

		pageModel.TrustApproverName = name;
		pageModel.TrustApproverEmail = email;

		var results = ValidateModel(pageModel);

		// Assert
		Assert.That(results, Is.Not.Empty, $"Expected validation errors for Name: {name}, Email: {email}");
		Assert.That(results, Has.Some.Matches<ValidationResult>(r =>
			r.ErrorMessage == "You must input a valid name" ||
			r.ErrorMessage == "Name is required" ||
			r.ErrorMessage == "You must input a valid email address" ||
			r.ErrorMessage == "Email address is required"
		));
	}
	private static List<ValidationResult> ValidateModel(object model)
	{
		var results = new List<ValidationResult>();
		var context = new ValidationContext(model, null, null);
		Validator.TryValidateObject(model, context, results, true);
		return results;
	}
}
