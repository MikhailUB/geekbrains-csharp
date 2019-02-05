/*
Болотов Михаил
Урок 4. Объектно-ориентированное программирование. Часть 4
	
2. Дана коллекция List<T>.Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
	a. для целых чисел;
	b. *для обобщенной коллекции;
	c. **используя Linq.

3. *Дан фрагмент программы в методе Task3(): 
	а. Свернуть обращение к OrderBy с использованием лямбда-выражения =>.
	b. *Развернуть обращение к OrderBy с использованием делегата .
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson4
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Задача 2. Подсчет количества вхождений каждого элемента");
			Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight + 20);

			var intValues = new List<int> { 1, 2, 2, 3, 3, 5, 5, 5, 7, 7, 7 };
			Console.WriteLine($"Коллекция: {string.Join(", ", intValues)}");

			var intCounts = GetCountsOfEachElement_IntValues(intValues);
			Console.WriteLine($"Количество вхождений целых чисел");
			PrintDictionary(intCounts);

			Console.WriteLine();
			var stringValues = new List<string> { "a", "a", "a", "b", "b", "d", "d", "d", "c", "c", "f", "f", "x", "x", "x" };
			Console.WriteLine($"Коллекция: {string.Join(", ", stringValues)}");

			var strCounts = GetCountsOfEachElement_Generic(stringValues);
			Console.WriteLine($"Количество вхождений строк");
			PrintDictionary(strCounts);

			// Используем Linq
			strCounts = stringValues.GroupBy(value => value)
				.ToDictionary(group => group.Key, group => group.Count());
			Console.WriteLine();
			Console.WriteLine($"Количество вхождений строк с помощью Linq");
			PrintDictionary(strCounts);

			Console.WriteLine();
			Console.WriteLine("Задача 3. Делегаты и лямбда выражения");
			Task3();

			Console.ReadLine();
		}

		/// <summary>
		/// Метод считает количество вхождений каждого элемента для целых чисел
		/// </summary>
		/// <returns>Возвращает словарь Элемент - Количество его вхождений</returns>
		private static Dictionary<int, int> GetCountsOfEachElement_IntValues(List<int> values)
		{
			var elementCounts = new Dictionary<int, int>();
			foreach (var value in values)
			{
				elementCounts.TryGetValue(value, out int count);
				elementCounts[value] = count + 1;
			}
			return elementCounts;
		}

		/// <summary>
		/// Метод считает количество вхождений каждого элемента для обобщенной коллекции
		/// </summary>
		/// <typeparam name="T">Тип элементов</typeparam>
		/// <returns>Возвращает словарь Элемент - Количество его вхождений</returns>
		private static Dictionary<T, int> GetCountsOfEachElement_Generic<T>(List<T> values)
		{
			var elementCounts = new Dictionary<T, int>();
			foreach (var value in values)
			{
				elementCounts.TryGetValue(value, out int count);
				elementCounts[value] = count + 1;
			}
			return elementCounts;
		}

		private static void PrintDictionary<T>(Dictionary<T, int> elementCounts)
		{
			foreach (var pair in elementCounts)
			{
				Console.WriteLine($"Значение {pair.Key} = {pair.Value} элементов");
			}
		}

		private static void Task3()
		{
			var dict = new Dictionary<string, int>
			{
				{ "four", 4 },
				{ "two", 2 },
				{ "one", 1 },
				{ "three", 3 }
			};
			var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
			foreach (var pair in d)
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);

			// a. Свернуть обращение к OrderBy с использованием лямбда-выражения =>
			var a = dict.OrderBy(pair => pair.Value);
			Console.WriteLine();
			Console.WriteLine("ЛЯМБДА");
			foreach (var pair in a)
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);

			// b. *Развернуть обращение к OrderBy с использованием делегата
			var sortFunc = new SortFunc(SortSelector);
			var b = dict.OrderBy(pair => sortFunc(pair));
			Console.WriteLine();
			Console.WriteLine("ДЕЛЕГАТ");
			foreach (var pair in b)
				Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
		}

		delegate int SortFunc(KeyValuePair<string, int> pair);

		private static int SortSelector(KeyValuePair<string, int> pair)
		{
			return pair.Value;
		}
	}
}
