﻿using System.ComponentModel.DataAnnotations;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Extensions;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.Base;
using Dfe.Academies.External.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Pages.School
{
	public class ApplicationChangeSchoolNameModel : BaseSchoolPageEditModel
	{
		//// MR:- VM props to capture data
		[BindProperty]
		[Required(ErrorMessage = "You must provide details")]
		public SelectOption? ChangeName { get; set; }

		[BindProperty]
		public string? ChangeSchoolName { get; set; }

		public bool ChangeSchoolNameError
		{
			get
			{
				return !ModelState.IsValid && ModelState.Keys.Contains("ChangeSchoolNameNotEntered");
			}
		}

		public ApplicationChangeSchoolNameModel(IConversionApplicationRetrievalService conversionApplicationRetrievalService,
			IReferenceDataRetrievalService referenceDataRetrievalService,
			IConversionApplicationService academisationCreationService)
			: base(conversionApplicationRetrievalService, referenceDataRetrievalService, 
				academisationCreationService, "SchoolConversionKeyDetails")
		{}

		///<inheritdoc/>
		public override bool RunUiValidation()
		{
			if (ChangeName == SelectOption.Yes && string.IsNullOrWhiteSpace(ChangeSchoolName))
			{
				ModelState.AddModelError("ChangeSchoolNameNotEntered", "You must add a new school name");
			}			
			
			if (!ModelState.IsValid)
			{
				PopulateValidationMessages();
				return false;
			}

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
			// if school NOT changing name blank out 'ProposedNewSchoolName'
			if (ChangeName == SelectOption.No)
			{
				return new Dictionary<string, dynamic>
				{
					{ nameof(SchoolApplyingToConvert.ConversionChangeNamePlanned), false},
					{ nameof(SchoolApplyingToConvert.ProposedNewSchoolName), null }
				};
			}
			else
			{
				return new Dictionary<string, dynamic>
				{
					{ nameof(SchoolApplyingToConvert.ConversionChangeNamePlanned), true },
					{ nameof(SchoolApplyingToConvert.ProposedNewSchoolName), ChangeSchoolName! }
				};
			}
		}

		///<inheritdoc/>
		public override void PopulateUiModel(SchoolApplyingToConvert selectedSchool)
		{
			var conversionChangeNamePlanned = selectedSchool.ConversionChangeNamePlanned.GetEnumValue();

			if (conversionChangeNamePlanned.HasValue)
			{
				ChangeName = conversionChangeNamePlanned.Value;
			}

			ChangeSchoolName = selectedSchool.ProposedNewSchoolName;
		}
	}
}
