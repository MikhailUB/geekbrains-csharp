/*
Болотов Михаил
Урок 2. Объектно-ориентированное программирование. Часть 2

1. Построить три класса (базовый и 2 потомка), описывающих работников с почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок):
	a. Описать в базовом классе абстрактный метод для расчета среднемесячной заработной платы.
		Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата = 20.8 * 8 * почасовая ставка»;
		для работников с фиксированной оплатой: «среднемесячная заработная плата = фиксированная месячная оплата»;
	b. Создать на базе абстрактного класса массив сотрудников и заполнить его;
	c. *Реализовать интерфейсы для возможности сортировки массива, используя Array.Sort();
	d. *Создать класс, содержащий массив сотрудников, и реализовать возможность вывода данных с использованием foreach.
*/

using System;

namespace WorkersApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Классы работников";

			var workersArray = new BaseWorker[10];
			for (int i = 0; i < workersArray.Length; i++)
			{
				var name = $"Работник{12 - i}";
				var dateOfBirth = new DateTime(1990 + i, i + 1, i + 1);
				if (i % 2 == 0)
					workersArray[i] = new WorkerWithHourlyPayment(name, dateOfBirth, 500 + (50 * i));
				else
					workersArray[i] = new WorkerWithFixedPayment(name, dateOfBirth, 70000 + (160 * i));
			}
			Console.WriteLine("Массив до сортировки");
			foreach (var worker in workersArray)
				Console.WriteLine(worker);

			Array.Sort(workersArray);

			// Переносим отсортированный массив с объект Workers
			var workers = new Workers(workersArray.Length);
			for (int i = 0; i < workersArray.Length; i++)
			{
				workers[i] = workersArray[i];
			}
			Console.WriteLine();
			Console.WriteLine("После сортировки и переноса в класс Workers реализующий интрефейс IEnumerable");
			foreach (var worker in workers)
				Console.WriteLine(worker);

			Console.ReadLine();
		}
	}
}
