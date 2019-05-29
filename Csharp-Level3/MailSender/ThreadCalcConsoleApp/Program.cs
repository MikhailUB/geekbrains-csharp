using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadCalcConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Параллельные вычисления";

			Console.WriteLine("Введите целое число в интервале от нуля до 20 для расчета факториала");
			var strValue = Console.ReadLine();
			var fctN = Convert.ToInt32(strValue);

			Console.WriteLine("Введите целое число для расчета суммы чисел от нуля до введенного");
			strValue = Console.ReadLine();
			var sumN = Convert.ToInt32(strValue);

			var task1 = new Task(() => { CalcFactorial(fctN); });
			var task2 = new Task(() => { CalcSumNumbers(sumN); });
			task1.Start();
			task2.Start();
			task1.Wait();
			task2.Wait();

			Console.WriteLine();
			Console.WriteLine("Нажмите любую клавишу для выхода");
			Console.Read();
		}

		private static void CalcFactorial(int value)
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException("Значение должно быть от нуля до 20", nameof(value));

			long result = 1;
			for (int i = 2; i <= value; i++)
			{
				result *= i;
			}
			Console.WriteLine($"Для числа {value} факториал = {result:N0}");
		}

		private static void CalcSumNumbers(int endNumber)
		{
			if (endNumber < 0)
				throw new ArgumentException("Значение не может быть меньше нуля", nameof(endNumber));

			var result = 0;
			for (int i = 1; i <= endNumber; i++)
			{
				result += i;
			}
			Console.WriteLine($"От нуля до {endNumber} сумма целых чисел = {result}");
		}
	}
}
