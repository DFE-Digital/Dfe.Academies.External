namespace Dfe.Academies.External.Web.ViewModels;

public class FileUploadViewModel
{
	public List<string> FileNames { get; set; }
	public string FilePrefixSection { get; set; }
	public int ApplicationId { get; set; }
	public int Urn { get; set; }
	public Guid EntityId { get; set; }
	public string ApplicationReference { get; set; }
}
