using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Entityes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace MailSender.lib
{
	public class MailScheduler
	{
		private DispatcherTimer _timer = new DispatcherTimer();
		private DateTime _dateSend;
		private EmailSender _emailSender;
		private ObservableCollection<Recipient> _recipients;
		private MailMessage _message;

		public MailScheduler(EmailSender emailSender)
		{
			_emailSender = emailSender;
		}

		public void SendEmails(DateTime dateSend, ObservableCollection<Recipient> recipients, MailMessage message)
		{
			_dateSend = dateSend;
			_recipients = recipients;
			_message = message;

			_timer.Tick += Timer_Tick;
			_timer.Interval = new TimeSpan(0, 0, 1);
			_timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			if (_dateSend.ToShortTimeString() == DateTime.Now.ToShortTimeString())
			{
				_timer.Stop();
				_emailSender.SendMails(_recipients, _message);
			}
		}
	}
}
