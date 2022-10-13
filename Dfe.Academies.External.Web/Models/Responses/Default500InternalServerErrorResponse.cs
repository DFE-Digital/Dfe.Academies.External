using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models.Responses;

[DataContract]
public class Default500InternalServerErrorResponse : BaseErrorResponse
{
    public Default500InternalServerErrorResponse(string errorMessage)
        : base(errorMessage)
    {
    }
}
