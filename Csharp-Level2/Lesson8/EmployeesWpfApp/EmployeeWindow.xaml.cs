using System.Windows;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Interaction logic for EmployeeWindow.xaml
	/// </summary>
	public partial class EmployeeWindow : Window
	{
		private EmployeesCommon.Employee _employee;

		public EmployeeWindow(EmployeesCommon.Employee empl, string depName)
		{
			InitializeComponent();
			_employee = empl;
			DataContext = _employee;

			_depTextBox.Text = depName;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
