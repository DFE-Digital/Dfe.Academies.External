namespace Dfe.Academies.External.Web.Models
{
    public class SchoolApplyingToConvert
    {
        public int Id { get; set; }

        /// <summary>
        /// This would be existing Id from GIAS (?). 6 digit URN?
        /// </summary>
        public int SchoolId { get; set; }

        public string? SchoolName { get; set; }

        public List<ConversionApplicationComponent> ConversionApplicationComponents { get; set; } = new();

        // TODO MR:- contact head

        // TODO MR:- contact chair

        // TODO MR:- contact main contact

        //// MR:- below props from A2C-SIP - ApplyingSchool object
        public bool? SchoolAdInspectedButReportNotPublished { get; set; }

        public string? SchoolAdInspectedReportNotPublishedExplain { get; set; }

        public bool? SchoolLocalAuthorityReorganisation { get; set; }

        public string? SchoolLocalAuthorityReorganisationExplain { get; set; }

        public bool? SchoolLocalAuthorityClosurePlans { get; set; }

        public string? SchoolLocalAuthorityClosurePlansExplain { get; set; }

        public bool? SchoolAdSafeguarding { get; set; }

        public string? SchoolAdSafeguardingExplain { get; set; }

        // TODO:- other props from A2C-SIP - ApplyingSchool object

        public List<SchoolLoan> SchoolLoans { get; set; } = new();

        public List<SchoolLease> SchoolLeases { get; set; } = new();
    }
}
