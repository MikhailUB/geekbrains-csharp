using MailSender.lib.Entityes.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entityes
{
	public class Server : NamedEntity
	{
		[Required]
		public string Address { get; set; }
		[DefaultValue(25)]
		public int Port { get; set; }
		public bool UseSSL { get; set; }
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
