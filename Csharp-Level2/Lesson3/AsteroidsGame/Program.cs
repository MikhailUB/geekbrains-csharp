/*
Болотов Михаил
Урок 3. Объектно-ориентированное программирование. Часть 3
	
1. Добавить космический корабль, как описано в уроке.
2. Доработать игру «Астероиды»:
	a. Добавить ведение журнала в консоль с помощью делегатов;
	b. *добавить это и в файл.
3. Разработать аптечки, которые добавляют энергию.
4. Добавить подсчет очков за сбитые астероиды.
*/
using System;
using System.Windows.Forms;

namespace AsteroidsGame
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа в приложение
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			var form = new Form
			{
				Width = 1000,
				Height = 800,
				Text = "Астероиды"
			};
			Game.Init(form);
			form.Show();
			Game.Load();
			Game.Draw();

			Application.Run(form);
		}
	}
}
