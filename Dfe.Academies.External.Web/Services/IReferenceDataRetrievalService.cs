using Dfe.Academies.External.Web.Models;
using Dfe.Academies.External.Web.ViewModels;

namespace Dfe.Academies.External.Web.Services;

public interface IReferenceDataRetrievalService
{
	Task<IList<SchoolSearchResultViewModel>> SearchSchools(SchoolSearch schoolSearch);
}