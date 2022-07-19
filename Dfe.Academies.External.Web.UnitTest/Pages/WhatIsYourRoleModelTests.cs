using System.Threading.Tasks;
using Dfe.Academies.External.Web.Pages;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Dfe.Academies.External.Web.UnitTest.Pages;

[Parallelizable(ParallelScope.All)]
internal sealed class WhatIsYourRoleModelTests
{
    /// <summary>
    /// "draftConversionApplication" in temp storage
    /// from previous step in the new application wizard
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task WhatIsYourRoleModel___OnGetAsync___Valid()
    {
        // arrange
        var draftConversionApplicationStorageKey = TempDataHelper.DraftConversionApplicationKey;
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();
        
        var conversionApplication = ConversionApplicationTestDataFactory.BuildNewConversionApplicationWithChairRole();

        // act
        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);
        TempDataHelper.StoreSerialisedValue(draftConversionApplicationStorageKey, pageModel.TempData, conversionApplication);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    /// <summary>
    /// No "draftConversionApplication" in temp storage
    /// Code handles, spins up new()
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task WhatIsYourRoleModel___OnGetAsync___InValid()
    {
        // arrange
        var mockAcademisationCreationService = new Mock<IConversionApplicationCreationService>();
        var mockLogger = new Mock<ILogger<WhatIsYourRoleModel>>();

        var pageModel = SetupWhatIsYourRoleModel(mockLogger.Object, mockAcademisationCreationService.Object);

        // act
        await pageModel.OnGetAsync();

        // assert
        Assert.That(pageModel.TempData["Errors"], Is.EqualTo(null));
    }

    // TODO MR:- WhatIsYourRoleModel___OnPostAsync___ModelIsValid___InValid

    // TODO MR:- WhatIsYourRoleModel___OnPostAsync___ModelIsValid___InValid - manual model data check
    // if (SchoolRole == SchoolRoles.Other && string.IsNullOrWhiteSpace(OtherRoleNotListed))

    // TODO MR:- WhatIsYourRoleModel___OnPostAsync___ModelIsValid___Valid


    // TODO MR:- WhatIsYourRoleModel___OnPostAsync___ModelIsValid___InValid = without "draftConversionApplication" in temp storage


    private static WhatIsYourRoleModel SetupWhatIsYourRoleModel(
        ILogger<WhatIsYourRoleModel> mockLogger, 
        IConversionApplicationCreationService mockAcademisationCreationService,
        bool isAuthenticated = false)
    {
        (PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

        return new WhatIsYourRoleModel(mockLogger, mockAcademisationCreationService)
        {
            PageContext = pageContext,
            TempData = tempData,
            Url = new UrlHelper(actionContext),
            MetadataProvider = pageContext.ViewData.ModelMetadata
        };
    }
}