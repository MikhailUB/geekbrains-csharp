using MailSender.lib.Data.EF;
using MailSender.lib.Entityes;
using MailSender.lib.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.lib.Services
{
	public class RecipientsDataEF : IRecipientsData
	{
		private readonly MailSenderDBContext _db;

		public RecipientsDataEF(MailSenderDBContext db)
		{
			_db = db;
		}

		public IEnumerable<Recipient> GetAll()
		{
			return _db.Recipients.ToList();
		}

		public Recipient GetById(int id)
		{
			return _db.Recipients.FirstOrDefault(i => i.Id == id);
		}

		public int Add(Recipient recipient)
		{
			if (_db.Recipients.Any(r => r.Id == recipient.Id))
				return recipient.Id;

			_db.Recipients.Add(new Recipient
			{
				Name = recipient.Name,
				Email = recipient.Email
			});
			SaveChanges();

			return recipient.Id;
		}

		public void Edit(Recipient recipient)
		{
			var dbRecipient = _db.Recipients.FirstOrDefault(r => r.Id == recipient.Id);
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
			var dbRecipient = _db.Recipients.FirstOrDefault(r => r.Id == id);
			if (dbRecipient != null)
			{
				_db.Recipients.Remove(dbRecipient);
				SaveChanges();
			}
		}

		public void SaveChanges() => _db.SaveChanges();
	}
}
