using System.Security.Claims;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums; 
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Dfe.Academies.External.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages
{
	public class ApplicationOverviewModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
									IReferenceDataRetrievalService referenceDataRetrievalService,
									IConversionApplicationService conversionApplicationCreationService,
									ILogger<ApplicationOverviewModel> logger
		) : BasePageEditModel(conversionApplicationRetrievalService, referenceDataRetrievalService)
	{
		[BindProperty]
		public int ApplicationId { get; set; }

		[BindProperty]
		public bool HideDeleteButton { get; set; } = true;

		//// Below are props for UI display
		public ApplicationTypes ApplicationType { get; private set; }

		public string ApplicationReferenceNumber { get; private set; } = string.Empty;

		public List<SchoolComponentsViewModel> SchoolOrSchoolsApplyingToConvert { get; private set; } = new();

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
		/// Always have a trust conversion status whether Join a MAT or form a MAT !!
		/// </summary>
		public Status TrustConversionStatus { get; private set; }

		/// <summary>
		/// ONLY whether declaration section has been completed!
		/// </summary>
		public Status DeclarationStatus { get; private set; }

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType
		/// </summary>
		public string SchoolHeaderText { get; private set; } = string.Empty;

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType
		/// </summary>
		public string TrustHeaderText { get; private set; } = string.Empty;

		/// <summary>
		/// flag to set different UI text - contributors
		/// </summary>
		public bool HasSchool { get; private set; }

		/// <summary>
		/// UI text, set within here ONLY dependent on ApplicationType &&& user role !!!
		/// </summary>
		public string HeaderText { get; private set; } = string.Empty; 
		public DateTime? ApplicationCreatedOn { get; set; }

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
			
			// var school = draftConversionApplication.Schools.FirstOrDefault();
			//
			// if (school != null)
			// {
			// 	school.SchoolApplicationComponents =
			// 		await ConversionApplicationRetrievalService.GetSchoolApplicationComponents(appId, school.URN);
			// }

			ApplicationId = appId;

			PopulateUiModel(draftConversionApplication);

			return Page();
		}

		private void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			
			// grab current user email
			string email = User.FindFirst(ClaimTypes.Email)?.Value ?? "";		
			var firstContributorEmail = conversionApplication?.Contributors?.FirstOrDefault()?.EmailAddress;
			
			HideDeleteButton = (email != firstContributorEmail);
				
		
			logger.LogInformation($"Populating application overview for user | Email: { email }");

			// look up user in contributors collection to find their role !!!
			if (!string.IsNullOrWhiteSpace(email))
			{
				foreach (var contributor in conversionApplication!.Contributors)
				{
					logger.LogInformation($"Contrubutor email: {contributor.EmailAddress} | role: {contributor.Role}");
				}

				// possible fix for not finding right user
				var currentUser =
					conversionApplication.Contributors.FirstOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());

				logger.LogInformation($"User found, Id: { currentUser?.ContributorId } | Name: {currentUser?.FullName} | Email: {email}");
				logger.LogInformation($"User role: {currentUser?.Role } | Email: {email}");

				// set users role
				if (currentUser is { Role: SchoolRoles.ChairOfGovernors })
				{
					UserHasSubmitApplicationRole = true;
				}
			}

			logger.LogInformation($"Can user submit | UserHasSubmitApplicationRole: {UserHasSubmitApplicationRole}");

			if (conversionApplication != null)
			{
				// 3 statuses required by UI !!!!
				TrustConversionStatus = ConversionApplicationRetrievalService.CalculateTrustStatus(conversionApplication);
				DeclarationStatus =
					ConversionApplicationRetrievalService.CalculateApplicationDeclarationStatus(conversionApplication);

				ApplicationType = conversionApplication.ApplicationType;
				ApplicationReferenceNumber = conversionApplication.ApplicationReference;
				ApplicationStatus = conversionApplication.ApplicationStatus;
				ApplicationCreatedOn = conversionApplication.CreatedOn;
				SchoolOrSchoolsApplyingToConvert = new List<SchoolComponentsViewModel>();
				
				foreach (var school in conversionApplication.Schools)
				{
					var applicationComponents = ConversionApplicationRetrievalService.GetSchoolApplicationComponents(ApplicationId, school.URN)
						.Result
						.ToList();
					
					SchoolOrSchoolsApplyingToConvert.Add(new SchoolComponentsViewModel(
						conversionApplication.ApplicationId,
						school.URN,
						school.SchoolName,
						ConversionApplicationRetrievalService.CalculateSchoolStatus(applicationComponents),
						applicationComponents));
				}
				
				ConversionStatus = ConversionApplicationRetrievalService.CalculateApplicationStatus(conversionApplication, SchoolOrSchoolsApplyingToConvert);
				NameOfTrustToJoin = conversionApplication.TrustName;

				HasSchool = conversionApplication.Schools.Any();
				
				if (conversionApplication.ApplicationType == ApplicationTypes.FormAMat)
				{
					HeaderText = ConversionStatus != Status.Completed ? "All school and trust details must be given before this application can be submitted." : "";
					TrustHeaderText = "Give details of the trust";
					SchoolHeaderText = "Give details of schools in the trust";
				}
				else // JAM
				{
					TrustHeaderText = "The trust the school will join";
					SchoolHeaderText = "The school applying to convert";
				}
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

		public async Task<IActionResult> OnPostAsync()
		{
			// no inputs from form - just logging WHO has submitted it !!
			// only have 'applicationStatus' in PUT

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			draftConversionApplication.ApplicationStatus = ApplicationStatus.Submitted;

			await conversionApplicationCreationService.SubmitApplication(ApplicationId);

			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			// need to go onto next step in process 'submit confirmation'
			return RedirectToPage("ApplicationSubmitted", new { appId = ApplicationId });
		}
	}
}
