using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс реализующий Звезду
	/// </summary>
	class Star : BaseObject
	{
		public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
		}

		/// <summary>
		/// Отрисовывает звезду
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width/2, Pos.Y, Pos.X + Size.Width / 2, Pos.Y + Size.Height);
		}

		/// <summary>
		/// Обновляет положение звезды
		/// </summary>
		public override void Update()
		{
			Pos.X += Dir.X;
			if (Pos.X < -Size.Width)
				Pos.X = Game.Width + Size.Width;

			if (Pos.X > (Game.Width + Size.Width))
				Pos.X = -Size.Width;
		}
	}
}
