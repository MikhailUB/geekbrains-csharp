﻿using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
	/// <summary>
	/// Сущность
	/// </summary>
	public abstract class BaseEntity : IBaseEntity
	{
		public int Id { get; set; }
	}
}
