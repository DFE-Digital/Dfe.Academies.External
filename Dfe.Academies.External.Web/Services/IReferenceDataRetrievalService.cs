namespace Dfe.Academies.External.Web.Services;

public interface IReferenceDataRetrievalService
{
	Task<IEnumerable<string>> SearchSchools(string inputText);
}