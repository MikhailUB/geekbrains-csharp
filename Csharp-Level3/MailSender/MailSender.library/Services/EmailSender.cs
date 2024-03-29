﻿using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace MailSender.lib.Services
{
	public class SmtpMailSenderService : IMailSenderService
	{
		public IMailSender CreateSender(Server server)
		{
			return new SmtpMailSender(server.Address, server.Port, server.UseSSL, server.Login, server.Password);
		}
	}

	public class SmtpMailSender : IMailSender
	{
		private string _server;
		private int _port;
		private bool _useSsl;

		private string _login;
		private string _password;

		public SmtpMailSender(string server, int port, bool useSsl, string login, string pwd)
		{
			_server = server;
			_port = port;
			_useSsl = useSsl;
			_login = login;
			_password = pwd;
		}

		public void Send(Entityes.MailMessage message, Sender from, Recipient to)
		{
			using (var client = new SmtpClient(_server, _port))
			{
				client.Credentials = new NetworkCredential(_login, _password);
				client.EnableSsl = _useSsl;

				var fromAddress = new MailAddress(from.Email, from.Name);
				var toAddress = new MailAddress(to.Email, to.Name);
				using (var msg = new System.Net.Mail.MailMessage(fromAddress, toAddress))
				{
					msg.Subject = message.Subject;
					msg.Body = message.Body;
					msg.IsBodyHtml = false;

					client.Send(msg);
				}
			}
		}

		public void Send(Entityes.MailMessage message, Sender from, IEnumerable<Recipient> to)
		{
			foreach (var recipient in to)
				Send(message, from, recipient);
		}

		public void SendParallel(Entityes.MailMessage message, Sender from, IEnumerable<Recipient> to)
		{
			foreach (var recipient in to)
				ThreadPool.QueueUserWorkItem(_ => Send(message, from, recipient));
		}

		public async Task SendAsync(Entityes.MailMessage message, Sender From, Recipient To)
		{
			using (var client = new SmtpClient(_server, _port) { Credentials = new NetworkCredential(_login, _password) })
			using (var msg = new System.Net.Mail.MailMessage())
			{
				msg.From = new MailAddress(From.Email, From.Name);
				msg.To.Add(new MailAddress(To.Email, To.Name));
				msg.Subject = message.Subject;
				msg.Body = message.Body;

				await client.SendMailAsync(msg).ConfigureAwait(false);
			}
		}

		public async Task SendAsync(Entityes.MailMessage message, Sender from, IEnumerable<Recipient> to)
		{
			await Task.WhenAll(to.Select(rec => SendAsync(message, from, rec)));
		}
	}
}
