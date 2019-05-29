using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class MailsListDataInMemory : DataInMemory<MailsList>, IMailsListsData
	{
		public override void Edit(MailsList item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Name = item.Name;
			dbItem.Messages = item.Messages;
		}
	}
}
