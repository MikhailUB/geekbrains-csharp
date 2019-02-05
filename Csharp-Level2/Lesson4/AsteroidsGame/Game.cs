/*
Болотов Михаил
Урок 4. Объектно-ориентированное программирование. Часть 4

1. Добавить в программу коллекцию астероидов. Как только она заканчивается (все астероиды сбиты),
	формируется новая коллекция, в которой на один астероид больше.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс реализующий логику игры "Астероиды"
	/// </summary>
	static class Game
	{
		private delegate void WiteTolog(string message);

		private static BufferedGraphicsContext _context;
		public static BufferedGraphics Buffer;
		public static BaseObject[] _objs;

		private static int AsteroidsCount { get; set; } = 3;
		private static List<Asteroid> _asteroids;
		private static Ship _ship;
		private static List<Bullet> _bullets;
		private static EnergyBox _energyBox;

		private static Timer _timer = new Timer { Interval = 100 };
		public static Random Rnd = new Random();

		private static WiteTolog _logWriters;
		private static FileLogger _fileLog = new FileLogger(Path.Combine("Log", "log.txt"));

		private static int _width;
		private static int _height;

		/// <summary>
		/// Ширина игрового поля
		/// </summary>
		public static int Width
		{
			get => _width;
			set
			{
				ThrowIfOutOfRange(value);
				_width = value;
			}
		}

		/// <summary>
		/// Высота игрового поля
		/// </summary>
		public static int Height
		{
			get => _height;
			set
			{
				ThrowIfOutOfRange(value);
				_height = value;
			}
		}

		private static void ThrowIfOutOfRange(int widthOrHeight)
		{
			const int min = 0;
			const int max = 1000;
			if (widthOrHeight < min || max < widthOrHeight)
				throw new ArgumentOutOfRangeException("value", $"Ширина и высота игрового поля должны быть в интервале от {min} до {max}.");
		}

		static Game()
		{
		}

		/// <summary>
		/// Инициализирует графические инструменты отрисовки объектов игры
		/// </summary>
		/// <param name="form">Форма используемая как поверхность для рисования</param>
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

			form.KeyDown += Form_KeyDown;

			_timer.Tick += Timer_Tick;
			_timer.Start();

			_logWriters += msg => Console.WriteLine(DateTime.Now + ": " + msg);
			_logWriters += _fileLog.Write;
		}

		/// <summary>
		/// Обработчик нажития клавиш
		/// </summary>
		private static void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				var bullet = new Bullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 5), new Point(5, 0), new Size(4, 1));
				_bullets.Add(bullet);
			}
			if (e.KeyCode == Keys.Up)
				_ship.Up();

			if (e.KeyCode == Keys.Down)
				_ship.Down();
		}

		private static void Timer_Tick(object sender, EventArgs e)
		{
			Draw();
			Update();
		}

		/// <summary>
		/// Создает игровые объекты 
		/// </summary>
		public static void Load()
		{
			const int objsCount = 30;

			_objs = new BaseObject[objsCount];
			_ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(25, 15));
			_ship.EnergyChanged += WriteEventToLog;
			_bullets = new List<Bullet>();
			CreateEnergyBox();

			for (int i = 0; i < _objs.Length; i++)
			{
				int r = Rnd.Next(5, BaseObject.MaxDir);
				_objs[i] = new Star(new Point(Width, Rnd.Next(0, Height)), new Point(-r/3, r/3), new Size(4, 4));
			}
			CreateAsteroids();
			Ship.ShipDie += Finish;
			BaseObject.ObjCollision += WriteEventToLog;

			Console.Title = "Логирование";
			WriteEventToLog("Старт игры");
		}

		private static void CreateEnergyBox()
		{
			_energyBox = new EnergyBox(new Point(Width, Rnd.Next(0, Height)), new Point(4, 10), new Size(25, 25));
		}

		private static void CreateAsteroids()
		{
			if (_asteroids == null)
				_asteroids = new List<Asteroid>();
			else
				AsteroidsCount++;

			for (var i = 0; i < AsteroidsCount; i++)
			{
				int r = Rnd.Next(30, BaseObject.MaxDir);
				var ast = new Asteroid(new Point(Width - r, Rnd.Next(0, Height - r)), new Point(-r / 5, r / 2), new Size(r, r));
				_asteroids.Add(ast);
			}
		}

		/// <summary>
		/// Логирование событий
		/// </summary>
		/// <param name="message">Сообщение для записи</param>
		private static void WriteEventToLog(string message)
		{
			_logWriters?.Invoke(message);
		}

		/// <summary>
		/// Отрисовывает игровые объекты на игровой поверхности
		/// </summary>
		public static void Draw()
		{
			Buffer.Graphics.Clear(Color.Black);
			foreach (var obj in _objs)
			{
				obj.Draw();
			}
			foreach (var ast in _asteroids)
			{
				ast?.Draw();
			}
			foreach (var bullet in _bullets)
			{
				bullet.Draw();
			}
			_energyBox.Draw();
			_ship?.Draw();
			if (_ship != null)
			{
				Buffer.Graphics.DrawString($"Energy: {_ship.Energy} Shot down asteroids: {_ship.ShotDownAsteroidsCount}", SystemFonts.DefaultFont, Brushes.White, 0, 0);
			}
			Buffer.Render();
		}

		/// <summary>
		/// Обновляет положение игровых объектов 
		/// </summary>
		public static void Update()
		{
			foreach (var obj in _objs)
				obj.Update();

			for (int k = _bullets.Count - 1; k > -1; k--)
			{
				_bullets[k].Update();
				if (_bullets[k].Rect.Location.X > Width)
					_bullets.RemoveAt(k);
			}
			_energyBox.Update();
			if (_ship.Collision(_energyBox))
			{
				_ship.EnergyAdd(Rnd.Next(1, 10));
				System.Media.SystemSounds.Exclamation.Play();
				CreateEnergyBox();
			}
			for (int i = _asteroids.Count - 1; i > -1; i--)
			{
				_asteroids[i].Update();
				var asteroidRemoved = false;
				for (int k = _bullets.Count - 1; k > -1; k--)
				{
					if (_bullets[k].Collision(_asteroids[i]))
					{
						System.Media.SystemSounds.Hand.Play();
						_asteroids.RemoveAt(i);
						_bullets.RemoveAt(k);
						_ship.AddShotDownAsteroid();
						asteroidRemoved = true;
						break;
					}
				}
				if (asteroidRemoved)
					continue;

				if (!_ship.Collision(_asteroids[i]))
					continue;

				_ship.EnergyLow(Rnd.Next(1, 10));
				System.Media.SystemSounds.Asterisk.Play();

				if (_ship.Energy <= 0)
					_ship.Die();
			}
			if (_asteroids.Count == 0)
				CreateAsteroids();
		}

		public static void Finish()
		{
			_timer.Stop();
			Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
			Buffer.Render();
			WriteEventToLog("Конец игры");
		}

		/// <summary>
		/// Возвращает картинку из папки с ресурсами
		/// </summary>
		/// <param name="name">Имя файла картинки</param>
		internal static Image GetImage(string name)
		{
			const string folder = "Resources";
			return Image.FromFile(Path.Combine(folder, name));
		}
	}
}
