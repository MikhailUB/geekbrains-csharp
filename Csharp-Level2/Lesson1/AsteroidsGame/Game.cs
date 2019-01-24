/*
Болотов Михаил
Урок 1. Объектно-ориентированное программирование. Часть 1
1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полет в звездном пространстве.
2. *Заменить кружочки картинками, используя метод DrawImage.
*/
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
			for (int i = 0; i < _objs.Length -3 ; i++)
			{
				int yPos = i * Height / objsCount;
				if (i < _objs.Length / 2)
				{
					var side = rand.Next(3, 21);
					_objs[i] = new BaseObject(new Point(Width, yPos), new Point(-i / 2, (3 - i) / 2), new Size(side, side));
				}
				else
				{
					var xDir = ((i % 2 == 0) ? -i : i) / 2;
					var side = rand.Next(3, 8);
					_objs[i] = new Star(new Point(Width, yPos + 50), new Point(xDir, 0), new Size(side, side));
				}
			}
			_objs[objsCount - 3] = new Planet(new Point(Width - 300, 50), new Point(-1, 2), new Size(60, 60));
			_objs[objsCount - 2] = new Planet(new Point(Width - 400, 150), new Point(-1, 1), new Size(45, 45), Brushes.SkyBlue);
			_objs[objsCount - 1] = new Planet(new Point(Width - 500, 250), new Point(-2, 1), new Size(30, 30), Brushes.Coral);
		}

		public static void Draw()
		{
			// Проверяем вывод графики
			Buffer.Graphics.Clear(Color.Black);
			//Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(Width - 300, 50, 100, 100));

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
