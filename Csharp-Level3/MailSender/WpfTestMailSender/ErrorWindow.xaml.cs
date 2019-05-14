using System.Windows;

namespace WpfTestMailSender
{
	/// <summary>
	/// Interaction logic for ErrorWindow.xaml
	/// </summary>
	public partial class ErrorWindow : Window
	{
		public ErrorWindow(string error)
		{
			InitializeComponent();

			_errorBlock.Text = error;
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
