using System.Collections.Generic;
using MailSender.lib.Entityes.Base;

namespace MailSender.lib.Entityes
{
	public class MailsList : BaseEntity
	{
		public string Name { get; set; }

		public IEnumerable<MailMessage> Messages { get; set; }
	}
}
