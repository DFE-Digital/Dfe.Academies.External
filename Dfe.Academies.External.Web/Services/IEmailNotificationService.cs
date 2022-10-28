using Dfe.Academies.External.Web.Models.Notifications;

namespace Dfe.Academies.External.Web.Services
{
	public interface IEmailNotificationService
	{
		Task SendAsync(MessageDto message);
	}
}
