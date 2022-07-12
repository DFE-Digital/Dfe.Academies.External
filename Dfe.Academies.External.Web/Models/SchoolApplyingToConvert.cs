namespace Dfe.Academies.External.Web.Models
{
    public class SchoolApplyingToConvert
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }

        public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; } = new();

        // TODO MR:- contact head

        // TODO MR:- contact chair

        // TODO MR:- contact main contact

        //// MR:- below props from A2C-SIP - ApplyingSchool object
        public bool? SchoolAdInspectedButReportNotPublished { get; set; }

        public string? SchoolAdInspectedReportNotPublishedExplain { get; set; }

        public bool? SchoolLAReorganisation { get; set; }

        public string? SchoolLAReorganisationExplain { get; set; }

        public bool? SchoolLAClosurePlans { get; set; }

        public string? SchoolLAClosurePlansExplain { get; set; }

        public bool? SchoolAdSafeguarding { get; set; }

        public string? SchoolAdSafeguardingExplain { get; set; }

        // TODO:- other props from A2C-SIP - ApplyingSchool object

        public List<SchoolLoan> SchoolLoans { get; set; } = new();

        public List<SchoolLease> SchoolLeases { get; set; } = new();
    }
}
