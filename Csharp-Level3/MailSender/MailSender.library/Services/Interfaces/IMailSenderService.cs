using MailSender.lib.Entityes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailSender.lib.Services.Interfaces
{
	public interface IMailSenderService
	{
		IMailSender CreateSender(Server server);
	}

	public interface IMailSender
	{
		void Send(MailMessage message, Sender from, Recipient to);

		void Send(MailMessage message, Sender from, IEnumerable<Recipient> to);

		void SendParallel(MailMessage message, Sender from, IEnumerable<Recipient> to);

		Task SendAsync(MailMessage message, Sender from, Recipient to);

		Task SendAsync(MailMessage message, Sender from, IEnumerable<Recipient> to);
	}
}
