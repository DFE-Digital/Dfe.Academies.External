namespace Dfe.Academies.External.Web.AcademiesAPIResponseModels;

public class EstablishmentResponse
{
    public EstablishmentResponse(string name, int urn, string? ukprn, string street, string town, string fullUkPostcode)
    {
	    Name = name;
	    Ukprn = ukprn;
	    Urn = urn;
	    Address = new AddressResponse(street: street, town:town, fullUkPostcode: fullUkPostcode);
    }

    public int Urn { get; set; }
    public string LocalAuthorityCode { get; set; }
    public string LocalAuthorityName { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
    public string OpenDate { get; set; }
    public string CloseDate { get; set; }
    public string StatutoryLowAge { get; set; }
    public string StatutoryHighAge { get; set; }
    public string NurseryProvision { get; set; }
    public string ReligiousEthos { get; set; }
    public string SchoolCapacity { get; set; }
    public string SchoolSponsorFlag { get; set; }
    public string SchoolSponsors { get; set; }
    public string FederationFlag { get; set; }
    public string Ukprn { get; set; }
    public string FeheiIdentifier { get; set; }
    public string FurtherEducationType { get; set; }
    public string OfstedLastInspection { get; set; }
    public string LastChangedDate { get; set; }
    public AddressResponse Address { get; set; }
    public string SchoolWebsite { get; set; }
    public string TelephoneNumber { get; set; }
    public string HeadteacherTitle { get; set; }
    public string HeadteacherFirstName { get; set; }
    public string HeadteacherLastName { get; set; }
    public string HeadteacherPreferredJobTitle { get; set; }
    public string InspectorateName { get; set; }
    public string InspectorateReport { get; set; }
    public string DateOfLastInspectionVisit { get; set; }
    public string DateOfNextInspectionVisit { get; set; }
    public string TeenMoth { get; set; }
    public string TeenMothPlaces { get; set; }
    public string CCF { get; set; }
    public string SENPRU { get; set; }
    public string EBD { get; set; }
    public string PlacesPRU { get; set; }
    public string FTProv { get; set; }
    public string EdByOther { get; set; }
    public string Section14Approved { get; set; }
    public string SEN1 { get; set; }
    public string SEN2 { get; set; }
    public string SEN3 { get; set; }
    public string SEN4 { get; set; }
    public string SEN5 { get; set; }
    public string SEN6 { get; set; }
    public string SEN7 { get; set; }
    public string SEN8 { get; set; }
    public string SEN9 { get; set; }
    public string SEN10 { get; set; }
    public string SEN11 { get; set; }
    public string SEN12 { get; set; }
    public string SEN13 { get; set; }
    public string TypeOfResourcedProvision { get; set; }
    public string ResourcedProvisionOnRoll { get; set; }
    public string ResourcedProvisionOnCapacity { get; set; }
    public string SenUnitOnRoll { get; set; }
    public string SenUnitCapacity { get; set; }

    public string GSSLACode { get; set; }
    public string Easting { get; set; }
    public string Northing { get; set; }
    public string CensusAreaStatisticWard { get; set; }

    public string SENStat { get; set; }
    public string SENNoStat { get; set; }
    public string BoardingEstablishment { get; set; }
    public string PropsName { get; set; }
    public string PreviousEstablishmentNumber { get; set; }
    public string OfstedRating { get; set; }
    public string RSCRegion { get; set; }
    public string Country { get; set; }
    public string UPRN { get; set; }
}
