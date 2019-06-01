using System;
using System.Threading.Tasks;

namespace MatrixMultiplicationConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var m1 = CreateMatrix();
			var m2 = CreateMatrix();

			Console.WriteLine("Матрица 1");
			PrintMatrix(m1);
			Console.WriteLine("Матрица 2");
			PrintMatrix(m2);

			var result = Multiplication(m1, m2);
			Console.WriteLine("Результирующая матрица ");
			PrintMatrix(result, 5);

			Console.ReadLine();
		}

		private static int[,] CreateMatrix()
		{
			const int rowsCount = 100;
			const int colsCount = 100;
			const int upLimit = 11;
			var rand = new Random(Guid.NewGuid().GetHashCode());

			var matrix = new int[rowsCount, colsCount];
			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < colsCount; j++)
					matrix[i, j] = rand.Next(upLimit);
			}
			return matrix;
		}

		private static int[,] Multiplication(int[,] m1, int[,] m2)
		{
			var m1RowsCount = m1.GetLength(0);
			var result = new int[m1RowsCount, m2.GetLength(1)];

			var tasks = new Task[m1RowsCount];
			for (int i = 0; i < m1RowsCount; i++)
			{
				var idx = i;
				tasks[idx] = MultiplRowAsync(m1, m2, idx, result);
			}
			Task.WaitAll(tasks);

			return result;
		}

		private static async Task MultiplRowAsync(int[,] m1, int[,] m2, int rowIdx, int[,] result)
		{
			await Task.Run(() =>
				{
					for (int j = 0; j < m2.GetLength(1); j++)
					{
						for (int k = 0; k < m2.GetLength(0); k++)
						{
							result[rowIdx, j] += m1[rowIdx, k] * m2[k, j];
						}
					}
				}
			);
		}

		private static void PrintMatrix(int[,] matrix, int cellWidth = 3)
		{
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
					Console.Write(matrix[i, j].ToString().PadLeft(cellWidth));

				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}
