using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class RecipientsListsDataInMemory : DataInMemory<RecipientsList>, IRecipientsListsData
	{
		public override void Edit(RecipientsList item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item)) return;

			dbItem.Name = item.Name;
			dbItem.Recipients = item.Recipients;
		}
	}
}
