namespace Dfe.Academies.External.Web.Models.Notifications
{
    public class MessageDto
    {
	    public MessageDto(string emailAddress, string templateId)
	    {
		    EmailAddress = emailAddress;
		    TemplateId = templateId;
	    }

		/// <summary>
		/// Recipients email
		/// </summary>
		public string EmailAddress { get; set; }

		/// <summary>
		/// GOV UK notify example as follows:- templateId: "f33517ff-2a88-4f6e-b855-c550268ce08a";
		/// </summary>
		public string TemplateId { get; set; }

		/// <summary>
		/// If a template has placeholder fields for personalised information such as name or reference number, you need to provide their values in a Dictionary
		/// </summary>
		public Dictionary<string, dynamic>? Personalisation { get; set; }

		/// <summary>
		/// A unique identifier you can create if you need to.
		/// This reference identifies a single unique notification or a batch of notifications.
		/// It must not contain any personal information such as name or postal address. 
		/// </summary>
		public string? Reference { get; set; }

		/// <summary>
		/// GOV UK notify example as follows:- string emailReplyToId: "8e222534-7f05-4972-86e3-17c5d9f894e2";
		/// </summary>
		public string? EmailReplyToId { get; set; }
	}
}
