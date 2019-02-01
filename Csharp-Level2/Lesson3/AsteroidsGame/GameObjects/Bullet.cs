using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс пули
	/// </summary>
	class Bullet : BaseObject
	{
		/// <summary>
		/// Инициализирует новый объект пули
		/// </summary>
		public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
		}

		/// <summary>
		/// Отрисовывает пулю
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		/// <summary>
		/// Обновляет положение пули
		/// </summary>
		public override void Update()
		{
			Pos.X += 5;
			if (Pos.X > Game.Width)
			{
				Pos.X = 0;
				Pos.Y += 30;
				if (Pos.Y > Game.Height)
					Pos.Y -= Game.Height;
			}
		}

	}
}
