using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
    public class NewApplicationContributorModel : BasePageModel
    {
        private readonly ILogger<NewApplicationContributorModel> _logger;
        private readonly IConversionApplicationCreationService _academisationCreationService;
        private ConversionApplication _draftConversionApplication;

        public NewApplicationContributorModel(ILogger<NewApplicationContributorModel> logger,
            IConversionApplicationCreationService academisationCreationService)
        {
            _logger = logger;
            _academisationCreationService = academisationCreationService;
        }

        public async Task OnGetAsync()
        {
            //// on load - grab draft application from temp
            _draftConversionApplication = TempDataHelper.GetSerialisedValue<ConversionApplication>("draftConversionApplication", TempData) ?? new ConversionApplication();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // error messages component consumes ViewData["Errors"]
                PopulateValidationMessages();
                return Page();
            }

            return Page();
        }

        public override void PopulateValidationMessages()
        {
            ViewData["Errors"] = ConvertModelStateToDictionary();

            if (!ModelState.IsValid)
            {
                foreach (var modelStateError in ConvertModelStateToDictionary())
                {
                    // MR:- add friendly message for validation summary
                    if (!this.ValidationErrorMessagesViewModel.ValidationErrorMessages.ContainsKey(modelStateError.Key))
                    {
                        this.ValidationErrorMessagesViewModel.ValidationErrorMessages.Add(modelStateError.Key, modelStateError.Value);
                    }
                }
            }
        }
    }
}
