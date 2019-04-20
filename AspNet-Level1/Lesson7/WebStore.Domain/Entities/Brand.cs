using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	/// <summary>
	/// Бренд
	/// </summary>
	[Table("Brands")] // имя таблицы
	public class Brand : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }

		// virtual - указание Entity Framework что свойство навигационное, т.е. связь с другой таблицей
		public virtual ICollection<Product> Products { get; set; } = new List<Product>();
	}
}
