using System.Collections.Generic;

namespace MailSender.lib.Services.Interfaces
{
	public interface IDataService<T>
	{
		IEnumerable<T> GetAll();

		T GetById(int id);

		int Add(T item);

		void Edit(T item);

		void Remove(int id);

		void SaveChanges();
	}
}
