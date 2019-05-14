using System.Net;
using System.Net.Mail;

namespace WpfTestMailSender
{
	public class EmailSendServiceClass
	{
		public EmailSendServiceClass()
		{
		}

		public void SendEmail()
		{
			using (var client = new SmtpClient(MailParams.Server, MailParams.Port))
			{
				client.Credentials = new NetworkCredential(MailParams.User, MailParams.Password);
				client.EnableSsl = true;

				using (var msg = new MailMessage(MailParams.User, MailParams.ToMail, MailParams.Subject, MailParams.Body))
				{
					msg.IsBodyHtml = false;

					client.Send(msg);
				}
			}
		}
	}
}
