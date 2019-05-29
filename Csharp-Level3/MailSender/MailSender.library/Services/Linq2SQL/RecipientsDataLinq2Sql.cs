using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.lib.Services
{
	public class RecipientsDataLinq2Sql : IRecipientsData
	{
		private readonly Data.Linq2Sql.MailSenderDB _db;

		public RecipientsDataLinq2Sql(Data.Linq2Sql.MailSenderDB db)
		{
			_db = db;
		}

		public IEnumerable<Recipient> GetAll()
		{
			return _db.Recipient.Select(r => new Recipient { Id = r.Id, Name = r.Name, Email = r.Email }).ToList();
		}

		public Recipient GetById(int id)
		{
			var dbRecipient = _db.Recipient.FirstOrDefault(i => i.Id == id);
			return new Recipient
			{
				Id = dbRecipient.Id,
				Name = dbRecipient.Name,
				Email = dbRecipient.Email
			};
		}

		public int Add(Recipient recipient)
		{
			if (_db.Recipient.Any(r => r.Id == recipient.Id))
				return recipient.Id;

			_db.Recipient.InsertOnSubmit(new Data.Linq2Sql.Recipient
			{
				Name = recipient.Name,
				Email = recipient.Email
			});
			SaveChanges();

			return recipient.Id;
		}

		public void Edit(Recipient recipient)
		{
			var dbRecipient = _db.Recipient.FirstOrDefault(r => r.Id == recipient.Id);
			if (dbRecipient is null)
			{
				Add(recipient);
				return;
			}
			dbRecipient.Name = recipient.Name;
			dbRecipient.Email = recipient.Email;
			SaveChanges();
		}

		public void Remove(int id)
		{
			var dbRecipient = _db.Recipient.FirstOrDefault(r => r.Id == id);
			if (dbRecipient != null)
			{
				_db.Recipient.DeleteOnSubmit(dbRecipient);
				SaveChanges();
			}
		}

		public void SaveChanges() => _db.SubmitChanges();
	}
}
