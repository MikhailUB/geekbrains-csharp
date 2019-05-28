using MailSender.lib.Entityes.Base;
using MailSender.lib.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailSender.lib.Services.InMemory
{
	public abstract class DataInMemory<T> : IDataService<T> where T : BaseEntity
	{
		protected readonly List<T> _items = new List<T>();

		public IEnumerable<T> GetAll() => _items;

		public T GetById(int id)
		{
			if (id < 0)
				throw new ArgumentOutOfRangeException(nameof(id), id, "Значение id должно быть больше 0");

			return _items.FirstOrDefault(item => item.Id == id);
		}

		public int Add(T item)
		{
			if (_items.Contains(item))
				return 0;

			item.Id = _items.Count == 0 ? 1 : _items.Max(i => i.Id) + 1;
			_items.Add(item);

			return item.Id;
		}

		public abstract void Edit(T item);

		public void Remove(int id)
		{
			var item = GetById(id);
			if (item != null)
				_items.Remove(item);
		}

		public void SaveChanges() { }
	}
}
