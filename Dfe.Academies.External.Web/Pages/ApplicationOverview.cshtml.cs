using System.Security.Claims;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
	public class ApplicationOverviewModel : BasePageEditModel
	{
		private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

		public int ApplicationId { get; private set; }

		// Below are props for UI display, shunt over to separate view model?
		public ApplicationTypes ApplicationType { get; private set; }

		public string ApplicationReferenceNumber { get; private set; } = string.Empty;

		public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; private set; } = new();

		public string? NameOfTrustToJoin { get; private set; }

		// overall application status
		public ApplicationStatus ApplicationStatus { get; private set; }

		public Status ConversionStatus { get; private set; }

		public string SchoolHeaderText { get; private set; } = string.Empty;

		/// <summary>
		/// this will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
		/// </summary>
		public string? SchoolName { get; private set; }

		public string TrustHeaderText { get; private set; } = string.Empty;

		/// <summary>
		/// Always have a trust conversion status whether Join a MAT or form a MAT !!
		/// </summary>
		public Status TrustConversionStatus { get; private set; }

		/// <summary>
		/// this will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
		/// </summary>
		public SchoolComponentsViewModel SchoolComponents { get; private set; } = new();

		public List<ConversionApplicationContributorViewModel> ExistingContributors { get; private set; } = new();

		public ApplicationOverviewModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
										IReferenceDataRetrievalService referenceDataRetrievalService
		) : base(conversionApplicationRetrievalService, referenceDataRetrievalService)
		{
			_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
		}

		public async Task OnGetAsync(int appId)
		{
			//// on load - grab draft application from temp
			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			if (draftConversionApplication != null)
			{
				var school = draftConversionApplication.Schools.FirstOrDefault();

				if (school != null)
				{
					school.SchoolApplicationComponents =
						await _conversionApplicationRetrievalService.GetSchoolApplicationComponents(appId, school.URN);
				}

				PopulateUiModel(draftConversionApplication, school);
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			return RedirectToPage("/SchoolOverview", ApplicationId);
		}

		private void PopulateUiModel(ConversionApplication? conversionApplication, SchoolApplyingToConvert? school)
		{
			if (conversionApplication != null)
			{
				// ApplicationStatus = whether school.SchoolApplicationComponents.Status == Completed !!
				// ApplicationStatus = could be 'NotStarted', 'InProgress' or 'Complete'

				ApplicationId = conversionApplication.ApplicationId;
				ApplicationType = conversionApplication.ApplicationType;
				ApplicationReferenceNumber = conversionApplication.ApplicationReference;
				ApplicationStatus = conversionApplication.ApplicationStatus;
				ConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
				SchoolOrSchoolsApplyingToConvert = conversionApplication.Schools;
				NameOfTrustToJoin = conversionApplication.TrustName;

				if (conversionApplication.ApplicationType == ApplicationTypes.FormAMat)
				{
					TrustHeaderText = "The trust being formed";
					SchoolHeaderText = "The schools applying to convert";
					TrustConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !
				}
				else
				{
					TrustHeaderText = "The trust the school will join";
					SchoolHeaderText = "The school applying to convert";
					SchoolName = school?.SchoolName;
					TrustConversionStatus = Status.NotStarted; // TODO MR:- what logic drives this !

					//// Convert from List<ConversionApplicationAuditEntry> -> List<ViewModels.ApplicationAuditViewModel>
					////Audits = auditEntries.Select(e =>
					//// new ViewModels.ApplicationAuditViewModel
					//// {
					////  What =
					////   $"{e.CreatedBy} {e.TypeOfChange} the {e.PropertyChanged}",
					////  When = e.DateCreated,
					////  Who = e.CreatedBy
					//// }).ToList();
				}

				// Convert from List<ConversionApplicationComponent> -> List<ViewModels.ApplicationComponentViewModel>
				if (school != null)
				{
					SchoolComponentsViewModel componentsVm = new()
					{
						URN = school.URN,
						ApplicationId = conversionApplication.ApplicationId,
						SchoolComponents = school.SchoolApplicationComponents.Select(c =>
							new ApplicationComponentViewModel(name: c.Name,
								uri: SetSchoolApplicationComponentUriFromName(c.Name))
							{
								Status = c.Status
							}).ToList()
					};

					SchoolComponents = componentsVm;
				}

				// grab current user email
				string email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";

				// look up user in contributors collection to find their role !!!
				if (!string.IsNullOrWhiteSpace(email))
				{
					var currentUser =
						conversionApplication.Contributors.FirstOrDefault(x => x.EmailAddress == email);

					// set users role
					if (currentUser is { Role: SchoolRoles.ChairOfGovernors })
					{
						UserHasSubmitApplicationRole = true;
					}
				}

				// contributors
				// convert application?.Contributors -> list<ConversionApplicationContributorViewModel>
				if (conversionApplication.Contributors.Any())
				{
					var contributors = conversionApplication.Contributors
						.Select(e => new ConversionApplicationContributorViewModel(e.FullName, e.Role, e.OtherRoleName))
						.ToList();

					ExistingContributors = contributors;
				}

				// TODO MR:- submit button should NOT be available unless ALL school.SchoolApplicationComponents.Status == Completed !!
			}
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			// does not apply on this page
			return true;
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			// does not apply on this page
			return new();
		}
	}
}
