using Dfe.Academies.External.Web.Exceptions;
using Dfe.Academies.External.Web.Services;
using Quartz;

namespace Dfe.Academies.External.Web.Jobs;

public class FixSharepointFoldersJob : IJob
{
	private readonly IConversionApplicationRetrievalService _conversionApplicationRetrievalService;
	private readonly IFileUploadService _fileUploadService;
	private readonly ILogger<FixSharepointFoldersJob> _logger;
	public FixSharepointFoldersJob(IConversionApplicationRetrievalService conversionApplicationRetrievalService, IFileUploadService fileUploadService, ILogger<FixSharepointFoldersJob> logger)
	{
		_conversionApplicationRetrievalService = conversionApplicationRetrievalService;
		_fileUploadService = fileUploadService;
		_logger = logger;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		var sharepointApplicationServiceModels = await _conversionApplicationRetrievalService.GetAllApplications();
		foreach (var applicationSchoolSharepointServiceModel in sharepointApplicationServiceModels)
		{
			_logger.LogInformation($"Application sharepoint model: {applicationSchoolSharepointServiceModel.ApplicationReference}");
			foreach (var schoolSharepointServiceModel in applicationSchoolSharepointServiceModel.SchoolSharepointServiceModels.Where(x => x.EntityId != Guid.Empty))
			{
				_logger.LogInformation($"Fixing folder structure for application: {applicationSchoolSharepointServiceModel.ApplicationReference} with school: {schoolSharepointServiceModel.Name} :: ${schoolSharepointServiceModel.EntityId}");
				try
				{
					await _fileUploadService.FixApplyingSchool(applicationSchoolSharepointServiceModel.ApplicationReference, schoolSharepointServiceModel.EntityId.ToString());
				}
				catch (FileUploadException ex)
				{
					_logger.LogCritical(ex, $"Could not fix folder structure for application: {applicationSchoolSharepointServiceModel.ApplicationReference} with school: {schoolSharepointServiceModel.Name} :: ${schoolSharepointServiceModel.EntityId}");
					_logger.LogDebug($"{ex.StackTrace}");
				}
			}
		}
	}
}
