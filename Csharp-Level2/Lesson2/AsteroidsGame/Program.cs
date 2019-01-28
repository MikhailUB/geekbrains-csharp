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
