using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
	/// <summary>
	/// Секция товаров
	/// </summary>
	public class Section : NamedEntity, IOrderedEntity
	{
		public int Order { get; set; }

		/// <summary>
		/// Идентификатор родительской секции
		/// </summary>
		public int? ParentId { get; set; }
	}
}
