using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace MailSender.lib
{
	public class MailScheduler
	{
		private DispatcherTimer _timer = new DispatcherTimer();
		private DateTime _dateSend;
		private IMailSender _emailSender;
		private Sender _from;
		private ObservableCollection<Recipient> _recipients;
		private MailMessage _message;

		public MailScheduler(IMailSender emailSender)
		{
			_emailSender = emailSender;
		}

		public void SendEmails(DateTime dateSend, Sender from, ObservableCollection<Recipient> recipients, MailMessage message)
		{
			_dateSend = dateSend;
			_from = from;
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
				_emailSender.Send(_message, _from, _recipients);
			}
		}
	}
}
