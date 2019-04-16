using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
	/// <summary>
	/// Сущность
	/// </summary>
	public abstract class BaseEntity : IBaseEntity
	{
		[Key] // признак первичного ключа
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // БД будет устанавливать значение при добавлении записи в таблицу
		public int Id { get; set; }
	}
}
