/*
Болотов Михаил
Урок 2. Объектно-ориентированное программирование. Часть 2

2. Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в наследниках.
3. Сделать так, чтобы при столкновении пули с астероидом они регенерировались в разных концах экрана.
4. Сделать проверку на задание размера экрана в классе Game. Если высота или ширина (Width, Height) больше 1000
	или принимает отрицательное значение, выбросить исключение ArgumentOutOfRangeException().
5.*Создать собственное исключение GameObjectException, которое появляется при попытке  создать объект
	с неправильными характеристиками (например, отрицательные размеры, слишком большая скорость или неверная позиция).
 */
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс реализующий логику игры "Астероиды"
	/// </summary>
	static class Game
	{
		private static BufferedGraphicsContext _context;
		public static BufferedGraphics Buffer;
		public static BaseObject[] _objs;

		private static Bullet _bullet;
		private static Asteroid[] _asteroids;

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

			var timer = new Timer { Interval = 100 };
			timer.Tick += Timer_Tick;
			timer.Start();
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
			const int astsCount = 3;

			_objs = new BaseObject[objsCount];
			_bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
			_asteroids = new Asteroid[astsCount];

			var rnd = new Random();
			for (int i = 0; i < _objs.Length; i++)
			{
				int r = rnd.Next(5, BaseObject.MaxDir);
				_objs[i] = new Star(new Point(Width, rnd.Next(0, Height)), new Point(-r/3, r/3), new Size(4, 4));
			}
			for (var i = 0; i < _asteroids.Length; i++)
			{
				int r = rnd.Next(10, BaseObject.MaxDir);
				_asteroids[i] = new Asteroid(new Point(Width, rnd.Next(0, Height)), new Point(-r / 5, r / 2), new Size(r, r));
			}

			/*
			for (int i = 0; i < _objs.Length -3 ; i++)
			{
				int yPos = i * Height / objsCount;
				if (i < _objs.Length / 2)
				{
					var side = rnd.Next(3, 21);
					_objs[i] = new Asteroid(new Point(Width, yPos), new Point(-i / 2, (3 - i) / 2), new Size(side, side));
				}
				else
				{
					var xDir = ((i % 2 == 0) ? -i : i) / 2;
					var side = rnd.Next(3, 8);
					_objs[i] = new Star(new Point(Width, yPos + 50), new Point(xDir, 0), new Size(side, side));
				}
			}
			_objs[objsCount - 3] = new Planet(new Point(Width - 300, 50), new Point(-1, 2), new Size(60, 60));
			_objs[objsCount - 2] = new Planet(new Point(Width - 400, 150), new Point(-1, 1), new Size(45, 45), Brushes.SkyBlue);
			_objs[objsCount - 1] = new Planet(new Point(Width - 500, 250), new Point(-2, 1), new Size(30, 30), Brushes.Coral);
			*/
		}

		/// <summary>
		/// Отрисовывает игровые объекты на игровой поверхности
		/// </summary>
		public static void Draw()
		{
			// Проверяем вывод графики
			Buffer.Graphics.Clear(Color.Black);
			//Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(Width - 300, 50, 100, 100));

			foreach (var obj in _objs)
				obj.Draw();
			foreach (var ast in _asteroids)
				ast.Draw();

			_bullet.Draw();

			Buffer.Render();
		}

		/// <summary>
		/// Обновляет положение игровых объектов 
		/// </summary>
		public static void Update()
		{
			foreach (var obj in _objs)
				obj.Update();

			foreach (var ast in _asteroids)
			{
				ast.Update();
				if (ast.Collision(_bullet))
				{
					System.Media.SystemSounds.Hand.Play();
					ast.MoveToRight();
					_bullet.MoveToLeft();
				}
			}
			_bullet.Update();
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
