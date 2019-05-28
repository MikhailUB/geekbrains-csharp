using MailSender.lib.Data.Linq2Sql;
using MailSender.lib.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.lib.Services
{
	public class RecipientsDataLinq2Sql : IRecipientsData
	{
		private readonly MailSenderDB _db;

		public RecipientsDataLinq2Sql(MailSenderDB db)
		{
			_db = db;
		}

		public IEnumerable<Recipient> GetAll()
		{
			return _db.Recipient.ToArray();
		}

		public Recipient GetById(int id) => _db.Recipient.FirstOrDefault(i => i.Id == id);

		public int Add(Recipient recipient)
		{
			if (recipient.Id != 0)
				return recipient.Id;

			_db.Recipient.InsertOnSubmit(recipient);
			SaveChanges();
			return recipient.Id;
		}

		public void Edit(Recipient recipient)
		{
			if (!_db.Recipient.Contains(recipient))
				_db.Recipient.InsertOnSubmit(recipient);
		}

		public void Remove(int id)
		{
			var item = GetById(id);
			_db.Recipient.DeleteOnSubmit(item);
			SaveChanges();
		}

		public void SaveChanges() => _db.SubmitChanges();
	}
}
