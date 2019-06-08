/*
Болотов Михаил
ДЗ Урок 8. Рефлексия, позднее связывание и атрибуты. Прохождение собеседования на вакансию "Разработчик на языке C#"
*/
using System;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "ДЗ к уроку 8. Рефлексия, позднее связывание и атрибуты. Прохождение собеседования на вакансию 'Разработчик на языке C#'";

			// Задача 1. Решение в файле Task1_Image.jpg

			// Задача 2
			try
			{
				Task2();
			}
			catch (InvalidCastException icEx)
			{
				Console.WriteLine("Ошибка в задаче 2: " + icEx.Message);
			}
			Console.WriteLine();

			// Задача 3. Решение в файле Task3_sqlQuery.sql

			// Задача 4
			Task4();

			// Задача 5. Решение в файле Task5_sqlQuery.sql

			Console.ReadLine();
		}

		private static void Task2()
		{
			Console.WriteLine("Задача 2");
			int i = 1;
			object obj = i;
			++i;
			Console.WriteLine(i);
			Console.WriteLine(obj);
			// будет ошибка, т.к. obj хранит Int32 и занимает в памяти 4 байта,
			// его невозможно распаковать (unboxing) в short, под который отводится лишь 2 байта памяти
			Console.WriteLine((short)obj);
		}

		private enum SomeEnum
		{
			First = 4,
			Second, // = 5, т.к. базовым типом для enum является int и по умолчанию значение следующего елемента увеличивается на 1 от предыдущего
			Third = 7
		}

		private static void Task4()
		{
			Console.WriteLine("Задача 4");
			Console.WriteLine((int)SomeEnum.Second);
			Console.WriteLine();
		}
	}

}
