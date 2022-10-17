using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models.Responses;

[DataContract]
public class Default501NotImplementedResponse : BaseErrorResponse
{
    public Default501NotImplementedResponse(NotImplementedException ex)
        : base(ex.Message)
    {
    }
}
