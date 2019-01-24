using AsteroidsGame.Properties;
using System.Drawing;

namespace AsteroidsGame
{
	class BaseObject
	{
		protected Point Pos;
		protected Point Dir;
		protected Size Size;

		public BaseObject(Point pos, Point dir, Size size)
		{
			Pos = pos;
			Dir = dir;
			Size = size;
		}

		public virtual void Draw()
		{
			//Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
			Game.Buffer.Graphics.DrawImage(Resources.Asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		public virtual void Update()
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
