using System.Drawing;

namespace AsteroidsGame
{
	class Planet : BaseObject
	{
		private Brush Brush { get; set; }

		public Planet(Point pos, Point dir, Size size) : this(pos, dir, size, Brushes.Wheat)
		{
		}

		public Planet(Point pos, Point dir, Size size, Brush brush) : base(pos, dir, size)
		{
			Brush = brush;
		}

		public override void Draw()
		{
			Game.Buffer.Graphics.FillEllipse(Brush, new Rectangle(Pos, Size));
		}

		public override void Update()
		{
			Pos.X += Dir.X;
			Pos.Y += Dir.Y;

			if (Pos.X < -Size.Width)
				Pos.X = Game.Width + Size.Width;

			if (Pos.X > (Game.Width + Size.Width))
				Pos.X = -Size.Width;

			if (Pos.Y < -Size.Height)
				Pos.Y = Game.Height + Size.Height;

			if (Pos.Y > (Game.Height + Size.Height))
				Pos.Y = -Size.Height;
		}

	}
}
