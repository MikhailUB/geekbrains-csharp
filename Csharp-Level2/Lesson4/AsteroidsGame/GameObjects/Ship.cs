using System.Drawing;

namespace AsteroidsGame
{
	/// <summary>
	/// Класс Корабль
	/// </summary>
	class Ship : BaseObject
	{
		private int _energy = 100;
		/// <summary>
		/// Количество энергии 
		/// </summary>
		public int Energy => _energy;
		/// <summary>
		/// Количество сбитых астероидов
		/// </summary>
		public int ShotDownAsteroidsCount { get; private set; }

		/// <summary>
		/// Событие происходит при разрушении корабля
		/// </summary>
		public static event Message ShipDie;
		/// <summary>
		/// Происходит при изменении энергии корабля
		/// </summary>
		public event MessageExt EnergyChanged;

		/// <summary>
		/// Уменьшить энергию
		/// </summary>
		/// <param name="n">Уменьшаемое количество</param>
		public void EnergyLow(int n)
		{
			_energy -= n;
			EnergyChanged?.Invoke($"Повреждение корабля на {n} единиц.");
		}

		/// <summary>
		/// Прибавить энергию
		/// </summary>
		/// <param name="n">Добавляемое количество</param>
		public void EnergyAdd(int n)
		{
			_energy += n;
			EnergyChanged?.Invoke($"Восстановление корабля на {n} единиц.");
		}

		/// <summary>
		/// Добавить сбитый астероид
		/// </summary>
		public void AddShotDownAsteroid()
		{
			ShotDownAsteroidsCount++;
		}

		/// <summary>
		/// Инициализирует новый объект корабля
		/// </summary>
		public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
		{
		}

		/// <summary>
		/// Отрисовывает корабль
		/// </summary>
		public override void Draw()
		{
			Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
		}

		/// <summary>
		/// Обновляет положение корабля
		/// </summary>
		public override void Update()
		{
		}

		/// <summary>
		/// Двигаться вверх
		/// </summary>
		public void Up()
		{
			if (Pos.Y > 0)
				Pos.Y -= Dir.Y;
		}

		/// <summary>
		/// Двигаться вниз
		/// </summary>
		public void Down()
		{
			if (Pos.Y < Game.Height - Size.Height)
				Pos.Y += Dir.Y;
		}

		/// <summary>
		/// Разрушить корабль
		/// </summary>
		public void Die()
		{
			ShipDie?.Invoke();
		}
	}
}
