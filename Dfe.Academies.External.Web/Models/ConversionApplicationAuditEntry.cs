namespace Dfe.Academies.External.Web.Models;

    public class ConversionApplicationAuditEntry
    {
        public ConversionApplicationAuditEntry(string createdBy, string typeOfChange, string entityChanged, string propertyChanged)
        {
            CreatedBy = createdBy;
            TypeOfChange = typeOfChange;
            EntityChanged = entityChanged;
            PropertyChanged = propertyChanged;  
        }

        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        /// <summary>
        /// Add / Delete / Update / Read
        /// </summary>
        public string TypeOfChange { get; set; }

        /// <summary>
        /// E.g. Application
        /// </summary>
        public string EntityChanged { get; set; }

        public string PropertyChanged { get; set; }
    }

