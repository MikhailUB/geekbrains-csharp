using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Entityes;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace MailSender.lib
{
	public class EmailSender
	{
		private string _server;
		private int _port;
		private bool _useSsl;

		private string _login;
		private string _password;

		public EmailSender(string server, int port, bool useSsl, string login, string pwd)
		{
			_server = server;
			_port = port;
			_useSsl = useSsl;
			_login = login;
			_password = pwd;
		}

		public void SendEmail(string to, string subject, string body)
		{
			using (var client = new SmtpClient(_server, _port))
			{
				client.Credentials = new NetworkCredential(_login, _password);
				client.EnableSsl = _useSsl;

				using (var msg = new System.Net.Mail.MailMessage(_login, to, subject, body))
				{
					msg.IsBodyHtml = false;

					client.Send(msg);
				}
			}
		}

		public void SendMails(IEnumerable<Recipient> recipients, Entityes.MailMessage message)
		{
			foreach (var recipient in recipients)
				SendEmail(recipient.Email, message.Subject, message.Body);
		}

	}
}
