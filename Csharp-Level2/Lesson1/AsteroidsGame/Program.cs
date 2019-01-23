using System;
using System.Windows.Forms;

namespace AsteroidsGame
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			var form = new Form
			{
				Width = 1200,
				Height = 800,
				Text = "Астероиды"
			};
			Game.Init(form);
			form.Show();
			Game.Draw();

			Application.Run(form);
		}
	}
}
