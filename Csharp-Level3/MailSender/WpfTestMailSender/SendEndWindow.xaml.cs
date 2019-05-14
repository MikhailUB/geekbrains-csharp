using System.Windows;

namespace WpfTestMailSender
{
	/// <summary>
	/// Interaction logic for SendEndWindow.xaml
	/// </summary>
	public partial class SendEndWindow : Window
	{
		public SendEndWindow()
		{
			InitializeComponent();
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
