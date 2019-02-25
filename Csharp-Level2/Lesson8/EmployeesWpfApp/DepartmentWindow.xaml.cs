using EmployeesCommon;
using System.Windows;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Interaction logic for DepartmentWindow.xaml
	/// </summary>
	public partial class DepartmentWindow : Window
	{
		private Department _department;

		public DepartmentWindow(Department dep)
		{
			InitializeComponent();
			_department = dep;
			DataContext = _department;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
