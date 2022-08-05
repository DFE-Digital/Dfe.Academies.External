using Dfe.Academies.External.Web.Controllers;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Controllers;

[Parallelizable(ParallelScope.All)]
internal sealed class SchoolControllerTests
{
    /// <summary>
    /// TODO MR:- re-test after PR55 - plumb in trams API
    /// </summary>
    //[Test]
    public async Task SchoolController___Search___ReturnsResult()
    {
        // arrange
        var mockLogger = new Mock<ILogger<SchoolController>>();
        var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
        var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object, mockConversionApplicationRetrievalService.Object);
        string schoolName = "wise";

        // act
        var result = await schoolController.Search(schoolName);

        //var viewModel = (result as ViewResult).Model;

        // assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.Zero);
        //Assert.That(viewModel, Is.Not.Null);
    }

    /// <summary>
    /// TODO MR:- re-test after PR55 - plumb in trams API
    /// </summary>
    //[Test]
    public async Task SchoolController___ReturnSchoolDetailsPartialViewPopulated___ReturnsPartialView()
    {
	    // arrange
	    var mockLogger = new Mock<ILogger<SchoolController>>();
	    var mockReferenceDataRetrievalService = new Mock<IReferenceDataRetrievalService>();
	    var mockConversionApplicationRetrievalService = new Mock<IConversionApplicationRetrievalService>();
        var schoolController = new SchoolController(mockLogger.Object, mockReferenceDataRetrievalService.Object, mockConversionApplicationRetrievalService.Object);
	    string selectedSchool = "Wise owl primary school (587634)"; // selected value will be in the format 'Wise owl primary school (587634)'

        // act
        IActionResult result = await schoolController.ReturnSchoolDetailsPartialViewPopulated(selectedSchool);

	    var viewModel = (result as ViewResult).Model;

	    // assert
	    Assert.That(result, Is.Not.Null);
	    Assert.That(viewModel, Is.Not.Null);
    }
}