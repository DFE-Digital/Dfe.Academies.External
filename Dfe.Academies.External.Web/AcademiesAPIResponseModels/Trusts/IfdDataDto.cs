using System.Text.Json.Serialization;

namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels.Trusts;

public sealed class IfdDataDto
{
	[JsonPropertyName("trustType")]
	public string TrustType { get; }

	[JsonPropertyName("trustContactPhoneNumber")]
	public string TrustContactPhoneNumber { get; }

	[JsonPropertyName("trustAddress")]
	public GroupContactAddressDto GroupContactAddress { get; }
	
	[JsonConstructor]
	public IfdDataDto(string trustType, string trustContactPhoneNumber, GroupContactAddressDto groupContactAddress) => 
		(TrustType, TrustContactPhoneNumber, GroupContactAddress) = (trustType, trustContactPhoneNumber, groupContactAddress);
}
