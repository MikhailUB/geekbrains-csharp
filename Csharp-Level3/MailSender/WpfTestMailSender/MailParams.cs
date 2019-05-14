using System.Security;

namespace WpfTestMailSender
{
	public static class MailParams
	{
		public static string Server { get; set; }
		public static int Port { get; set; }
		public static string User { get; set; }
		public static SecureString Password { get; set; }
		public static string ToMail { get; set; }
		public static string Subject { get; set; }
		public static string Body { get; set; }
	}
}
