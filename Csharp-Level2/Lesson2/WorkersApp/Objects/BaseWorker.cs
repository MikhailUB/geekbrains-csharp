using System;

namespace WorkersApp
{
	/// <summary>
	/// Базовый класс представляющий работника
	/// </summary>
	public abstract class BaseWorker : IComparable<BaseWorker>
	{
		/// <summary>
		/// ФИО
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Дата рождения
		/// </summary>
		public DateTime DateOfBirth { get; private set; }

		/// <summary>
		/// Инициализирует новый объект работника
		/// </summary>
		/// <param name="name">ФИО</param>
		/// <param name="dateOfBirth">Дата рождения</param>
		public BaseWorker(string name, DateTime dateOfBirth)
		{
			Name = name;
			DateOfBirth = dateOfBirth;
		}

		/// <summary>
		/// Строковое представление работника
		/// </summary>
		public override string ToString()
		{
			return $"{Name}   {DateOfBirth:d}   {CalcMonthlyPayment():N0}";
		}

		/// <summary>
		/// Расчитывает среднемесячную зарплату
		/// </summary>
		public abstract decimal CalcMonthlyPayment();

		/// <summary>
		/// Сравнивает по ФИО текущего работника с переданным и возвращает их относительную позицию при сортировке
		/// </summary>
		/// <param name="other">Работник для сравнения</param>
		/// <returns>Результат сравнения в виде числа</returns>
		public int CompareTo(BaseWorker other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}
