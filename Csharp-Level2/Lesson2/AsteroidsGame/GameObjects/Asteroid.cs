using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс реализующий Астероид
	/// </summary>
	class Asteroid : BaseObject
	{
		/// <summary>
		/// Энергия Астероида
		/// </summary>
		public int Power { get; set; }

		private Image _image = Game.GetImage("Asteroid.PNG");

		/// <summary>
		/// Инициализирует новый объект Астероида
		/// </summary>
		public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
			Power = 1;
		}

		/// <summary>
		/// Отрисовывает Астероид на игровой поверхности
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.DrawImage(_image, Pos.X, Pos.Y, Size.Width, Size.Height);
			//Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		/// <summary>
		/// Обновляет координалы расположения Астероида на игровой поверхности
		/// </summary>
		public override void Update()
		{
			Pos.X += Dir.X;
			Pos.Y += Dir.Y;

			if (Pos.X < 0 || Game.Width < Pos.X)
				Dir.X = -Dir.X;

			if (Pos.Y < 0 || Game.Height < Pos.Y)
				Dir.Y = -Dir.Y;
		}
	}
}
