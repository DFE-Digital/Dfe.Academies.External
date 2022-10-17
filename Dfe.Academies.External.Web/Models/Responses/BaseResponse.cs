using System.Runtime.Serialization;

namespace Dfe.Academies.External.Web.Models.Responses;

[DataContract]
public class BaseResponse
{
    internal bool SerializeDevOnlyInfo { get; set; }

    [DataMember(Name = "_DEV_ONLY_INFO_")]
    internal IDictionary<string, object> DevOnlyInfo { get; set; }

    public BaseResponse()
    {
        SerializeDevOnlyInfo = false;
    }

    public bool ShouldSerializeDevOnlyInfo()
        => SerializeDevOnlyInfo;
}

