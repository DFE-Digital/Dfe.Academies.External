using Dfe.Academies.External.Web.Pages.School;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.UnitTest.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Dfe.Academies.External.Web.UnitTest.Pages.School
{
	internal sealed class ApplicationConversionTargetDateModelTests
	{
		// TODO MR:- OnGet

		// TODO MR:- OnPostAsync___ModelIsValid___InValid

		// TODO MR:- OnPostAsync___ModelIsValid___Valid

		private static ApplicationConversionTargetDateModel SetupApplicationConversionTargetDateModel(
			ILogger<ApplicationConversionTargetDateModel> mockLogger,
			IConversionApplicationCreationService mockConversionApplicationCreationService,
			IConversionApplicationRetrievalService mockConversionApplicationRetrievalService,
			IReferenceDataRetrievalService mockReferenceDataRetrievalService,
			bool isAuthenticated = false)
		{
			(PageContext pageContext, TempDataDictionary tempData, ActionContext actionContext) = PageContextFactory.PageContextBuilder(isAuthenticated);

			return new ApplicationConversionTargetDateModel(mockLogger, mockConversionApplicationRetrievalService,
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
