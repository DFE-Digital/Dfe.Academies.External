namespace Dfe.Academies.External.Web.Services;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// No-operation implementation of IFileUploadService for temporary use when file storage is unavailable.
/// This service allows the application to function without SharePoint connectivity while appearing to succeed to the UI.
/// </summary>
[ExcludeFromCodeCoverage]
public class NoOpFileUploadService : IFileUploadService
{
	private readonly ILogger<NoOpFileUploadService> _logger;

	public NoOpFileUploadService(ILogger<NoOpFileUploadService> logger)
	{
		_logger = logger;
	}

	public Task<List<string>> GetFiles(string entityName, string recordId, string recordName, string fieldName)
	{
		_logger.LogWarning("NoOpFileUploadService.GetFiles called - returning empty list. File storage is temporarily disabled.");
		return Task.FromResult(new List<string>());
	}

	public Task<string> UploadFile(string entity, string recordId, string recordName, string fieldName, IFormFile file)
	{
		_logger.LogWarning("NoOpFileUploadService.UploadFile called for file '{FileName}' - operation skipped. File storage is temporarily disabled.", file.FileName);
		return Task.FromResult(string.Empty);
	}

	public Task DeleteFile(string entityName, string recordId, string recordName, string fieldName, string fileName)
	{
		_logger.LogWarning("NoOpFileUploadService.DeleteFile called for file '{FileName}' - operation skipped. File storage is temporarily disabled.", fileName);
		return Task.CompletedTask;
	}

	public Task FixApplyingSchool(string appReference, string schoolEntityId)
	{
		_logger.LogWarning("NoOpFileUploadService.FixApplyingSchool called - operation skipped. File storage is temporarily disabled.");
		return Task.CompletedTask;
	}
}
