using AutoMapper.Internal;
using Dfe.Academies.External.Web.Commands;
using Dfe.Academies.External.Web.Dtos;
using Dfe.Academies.External.Web.Enums;
using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.Pages.School;
using Microsoft.AspNetCore.Mvc;

namespace Dfe.Academies.External.Web.Services;

public sealed class ConversionApplicationCreationService : BaseService, IConversionApplicationCreationService
{
	private readonly ILogger<ConversionApplicationCreationService> _logger;
	private readonly HttpClient _httpClient;
	private readonly ResilientRequestProvider _resilientRequestProvider;
	private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;

	public ConversionApplicationCreationService(IHttpClientFactory httpClientFactory,
												ILogger<ConversionApplicationCreationService> logger,
												IConversionApplicationRetrievalService conversionApplicationRetrievalService) : base(httpClientFactory)
	{
		_httpClient = httpClientFactory.CreateClient(AcademisationAPIHttpClientName);
		_logger = logger;
		_resilientRequestProvider = new ResilientRequestProvider(_httpClient);
		_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
	}

	///<inheritdoc/>
	public async Task<ConversionApplication> CreateNewApplication(ConversionApplication application)
	{
		try
		{
			// guard clause - CANNOT create an application without a contributor
			if (!application.Contributors.Any())
			{
				throw new ArgumentException("Mandatory Contributor Missing");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/?api-version=V1";
			CreateApplicationApiModel createApplicationApiModel;

			// Push data into Academisation API
			// JsonSerializerOptions = done within _resilientRequestProvider
			var contributor = application.Contributors.FirstOrDefault();

			createApplicationApiModel =
				new(application.ApplicationType.ToString(),
					new ApplicationContributorApiModel(contributor.FirstName,
						contributor.LastName,
						contributor.EmailAddress,
						contributor.Role.ToString(),
						contributor.OtherRoleName)
					);

			var result = await _resilientRequestProvider.PostAsync<ConversionApplication, CreateApplicationApiModel>(apiurl, createApplicationApiModel);

			return result;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::CreateNewApplication::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task AddSchoolToApplication(int applicationId, int schoolUrn, string name)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://academies-academisation-api-dev.azurewebsites.net/application/99
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";

			// PL:- if is form a mat we can add as many schools as we want just check that we aren't adding the same school twice
			// if is join a mat then we add only if it is the first one
			if ((application.ApplicationType == ApplicationTypes.FormAMat && application.Schools.All(c => c.URN != schoolUrn)) ||
			    (application.ApplicationType == ApplicationTypes.JoinAMat && !application.Schools.Any()))
			{
				SchoolApplyingToConvert school = new(name, schoolUrn, null);
				application.Schools.Add(school);
			}

			//PL:- if it is join a mat we only add one school and from then it is changes to the existing school to maintain the attached data
			if (application.ApplicationType == ApplicationTypes.JoinAMat)
			{
				var existingSchool = application.Schools.FirstOrDefault();
				if (existingSchool != null)
				{
					// MR:- can't do remove because then we will bin all the associated data !!
					existingSchool.URN = schoolUrn;
					existingSchool.SchoolName = name;
				}
			}

			//// structure of JSON in body is having a 'contributors' prop - same as ConversionApplication() obj
			// MR:- no response from Academies API - Just an OK
			await _resilientRequestProvider.PutAsync(apiurl, application);
		}
		catch (HttpRequestException httpEx)
		{
			_logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::APIException - {Message}",
				httpEx.Message);
			throw;
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::AddSchoolToApplication::Exception - {Message}",
				ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task AddTrustToApplication(int applicationId, int trustUkPrn, string name, string trustReference)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			if (application.ApplicationType != ApplicationTypes.JoinAMat)
			{
				throw new ArgumentException("Application not of correct type");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://s184d01-aca-aca-app.nicedesert-a691fec6.westeurope.azurecontainerapps.io/application/99/join-trust
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/join-trust?api-version=V1";

			ExistingTrust trust;
			if (application.JoinTrustDetails != null)
			{
				trust = new ExistingTrust(applicationId, name, trustReference, trustUkPrn,
					application.JoinTrustDetails.ChangesToTrust,
					application.JoinTrustDetails.ChangesToTrustExplained,
					application.JoinTrustDetails.ChangesToLaGovernance,
					application.JoinTrustDetails.ChangesToLaGovernanceExplained);
			}
			else
			{
				trust = new ExistingTrust(applicationId, name, trustReference, trustUkPrn);
			}

			// MR:- no response from Academies API - Just an OK
			await _resilientRequestProvider.PutAsync(apiurl, trust);
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::AddTrustToApplication::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task SetExistingTrustDetails(int applicationId, ExistingTrust existingTrust)
	{
		try
		{
			// MR:- may need to call GetApplication() first within ConversionApplicationRetrievalService()
			// to grab current application data
			// before then patching ConversionApplication returned with data from application object
			var application = await GetApplication(applicationId);

			if (application == null || application.ApplicationId != applicationId)
			{
				throw new ArgumentException("Application not found");
			}

			if (application.ApplicationType != ApplicationTypes.JoinAMat)
			{
				throw new ArgumentException("Application not of correct type");
			}

			//// baseaddress has a backslash at the end to be a valid URI !!!
			//// https://s184d01-aca-aca-app.nicedesert-a691fec6.westeurope.azurecontainerapps.io/application/99/join-trust
			string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/join-trust?api-version=V1";


			// MR:- no response from Academies API - Just an OK
			await _resilientRequestProvider.PutAsync(apiurl, existingTrust);
		}
		catch (Exception ex)
		{
			_logger.LogError("ConversionApplicationCreationService::SetExistingTrustDetails::Exception - {Message}", ex.Message);
			throw;
		}
	}

	///<inheritdoc/>
	public async Task PutSchoolApplicationDetails(int applicationId, int schoolUrn, Dictionary<string, dynamic> schoolProperties)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		var school = application.Schools.FirstOrDefault(s => s.URN == schoolUrn);
		if (school == null)
		{
			throw new ArgumentException("School not found");
		}

		//Populate all school fields with the values in the dictionary
		foreach (var property in schoolProperties)
		{
			var prop = school.GetType().GetProperty(property.Key);
			if (prop.CanBeSet())
				prop?.SetValue(school, property.Value);
		}
		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";
		await _resilientRequestProvider.PutAsync(apiurl, application);
	}

	///<inheritdoc/>
	public async Task AddContributorToApplication(ConversionApplicationContributor contributor, int applicationId)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		application.Contributors.Add(contributor);

		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";
		await _resilientRequestProvider.PutAsync(apiurl, application);
	}

	///<inheritdoc/>
	public async Task RemoveContributorFromApplication(int contributorId, int applicationId)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		var contributor = application.Contributors.FirstOrDefault(c => c.ContributorId == contributorId);
		if (contributor != null)
		{
			application.Contributors.Remove(contributor);
		}

		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}?api-version=V1";
		await _resilientRequestProvider.PutAsync(apiurl, application);
	}

	///<inheritdoc/>
	public async Task CreateLoan(int applicationId, int schoolId, SchoolLoan loan)
	{
		var createLoanCommand = new CreateLoanCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			Amount = loan.Amount,
			Purpose = loan.Purpose,
			Provider = loan.Provider,
			InterestRate = loan.InterestRate,
			Schedule = loan.Schedule
		};
		string apiurl = $"{_httpClient.BaseAddress}school/loan/create";
		await _resilientRequestProvider.PutAsync(apiurl, createLoanCommand);
	}

	///<inheritdoc/>
	public async Task UpdateLoan(int applicationId, int schoolId, SchoolLoan loan)
	{
		var updateLoanCommand = new UpdateLoanCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			LoanId = loan.LoanId,
			Amount = loan.Amount,
			Purpose = loan.Purpose,
			Provider = loan.Provider,
			InterestRate = loan.InterestRate,
			Schedule = loan.Schedule
		};
		string apiurl = $"{_httpClient.BaseAddress}school/loan/update";
		await _resilientRequestProvider.PostAsync<IActionResult, UpdateLoanCommand>(apiurl, updateLoanCommand);
	}

	///<inheritdoc/>
	public async Task DeleteLoan(int applicationId, int schoolId, int loanId)
	{
		var deleteLoanCommand = new DeleteLoanCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			LoanId = loanId
		};
		string apiurl = $"{_httpClient.BaseAddress}school/loan/delete";
		await _resilientRequestProvider.DeleteAsync<DeleteLoanCommand>(apiurl, deleteLoanCommand);
	}

	///<inheritdoc/>
	public async Task CreateLease(int applicationId, int schoolId, SchoolLease lease)
	{
		var createLeaseCommand = new CreateLeaseCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			InterestRate = lease.InterestRate,
			LeaseTerm = lease.LeaseTerm,
			PaymentsToDate = lease.PaymentsToDate,
			Purpose = lease.Purpose,
			RepaymentAmount = lease.RepaymentAmount,
			ResponsibleForAssets = lease.ResponsibleForAssets,
			ValueOfAssets = lease.ValueOfAssets
		};
		string apiurl = $"{_httpClient.BaseAddress}school/lease/create";
		await _resilientRequestProvider.PutAsync(apiurl, createLeaseCommand);
	}

	///<inheritdoc/>
	public async Task UpdateLease(int applicationId, int schoolId, SchoolLease lease)
	{
		var updateLeaseCommand = new UpdateLeaseCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			LeaseId = lease.LeaseId,
			InterestRate = lease.InterestRate,
			LeaseTerm = lease.LeaseTerm,
			PaymentsToDate = lease.PaymentsToDate,
			Purpose = lease.Purpose,
			RepaymentAmount = lease.RepaymentAmount,
			ResponsibleForAssets = lease.ResponsibleForAssets,
			ValueOfAssets = lease.ValueOfAssets
		};
		string apiurl = $"{_httpClient.BaseAddress}school/lease/update";
		await _resilientRequestProvider.PostAsync<IActionResult, UpdateLeaseCommand>(apiurl, updateLeaseCommand);
	}

	///<inheritdoc/>
	public async Task DeleteLease(int applicationId, int schoolId, int leaseId)
	{
		var deleteLeaseCommand = new DeleteLeaseCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			LeaseId = leaseId
		};
		string apiurl = $"{_httpClient.BaseAddress}school/lease/delete";
		await _resilientRequestProvider.DeleteAsync<DeleteLeaseCommand>(apiurl, deleteLeaseCommand);
	}

	///<inheritdoc/>
	public async Task SetAdditionalDetails(int applicationId, int schoolId, string trustBenefitDetails, string? ofstedInspectionDetails,
		bool safeguarding, string? localAuthorityReorganisationDetails, string? localAuthorityClosurePlanDetails,
		string? dioceseName, string? dioceseFolderIdentifier, bool partOfFederation, string? foundationTrustOrBodyName,
		string? foundationConsentFolderIdentifier, DateTimeOffset? exemptionEndDate, string mainFeederSchools,
		string? resolutionConsentFolderIdentifier, SchoolEqualitiesProtectedCharacteristics? protectedCharacteristics,
		string? furtherInformation)
	{
		var setAdditionalDetailsCommand = new SetAdditionalDetailsCommand
		{
			ApplicationId = applicationId,
			SchoolId = schoolId,
			TrustBenefitDetails = trustBenefitDetails,
			OfstedInspectionDetails = ofstedInspectionDetails,
			Safeguarding = safeguarding,
			LocalAuthorityReorganisationDetails = localAuthorityReorganisationDetails,
			LocalAuthorityClosurePlanDetails = localAuthorityClosurePlanDetails,
			DioceseName = dioceseName,
			DioceseFolderIdentifier = dioceseFolderIdentifier,
			PartOfFederation = partOfFederation,
			FoundationTrustOrBodyName = foundationTrustOrBodyName,
			FoundationConsentFolderIdentifier = foundationConsentFolderIdentifier,
			ExemptionEndDate = exemptionEndDate,
			MainFeederSchools = mainFeederSchools,
			ResolutionConsentFolderIdentifier = resolutionConsentFolderIdentifier,
			ProtectedCharacteristics = protectedCharacteristics,
			FurtherInformation = furtherInformation
		};

		string apiurl = $"{_httpClient.BaseAddress}school/additional-details";
		await _resilientRequestProvider.PutAsync<SetAdditionalDetailsCommand>(apiurl, setAdditionalDetailsCommand);
	}

	///<inheritdoc/>
	public async Task PutApplicationFormAMatDetails(int applicationId, Dictionary<string, dynamic> famTrustProperties)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		var existingFamDetails = application.FormTrustDetails ?? new NewTrust(applicationId, "", application.ApplicationReference);

		// Populate all form a trust fields with the values in the dictionary
		foreach (var property in famTrustProperties)
		{
			existingFamDetails.GetType().GetProperty(property.Key)?.SetValue(existingFamDetails, property.Value);
		}

		application.FormTrustDetails = existingFamDetails;

		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/form-trust?api-version=V1";
		await _resilientRequestProvider.PutAsync(apiurl, application.FormTrustDetails);
	}

	///<inheritdoc/>
	public async Task SubmitApplication(int applicationId)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		// MR:- shouldn't we set who did this in the database?
		var command = new SubmitApplicationCommand(applicationId);

		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/submit?api-version=V1";
		// expected object is not used, so deserialization to a generic object is sufficient
		await _resilientRequestProvider.PostAsync<Object, SubmitApplicationCommand>(apiurl, command);
	}

	public async Task CreateKeyPerson(int applicationId, NewTrustKeyPerson person)
	{
		var createTrustKeyPersonCommand = new CreateTrustKeyPersonCommand
		{
			ApplicationId = applicationId,
			Name = person.Name,
			DateOfBirth = person.DateOfBirth,
			Biography = person.Biography,
			Roles = person.Roles
		};
		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/form-trust/key-person?api-version=V1";
		await _resilientRequestProvider.PostAsync<IActionResult, CreateTrustKeyPersonCommand>(apiurl, createTrustKeyPersonCommand);
	}

	public async Task UpdateKeyPerson(int applicationId, NewTrustKeyPerson person)
	{
		var updateTrustKeyPersonCommand = new UpdateTrustKeyPersonCommand()
		{
			ApplicationId = applicationId,
			KeyPersonId = person.Id,
			Name = person.Name,
			DateOfBirth = person.DateOfBirth,
			Biography = person.Biography,
			Roles = person.Roles
		};

		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/form-trust/key-person/{person.Id}?api-version=V1";
		await _resilientRequestProvider.PutAsync(apiurl, updateTrustKeyPersonCommand);
	}

	public Task DeleteKeyPerson(int applicationId, int keyPersonId)
	{
		throw new NotImplementedException();
	}

	///<inheritdoc/>
	public async Task RemoveSchoolFromApplication(int applicationId, int schoolUrn)
	{
		var application = await GetApplication(applicationId);

		if (application?.ApplicationId != applicationId)
		{
			throw new ArgumentException("Application not found");
		}

		var deleteSchoolCommand = new DeleteSchoolCommand
		{
			ApplicationId = applicationId,
			Urn = schoolUrn
		};

		// https://academies-academisation-api-dev.azurewebsites.net/application/99/form-trust/school/99
		string apiurl = $"{_httpClient.BaseAddress}application/{applicationId}/form-trust/school/{schoolUrn}";
		await _resilientRequestProvider.DeleteAsync<DeleteSchoolCommand>(apiurl, deleteSchoolCommand);
	}

	private async Task<ConversionApplication?> GetApplication(int applicationId)
	{
		return await _conversionApplicationRetrievalService.GetApplication(applicationId);
	}
}
