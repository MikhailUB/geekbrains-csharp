using MailSender.lib.Data.Linq2Sql;
using System.Collections.Generic;

namespace MailSender.lib.Services.Interfaces
{
	public interface IRecipientsData
	{
		IEnumerable<Recipient> GetAll();
		int Create(Recipient recipient);
		void Write(Recipient recipient);
		void SaveChanges();
	}
}
