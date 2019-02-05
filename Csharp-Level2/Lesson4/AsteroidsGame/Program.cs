/*
Болотов Михаил
Урок 4. Объектно-ориентированное программирование. Часть 4
	
1. Добавить в программу коллекцию астероидов. Как только она заканчивается (все астероиды сбиты),
	формируется новая коллекция, в которой на один астероид больше.
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
