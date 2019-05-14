using System;
using System.Windows;

namespace WpfTestMailSender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class WpfMailSender : Window
	{
		public WpfMailSender()
		{
			InitializeComponent();
		}

		private void SendButton_Click(object sender, RoutedEventArgs e)
		{
			const int defaultPort = 25;
			MailParams.Server = _server.Text;
			MailParams.Port = int.TryParse(_port.Text, out int port) ? port : defaultPort;
			MailParams.User = _user.Text;
			MailParams.Password = _passwordBox.SecurePassword;
			MailParams.ToMail = _toMail.Text;
			MailParams.Subject = _subject.Text;
			MailParams.Body = _message.Text;

			var sendService = new EmailSendServiceClass();
			try
			{
				sendService.SendEmail();
				var sew = new SendEndWindow() { Owner = this };
				sew.ShowDialog();
			}
			catch (Exception ex)
			{
				var ew = new ErrorWindow(ex.Message) { Owner = this };
				ew.ShowDialog();
			}

		}
	}
}
