using MailSender.Components;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MailSender
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ExitOnClick(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void OnLeftButtonClick(object sender, EventArgs e)
		{
			if (!(sender is TabItemsControl tabControl))
				return;

			if (tabControl.LeftButtonEnabled)
				MainTabControl.SelectedIndex--;
		}

		private void OnRightButtonClick(object sender, EventArgs e)
		{
			if (!(sender is TabItemsControl tabControl))
				return;

			if (tabControl.RightButtonEnabled)
				MainTabControl.SelectedIndex++;
		}

		private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LeftRightButtons.LeftButtonEnabled = MainTabControl.SelectedIndex > 0;
			LeftRightButtons.RightButtonEnabled = MainTabControl.SelectedIndex < MainTabControl.Items.Count - 1;
		}

		private void OnToSchedulerClick(object sender, RoutedEventArgs e)
		{
			MainTabControl.SelectedItem = SchedulerTab;
		}
	}
}
