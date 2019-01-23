using System;
using System.Drawing;
using System.Windows.Forms;

namespace AsteroidsGame
{
	static class Game
	{
		private static BufferedGraphicsContext _context;
		public static BufferedGraphics Buffer;
		public static BaseObject[] _objs;

		// Свойства
		// Ширина и высота игрового поля
		public static int Width { get; set; }
		public static int Height { get; set; }

		static Game()
		{
		}

		public static void Init(Form form)
		{
			// Предоставляет доступ к главному буферу графического контекста для текущего приложения
			_context = BufferedGraphicsManager.Current;
			// Графическое устройство для вывода графики
			var g = form.CreateGraphics();
			// Создаем объект (поверхность рисования) и связываем его с формой
			// Запоминаем размеры формы
			Width = form.ClientSize.Width;
			Height = form.ClientSize.Height;
			// Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
			Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

			Load();

			var timer = new Timer { Interval = 100 };
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

		public static void Load()
		{
			const int objsCount = 30;
			var rand = new Random();

			_objs = new BaseObject[objsCount];
			for (int i = 0; i < _objs.Length / 2; i++)
			{
				var side = rand.Next(3, 11);
				_objs[i] = new BaseObject(new Point(Width, i * Height / objsCount), new Point(-i, -i), new Size(side, side));
			}
			for (int i = _objs.Length / 2; i < _objs.Length; i++)
			{
				var side = rand.Next(3, 7);
				_objs[i] = new Star(new Point(Width, i * Height / objsCount), new Point(-i, 0), new Size(side, side));
			}
		}

		public static void Draw()
		{
			// Проверяем вывод графики
			Buffer.Graphics.Clear(Color.Black);
			Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(Width - 300, 50, 100, 100));

			foreach (var obj in _objs)
			{
				obj.Draw();
			}
			Buffer.Render();
		}

		public static void Update()
		{
			foreach (var obj in _objs)
			{
				obj.Update();
			}
		}
	}
}
