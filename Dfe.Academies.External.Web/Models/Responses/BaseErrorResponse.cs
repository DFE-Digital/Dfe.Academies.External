using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models.Responses;

[DataContract]
public class BaseErrorResponse : BaseResponse
{
    /// <summary>
    /// The details of the error experienced.
    /// </summary>
    [DataMember]
    public string Error { get; set; }

    public BaseErrorResponse(string error)
    {
        Error = error;
    }
}
