using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс аптечки (ящика пополняющего энергию корабля)
	/// </summary>
	class EnergyBox : BaseObject
	{
		public EnergyBox(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
		}

		/// <summary>
		/// Отрисовка
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawRectangle(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
			Game.Buffer.Graphics.DrawString("E", new Font(FontFamily.GenericSansSerif, 18), Brushes.Red, Pos.X, Pos.Y-2);
		}

		/// <summary>
		/// Обновление положения
		/// </summary>
		public override void Update()
		{
			Pos.X -= Dir.X;
			if (Pos.X < -Size.Width)
			{
				Pos.X = Game.Width + Size.Width;
				Pos.Y += Dir.Y * 5;
			}
			if (Pos.X > (Game.Width + Size.Width))
			{
				Pos.X = -Size.Width;
				Pos.Y += Dir.Y * 5;
			}
			if (Pos.Y > Game.Height)
				Pos.Y = 0;
		}
	}
}
