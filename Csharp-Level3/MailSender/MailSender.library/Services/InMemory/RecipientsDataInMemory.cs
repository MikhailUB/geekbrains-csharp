using MailSender.lib.Data;
using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Services.Interfaces;
using System.Linq;

namespace MailSender.lib.Services.InMemory
{
	public class RecipientsDataInMemory : DataInMemory<Recipient>, IRecipientsData
	{
		public RecipientsDataInMemory()
		{
			_items.AddRange(TestData.Senders.Select((s, i) => new Recipient
			{
				Id = i + 1,
				Name = s.Name,
				Email = s.Email
			}));
		}

		public override void Edit(Recipient item)
		{
			var dbItem = GetById(item.Id);
			if (dbItem is null || ReferenceEquals(dbItem, item))
				return;

			dbItem.Name = item.Name;
			dbItem.Email = item.Email;
		}
	}
}
