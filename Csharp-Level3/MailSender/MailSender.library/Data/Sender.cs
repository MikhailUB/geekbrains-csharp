namespace MailSender.lib.Data
{
	public class Sender
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}

	public class Server
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public int Port { get; set; }
		public bool UseSSL { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
	}

}
