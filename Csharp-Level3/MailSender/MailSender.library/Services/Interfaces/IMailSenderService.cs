using MailSender.lib.Entityes;
using System.Collections.Generic;

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
	}
}
