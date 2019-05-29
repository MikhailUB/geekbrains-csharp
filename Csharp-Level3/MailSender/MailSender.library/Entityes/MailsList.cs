using System.Collections.Generic;
using MailSender.lib.Entityes.Base;

namespace MailSender.lib.Entityes
{
	public class MailsList : NamedEntity
	{
		public IEnumerable<MailMessage> Messages { get; set; }
	}
}
