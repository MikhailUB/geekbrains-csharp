/*
Болотов Михаил
Урок 6. Связывание данных. Триггеры.
	
Изменить WPF-приложение для ведения списка сотрудников компании (из урока 5),
используя связывание данных, ListView, ObservableCollection и INotifyPropertyChanged.
	1. Создать сущности Employee и Department и заполнить списки сущностей начальными данными.
	2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение).
		Это можно сделать, например, с использованием ComboBox или ListView.
	3. Предусмотреть редактирование сотрудников и департаментов. Должна быть возможность 
		изменить департамент у сотрудника. Список департаментов для выбора можно выводить в
		ComboBox, и все это можно выводить на дополнительной форме.
	4. Предусмотреть возможность создания новых сотрудников и департаментов. Реализовать это
		либо на форме редактирования, либо сделать новую форму.
*/
using System.Windows;
using System.Windows.Controls;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Главное окно приложения "Департаменты и сотрудники"
	/// </summary>
	public partial class MainWindow
	{
		// Объект Модель инкапсулирует данные и логику приложения
		private Model _model;

		private void DepAdd_Click(object sender, RoutedEventArgs e)
		{
			_model.CreateDepartment();
			_depsListBox.SelectedIndex = _depsListBox.Items.Count - 1;
		}

		private void DepDel_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is Department dep)
			{
				if (_emplsList.Items.Count > 0)
				{
					MessageBox.Show(this, "Для удаления департамента сначала удалите его сотрудников.");
				}
				else if (MessageBox.Show(this, $"Удалить '{dep.Name}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					_model.DeleteDepartment(dep);
				}
			}
		}

		private void DepsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshEmployees();
			_emplsList.SelectedIndex = 0;
		}

		private void RefreshEmployees()
		{
			_emplsList.ItemsSource = _model.GetEmployeesByDep(_depsListBox.SelectedItem as Department);
		}

		private void EmplAdd_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is Department dep)
			{
				var empl = _model.CreateEmployee(dep);
				RefreshEmployees();
				_emplsList.SelectedIndex = _emplsList.Items.Count - 1;
			}
			else
				MessageBox.Show(this, "Не выбран департамент.");
		}

		private void EmplDel_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsList.SelectedItem is Employee empl &&
				MessageBox.Show(this, $"Удалить '{empl.FullName}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				var idx = _emplsList.SelectedIndex;
				_model.DeleteEmployee(empl);
				RefreshEmployees();
				_emplsList.SelectedIndex = idx < _emplsList.Items.Count ? idx : idx - 1;
			}
		}

		private void EmplDepComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_emplsList.SelectedItem is Employee empl &&
				empl.Department.Id != ((Department)_depsListBox.SelectedItem).Id)
			{
				var idx = _emplsList.SelectedIndex;
				RefreshEmployees();
				_emplsList.SelectedIndex = idx < _emplsList.Items.Count ? idx : idx - 1;
				MessageBox.Show(this, $"Сотрудник '{empl.FullName}' перенесен в департамент '{empl.Department.Name}'.");
			}
		}
	}
}
