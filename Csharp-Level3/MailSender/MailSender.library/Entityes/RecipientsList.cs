using System.Collections.Generic;
using MailSender.lib.Entityes.Base;

namespace MailSender.lib.Entityes
{
	public class RecipientsList : NamedEntity
	{
		public IEnumerable<Recipient> Recipients { get; set; }
	}

	public class Recipient : Human { }
}
