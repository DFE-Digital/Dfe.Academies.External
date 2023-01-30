using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Helpers;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dfe.Academies.External.Web.Pages.Trust.FormAMat
{
	public class ApplicationNewTrustKeyPersonModel : BaseTrustFamApplicationPageEditModel
	{
		[BindProperty(SupportsGet = true)]
		public int KeyPersonId { get; set; }

		public bool IsEdit { get { return KeyPersonId > 0; } }

		public string TrustKeyPersonDobDate = "sip_formtrustkeypersondate";
		public string TrustName { get; private set; } = string.Empty;

		[BindProperty]
		public string? TrustKeyPersonDob { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TrustKeyPersonDobDay { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TrustKeyPersonDobMonth { get; set; }

		[BindProperty] // MR:- don't know whether I need this
		public string? TrustKeyPersonDobYear { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please enter a name")]
		public string TrustKeyPersonName { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Please enter a biography")]
		public string TrustKeyPersonBiography { get; set; } = string.Empty;

		[BindProperty]
		public string? TrustKeyPersonTimeInRole { get; set; } = string.Empty;

		public DateTime TrustKeyPersonDobLocal { get; set; }

		[BindProperty]
		public bool TrustKeyPersonMember { get; set; }

		[BindProperty]
		public bool TrustKeyPersonTrustee { get; set; }

		[BindProperty]
		public bool TrustKeyPersonCeo { get; set; }

		[BindProperty]
		public bool TrustKeyPersonChair { get; set; }

		[BindProperty]
		public bool TrustKeyPersonFinancialDirector { get; set; }

		[BindProperty]
		public bool TrustKeyPersonOther { get; set; }


		public bool TrustKeyPersonDobNotEntered
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustKeyPersonDobLocalNotEntered");
			}
		}

		public bool TrustKeyPersonDobNotError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustKeyPersonDobLocalError");
			}
		}

		public bool TrustKeyPersonRoleError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustKeyPersonRoleError");
			}
		}

		public bool TrustKeyPersonTimeInRoleNotEntered
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("TrustKeyPersonTimeInRoleNotEntered");
			}
		}

		public ApplicationNewTrustKeyPersonModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
												IReferenceDataRetrievalService referenceDataRetrievalService,
												IConversionApplicationCreationService conversionApplicationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, conversionApplicationCreationService,
				"ApplicationNewTrustKeyPeopleSummary")
		{
		}

		public override async Task<IActionResult> OnPostAsync()
		{
			var form = Request.Form;
			var dateComponents = RetrieveDateTimeComponentsFromDatePicker(form, TrustKeyPersonDobDate);
			string day = dateComponents.FirstOrDefault(x => x.Key == "day").Value;
			string month = dateComponents.FirstOrDefault(x => x.Key == "month").Value;
			string year = dateComponents.FirstOrDefault(x => x.Key == "year").Value;

			TrustKeyPersonDobLocal = BuildDateTime(day, month, year);

			if (!RunUiValidation())
			{
				// MR:- date input disappears without below !!
				RePopDatePickerModel(day, month, year);
				return Page();
			}

			// grab draft application from temp= null
			var draftConversionApplication =
				TempDataHelper.GetSerialisedValue<ConversionApplication>(
					TempDataHelper.DraftConversionApplicationKey, TempData) ?? new ConversionApplication();

			var roles = new List<NewTrustKeyPersonRole>();
			var keyPerson = draftConversionApplication.FormTrustDetails?.KeyPeople.SingleOrDefault(x => x.Id == KeyPersonId);

			//only add in the role if it is not in current list when editing
			if (TrustKeyPersonCeo)
			{

				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.CEO, string.Empty));
			}

			if (TrustKeyPersonChair)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Chair, string.Empty));
			}

			if (TrustKeyPersonFinancialDirector && TrustKeyPersonTimeInRole != null)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.FinancialDirector, TrustKeyPersonTimeInRole));
			}
			else
			{
				TrustKeyPersonTimeInRole = string.Empty;
			}

			if (TrustKeyPersonTrustee)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Trustee, string.Empty));
			}

			if (TrustKeyPersonMember)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Member, string.Empty));
			}

			if (TrustKeyPersonOther)
			{
				roles.Add(new NewTrustKeyPersonRole(KeyPersonRole.Other, string.Empty));
			}

			if (IsEdit && keyPerson != null)
			{
				// set Id of role if already exists in list
				foreach (var role in roles)
				{
					var existingRole = keyPerson.Roles.SingleOrDefault(x => x.Role == role.Role);

					if (existingRole != null)
					{
						role.Id = existingRole.Id;
					}
				}

				keyPerson.Name = TrustKeyPersonName;
				keyPerson.Biography = TrustKeyPersonBiography;
				keyPerson.DateOfBirth = TrustKeyPersonDobLocal;
				keyPerson.Roles = roles;

				await ConversionApplicationCreationService.UpdateKeyPerson(ApplicationId, keyPerson);
			}
			else
			{
				var newKeyPerson = new NewTrustKeyPerson(TrustKeyPersonName, TrustKeyPersonDobLocal,
					TrustKeyPersonBiography, roles);

				await ConversionApplicationCreationService.CreateKeyPerson(ApplicationId, newKeyPerson);

			}



			// update temp store for next step
			TempDataHelper.StoreSerialisedValue(TempDataHelper.DraftConversionApplicationKey, TempData, draftConversionApplication);

			// need to go onto next step in process 'reasons for conversion page'
			return RedirectToPage(NextStepPage, new { appId = ApplicationId });
		}

		///<inheritdoc/>
		public override void PopulateValidationMessages()
		{
			PopulateViewDataErrorsWithModelStateErrors();
		}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (!TrustKeyPersonFinancialDirector && !TrustKeyPersonCeo && !TrustKeyPersonChair && !TrustKeyPersonMember && !TrustKeyPersonOther && !TrustKeyPersonTrustee)
			{
				ModelState.AddModelError("TrustKeyPersonRoleError", "Please select at least one role");
			}

			if (TrustKeyPersonDobLocal == DateTime.MinValue)
			{
				ModelState.AddModelError("TrustKeyPersonDobLocalNotEntered", "Date is invalid");
			}

			// date not less < today
			if (TrustKeyPersonDobLocal >= DateTime.Now.Date)
			{
				ModelState.AddModelError("TrustKeyPersonDobLocalError", "Date of birth must be in the past");
			}

			if (TrustKeyPersonFinancialDirector && string.IsNullOrEmpty(TrustKeyPersonTimeInRole))
			{
				ModelState.AddModelError("TrustKeyPersonTimeInRoleNotEntered", "Please enter time that the financial director expects to give to the role");
			}

			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

			return true;
		}

		public override Dictionary<string, dynamic> PopulateUpdateDictionary()
		{
			return new Dictionary<string, dynamic>
			{
			};
		}

		public override void PopulateUiModel(ConversionApplication? conversionApplication)
		{
			if (conversionApplication != null && conversionApplication.FormTrustDetails != null)
			{
				TrustName = conversionApplication.FormTrustDetails.FormTrustProposedNameOfTrust;
				if (IsEdit)
				{

					var keyPerson = conversionApplication.FormTrustDetails.KeyPeople.SingleOrDefault(x => x.Id == KeyPersonId);

					if (keyPerson != null)
					{
						TrustKeyPersonName = keyPerson.Name;
						TrustKeyPersonBiography = keyPerson.Biography;
						RePopDatePickerModel(keyPerson.DateOfBirth.Day.ToString(), keyPerson.DateOfBirth.Month.ToString(), keyPerson.DateOfBirth.Year.ToString());
						if (keyPerson.Roles.Any())
						{
							TrustKeyPersonCeo = keyPerson.Roles.Any(x => x.Role == KeyPersonRole.CEO);
							TrustKeyPersonChair = keyPerson.Roles.Any(x => x.Role == KeyPersonRole.Chair);
							if (keyPerson.Roles.Any(x => x.Role == KeyPersonRole.FinancialDirector))
							{
								var role = keyPerson.Roles.Single(x => x.Role == KeyPersonRole.FinancialDirector);
								TrustKeyPersonFinancialDirector = true;
								TrustKeyPersonTimeInRole = role.TimeInRole;
							}

							TrustKeyPersonTrustee = keyPerson.Roles.Any(x => x.Role == KeyPersonRole.Trustee);
							TrustKeyPersonMember = keyPerson.Roles.Any(x => x.Role == KeyPersonRole.Member);
							TrustKeyPersonOther = keyPerson.Roles.Any(x => x.Role == KeyPersonRole.Other);
						}

					}
				}
			}
		}

		private void RePopDatePickerModel(string newTrustKeyPersonDobDay, string newTrustKeyPersonDoMonth, string newTrustKeyPersonDoYear)
		{
			TrustKeyPersonDobDay = newTrustKeyPersonDobDay;
			TrustKeyPersonDobMonth = newTrustKeyPersonDoMonth;
			TrustKeyPersonDobYear = newTrustKeyPersonDoYear;
		}

		public bool IsPropertyInvalid(string propertyKey)
		{
			return ModelState.GetFieldValidationState(propertyKey) == ModelValidationState.Invalid;
		}
	}
}
