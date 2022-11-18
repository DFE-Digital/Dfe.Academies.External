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

		/// <summary>
		/// This is already set dependent on whether application type = join a mat / form a mat!
		/// </summary>
		public string? NameOfTrustToJoin { get; private set; }

		/// <summary>
		/// Overall application status from API
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

		/// <summary>
		/// flag to set different UI text - contributors
		/// </summary>
		public bool HasSchool { get; private set; }

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType &&& user role !!!
		/// </summary>
		public string HeaderText { get; private set; } = string.Empty;

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

			if (conversionApplication != null)
			{
				// ConversionStatus = whether school.SchoolApplicationComponents.Status == Completed !!
				// ConversionStatus = could be 'NotStarted', 'InProgress' or 'Complete'
				if (school != null && school.SchoolApplicationComponents.Any())
				{
					ConversionStatus = CalculateApplicationStatus(school);
				}

				TrustConversionStatus = ConversionApplicationRetrievalService.CalculateTrustStatus(conversionApplication);

				ApplicationId = conversionApplication.ApplicationId;
				ApplicationType = conversionApplication.ApplicationType;
				ApplicationReferenceNumber = conversionApplication.ApplicationReference;
				ApplicationStatus = conversionApplication.ApplicationStatus;
				SchoolOrSchoolsApplyingToConvert = conversionApplication.Schools;
				NameOfTrustToJoin = conversionApplication.TrustName;

				HasSchool = conversionApplication.HasSchool;
				
				if (conversionApplication.ApplicationType == ApplicationTypes.FormAMat)
				{
					HeaderText = "All school and trust details must be given before this application can be submitted.";
					TrustHeaderText = "The trust being formed";
					SchoolHeaderText = "The schools applying to convert";
				}
				else // JAM
				{
					// Also check UserHasSubmitApplicationRole - chair / non-chair !!
					HeaderText = UserHasSubmitApplicationRole ? "This application can’t be submitted until all sections are complete. Your answers will be saved after each question."
						: "Your answers will be saved after each question. Once all sections are complete, the school's chair will be able to submit the application.";
					TrustHeaderText = "The trust the school will join";
					SchoolHeaderText = "The school applying to convert";
					SchoolName = school?.SchoolName;
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

				// TODO:- need to remove 'Only the school's chair of governors can submit this application' for
				// non-chair user if declaration not filled in?? see ticket (https://dfe-gov-uk.visualstudio.com.mcas.ms/Academies-and-Free-Schools-SIP/_workitems/edit/112259)
				// TODO:- need seperate flag against conversionApplication?

				// TODO :- submit button should NOT be available unless ConversionStatus == Completed &&&&&&& TrustConversionStatus = Completed !!
				// see ticket (https://dfe-gov-uk.visualstudio.com.mcas.ms/Academies-and-Free-Schools-SIP/_workitems/edit/112260)

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
		}

		/// <summary>
		/// calculate overall application status based on whether all sections have been completed
		/// INCLUDING declaration i.e. submit button is visible!!
		/// </summary>
		/// <param name="school"></param>
		/// <returns></returns>
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

			// TODO:- will also need to check trust status flag which is separate from above

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
