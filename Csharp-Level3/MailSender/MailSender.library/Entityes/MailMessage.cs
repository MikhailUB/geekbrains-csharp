using MailSender.lib.Entityes.Base;
using System.ComponentModel.DataAnnotations;

namespace MailSender.lib.Entityes
{
	public class MailMessage : BaseEntity
	{
		[Required, MaxLength(256)]
		public string Subject { get; set; }

		public string Body { get; set; }
	}
}
