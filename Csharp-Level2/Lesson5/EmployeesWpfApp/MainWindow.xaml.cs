/*
Болотов Михаил
Урок 5. Знакомство с технологией WPF
	
Создать WPF-приложение для ведения списка сотрудников компании.
	1. Создать сущности Employee и Department и заполнить списки сущностей начальными данными.
	2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение).
		Это можно сделать, например, с использованием ComboBox или ListView.
	3. Предусмотреть редактирование сотрудников и департаментов. Должна быть возможность 
		изменить департамент у сотрудника. Список департаментов для выбора можно выводить в
		ComboBox, и все это можно выводить на дополнительной форме.
	4. Предусмотреть возможность создания новых сотрудников и департаментов. Реализовать это
	либо на форме редактирования, либо сделать новую форму.
*/
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Главное окно приложения "Департаменты и сотрудники"
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Департаменты
		/// </summary>
		private List<Department> Departments { get; set; }
		/// <summary>
		/// Сотрудники
		/// </summary>
		private List<Employee> Employees { get; set; }

		public MainWindow()
		{
			InitializeComponent();

			Departments = new List<Department>
			{
				new Department { Name = "Управление и финансы" },
				new Department { Name = "Департамент развития" }
			};
			Employees = new List<Employee>
			{
				new Employee(Departments[0]) { FirstName = "Иван", LastName = "Петров" },
				new Employee(Departments[0]) { FirstName = "Сергей", LastName = "Филипчук" },
				new Employee(Departments[1]) { FirstName = "Светлана", LastName = "Сенчукова" },
				new Employee(Departments[1]) { FirstName = "Дмитрий", LastName = "Захаров" }
			};
			_depsListBox.ItemsSource = Departments;
			_depsListBox.SelectedItem = Departments[0];

			_depComboBox.ItemsSource = Departments;
		}

		private void DepAdd_Click(object sender, RoutedEventArgs e)
		{
			Departments.Add(new Department { Name = "Новый департамент" });
			_depsListBox.SelectedIndex = _depsListBox.Items.Count - 1;
			_depsListBox.Items.Refresh();
			_depComboBox.Items.Refresh();
		}

		private void DepDel_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is Department dep)
			{
				var itemsSource = (List<Employee>)_emplsList.ItemsSource;
				if (itemsSource.Count > 0)
				{
					MessageBox.Show(this, "Для удаления департамента сначала удалите его сотрудников.");
					return;
				}
				if (MessageBox.Show(this, $"Удалить '{dep}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					Departments.Remove(dep);
					_depsListBox.Items.Refresh();
					_depComboBox.Items.Refresh();
				}
			}
		}

		private void DepSave_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is Department dep)
			{
				dep.Name = _depName.Text;
				_depsListBox.Items.Refresh();
				_depComboBox.Items.Refresh();
			}
		}

		private void DepsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dep = _depsListBox.SelectedItem as Department;
			_depName.Text = dep?.Name;

			var empls = dep != null ? Employees.Where(emp => emp.Department == dep).ToList() : new List<Employee>();
			_emplsList.ItemsSource = empls;
			if (empls.Count > 0)
				_emplsList.SelectedIndex = 0;
		}

		private void EmplList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var empl = _emplsList.SelectedItem as Employee;
			_emplFirstName.Text = empl?.FirstName;
			_emplLastName.Text = empl?.LastName;
			_depComboBox.SelectedItem = empl?.Department;
		}

		private void EmplAdd_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is Department dep)
			{
				var empl = new Employee(dep) { FirstName = "Имя", LastName = "Фамилия" };
				Employees.Add(empl);

				var itemsSource = (List<Employee>)_emplsList.ItemsSource;
				itemsSource.Add(empl);
				_emplsList.SelectedIndex = itemsSource.Count - 1;
				_emplsList.Items.Refresh();
			}
			else
				MessageBox.Show(this, "Не выбран департамент.");
		}

		private void EmplDel_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsList.SelectedItem is Employee empl &&
				MessageBox.Show(this, $"Удалить '{empl}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				var itemsSource = (List<Employee>)_emplsList.ItemsSource;
				itemsSource.Remove(empl);
				_emplsList.Items.Refresh();

				Employees.Remove(empl);
			}
		}

		private void EmplSave_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsList.SelectedItem is Employee empl)
			{
				empl.FirstName = _emplFirstName.Text;
				empl.LastName = _emplLastName.Text;
				empl.Department = (Department)_depComboBox.SelectedItem;
				if (!empl.Department.Equals(_depsListBox.SelectedItem))
				{
					var itemsSource = (List<Employee>)_emplsList.ItemsSource;
					itemsSource.Remove(empl);
					MessageBox.Show(this, $"Сотрудник '{empl}' перенесен в департамент '{empl.Department}'.");
				}
				_emplsList.Items.Refresh();
			}

		}
	}
}
