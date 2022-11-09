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
		public int ApplicationId { get; private set; }

		//// Below are props for UI display
		public ApplicationTypes ApplicationType { get; private set; }

		public string ApplicationReferenceNumber { get; private set; } = string.Empty;

		public List<SchoolApplyingToConvert> SchoolOrSchoolsApplyingToConvert { get; private set; } = new();

		public string? NameOfTrustToJoin { get; private set; }

		/// <summary>
		/// Overall application status - comes from API now
		/// </summary>
		public ApplicationStatus ApplicationStatus { get; private set; }

		/// <summary>
		/// Calculated within here ONLY dependent on whether all the components / sections have been completed !
		/// </summary>
		public Status ConversionStatus { get; private set; }

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType
		/// </summary>
		public string SchoolHeaderText { get; private set; } = string.Empty;

		/// <summary>
		/// This will ONLY have a value IF ApplicationType = FormNewMat OR FormNewSingleAcademyTrust
		/// </summary>
		public string? SchoolName { get; private set; }

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType
		/// </summary>
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
		}

		public async Task<ActionResult> OnGetAsync(int appId)
		{
			// check user access
			var checkStatus = await CheckApplicationPermission(appId);

			if (checkStatus is ForbidResult)
			{
				return RedirectToPage("ApplicationAccessException");
			}

			//// on load - grab draft application from temp
			var draftConversionApplication = await LoadAndSetApplicationDetails(appId);

			if (draftConversionApplication == null)
			{
				return Page();
			}

			var school = draftConversionApplication.Schools.FirstOrDefault();

			if (school != null)
			{
				school.SchoolApplicationComponents =
					await ConversionApplicationRetrievalService.GetSchoolApplicationComponents(appId, school.URN);
			}

			PopulateUiModel(draftConversionApplication, school);

			return Page();
		}

		public IActionResult OnPostAsync()
		{
			return RedirectToPage("/SchoolOverview", ApplicationId);
		}

		private void PopulateUiModel(ConversionApplication? conversionApplication, SchoolApplyingToConvert? school)
		{
			if (conversionApplication != null)
			{
				// ConversionStatus = whether school.SchoolApplicationComponents.Status == Completed !!
				// ConversionStatus = could be 'NotStarted', 'InProgress' or 'Complete'
				if (school != null && school.SchoolApplicationComponents.Any())
				{
					ConversionStatus = CalculateApplicationStatus(school);
				}

				ApplicationId = conversionApplication.ApplicationId;
				ApplicationType = conversionApplication.ApplicationType;
				ApplicationReferenceNumber = conversionApplication.ApplicationReference;
				ApplicationStatus = conversionApplication.ApplicationStatus;
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
				// &&&&&&& application should have a trust ++ trust sections filled in !!
			}
		}

		// TODO MR:- what logic drives this !
		private Status CalculateApplicationStatus(SchoolApplyingToConvert? school)
		{
			if (school != null && school.SchoolApplicationComponents.Any())
			{
				if(school.SchoolApplicationComponents.All(comp => comp.Status == Status.Completed))
				{
					return Status.Completed;
				}
				else
				{
					return Status.InProgress;
				}
			}

			return Status.NotStarted;
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
