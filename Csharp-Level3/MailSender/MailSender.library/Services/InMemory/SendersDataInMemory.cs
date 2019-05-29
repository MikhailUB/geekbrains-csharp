using MailSender.lib.Data;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;

namespace MailSender.lib.Services.InMemory
{
	public class SendersDataInMemory : DataInMemory<Sender>, ISendersData
	{
		public SendersDataInMemory() => _items.AddRange(TestData.Senders);

		public override void Edit(Sender item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Name = item.Name;
			dbItem.Email = item.Email;
		}
	}
}
