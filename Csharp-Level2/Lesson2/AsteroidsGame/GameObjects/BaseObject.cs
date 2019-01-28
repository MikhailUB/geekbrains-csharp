using System;
using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Базовый класс для игровых объектов
	/// </summary>
	abstract class BaseObject : ICollision
	{
		/// <summary>
		/// Позиция объекта
		/// </summary>
		protected Point Pos;
		
		/// <summary>
		/// Направление и скорость движения 
		/// </summary>
		protected Point Dir;
		
		/// <summary>
		/// Размер объекта
		/// </summary>
		protected Size Size;
		
		/// <summary>
		/// Максимальная разрешенная скорость объекта
		/// </summary>
		internal const int MaxDir = 50;

		/// <summary>
		/// Инициализирует новый игровой объект
		/// </summary>
		/// <param name="pos">Начальная позиция</param>
		/// <param name="dir">Направление и скорость движения</param>
		/// <param name="size">Размер объекта</param>
		protected BaseObject(Point pos, Point dir, Size size)
		{
			if (pos.X < 0 || Game.Width < pos.X)
				throw new GameObjectException($"X начальной позиции объекта (pos.X) должна быть в интервале от {0} до {Game.Width}.", "pos");

			if (pos.Y < 0 || Game.Height < pos.Y)
				throw new GameObjectException($"Y начальной позиции объекта (pos.Y) должна быть в интервале от {0} до {Game.Height}.", "pos");

			if (dir.X > MaxDir || dir.Y > MaxDir)
				throw new GameObjectException($"X и Y направления движения (dir.X и dir.Y) должны быть не больше {MaxDir}.", "dir");

			if (size.Width < 0 || size.Height < 0)
				throw new GameObjectException("Размеры объекта должны быть положительными.", "size");

			Pos = pos;
			Dir = dir;
			Size = size;
		}

		/// <summary>
		/// Отрисовывает объект на игровой поверхности
		/// </summary>
		public abstract void Draw();

		/// <summary>
		/// Обновляет положение объекта
		/// </summary>
		public abstract void Update();

		/// <summary>
		/// Смещает объект на левый край игровой поверхности
		/// </summary>
		public virtual void MoveToLeft()
		{
			Pos.X = 0;
		}

		/// <summary>
		/// Смещает объект на правый край игровой поверхности
		/// </summary>
		public virtual void MoveToRight()
		{
			Pos.X = Game.Width;
		}

		public bool Collision(ICollision obj)
		{
			return obj.Rect.IntersectsWith(Rect);
		}

		public Rectangle Rect => new Rectangle(Pos, Size);
	}
}
