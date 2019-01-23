using System.Drawing;

namespace AsteroidsGame
{
	class Star : BaseObject
	{
		public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
		}

		public override void Draw()
		{
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
			Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width/2, Pos.Y, Pos.X + Size.Width / 2, Pos.Y + Size.Height);
		}

		public override void Update()
		{
			Pos.X += Dir.X;
			if (Pos.X < 0)
				Pos.X = Game.Width + Size.Width;
		}
	}
}
