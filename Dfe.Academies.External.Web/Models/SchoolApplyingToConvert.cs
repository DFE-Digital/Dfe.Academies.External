namespace Dfe.Academies.External.Web.Models
{
    public class SchoolApplyingToConvert
    {
        public int? Id { get; set; }
        public string? SchoolName { get; set; }

        public List<ConversionApplicationComponent> SchoolApplicationComponents { get; set; } = new();
    }
}
