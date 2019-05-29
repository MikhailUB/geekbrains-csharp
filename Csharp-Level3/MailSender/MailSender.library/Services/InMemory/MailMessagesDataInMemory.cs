using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class MailMessagesDataInMemory : DataInMemory<MailMessage>, IMailMessagesData
	{
		public override void Edit(MailMessage item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Subject = item.Subject;
			dbItem.Body = item.Body;
		}
	}
}
