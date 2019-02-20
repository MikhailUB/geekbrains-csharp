/*
Болотов Михаил
Урок 7. Взаимодействие с базой данных
	
Изменить WPF-приложение для ведения списка сотрудников компании (из урока 5),
используя связывание данных, DataGrid и ADO.NET
	1. Создать таблицы Employee и Department в БД MSSQL Server и заполнить списки сущностей
		начальными данными.
	2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение).
		Это можно сделать, например, с использованием ComboBox или ListView.
	3. Предусмотреть редактирование сотрудников и департаментов. Должна быть возможность 
		изменить департамент у сотрудника. Список департаментов для выбора можно выводить в
		ComboBox, и все это можно выводить на дополнительной форме.
	4. Предусмотреть возможность создания новых сотрудников и департаментов. Реализовать это
		либо на форме редактирования, либо сделать новую форму.
*/
using System;
using System.Data;
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
		private ModelSqlDb _model;

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			_model = new ModelSqlDb("my_employees_db");
			DataContext = _model;
			if (_model.InvalidDatabase)
			{
				MessageBox.Show("Не удалось подключиться к БД.\nПриложение будет работать с ошибками.");
			}
		}

		private void DepAdd_Click(object sender, RoutedEventArgs e)
		{
			// Создаем департамент в БД
			_model.CreateDepartment();
			_depsListBox.SelectedIndex = _depsListBox.Items.Count - 1;
		}

		private void DepDel_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is DataRowView dep)
			{
				if (_emplsGrid.Items.Count > 0)
				{
					MessageBox.Show(this, "Для удаления департамента сначала удалите его сотрудников.");
				}
				else if (MessageBox.Show(this, $"Удалить '{dep["Name"]}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					var idx = _depsListBox.SelectedIndex;
					// Удаляем департамент в БД
					_model.DeleteDepartment(dep);
					_depsListBox.SelectedIndex = idx < _depsListBox.Items.Count ? idx : idx - 1;
				}
			}
		}

		private void DepSave_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is DataRowView dep)
			{
				// Обновляем департамент в БД
				_model.UpdateDepartment(dep, _depName.Text);
			}
		}

		private void DepsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RefreshEmployees();
			_emplsGrid.SelectedIndex = 0;
		}

		private void RefreshEmployees()
		{
			var dep = (DataRowView)_depsListBox.SelectedItem;
			if (dep != null)
				_model.FilterEmployeesByDep(dep); // Фильтруем сотрудников по текущему департаменту
		}

		private void EmplAdd_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is DataRowView dep)
			{
				// Создаем сотрудника в БД
				_model.CreateEmployee(dep);
				RefreshEmployees();
				_emplsGrid.SelectedIndex = _emplsGrid.Items.Count - 1;
			}
			else
				MessageBox.Show(this, "Не выбран департамент.");
		}

		private void EmplDel_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsGrid.SelectedItem is DataRowView empl &&
				MessageBox.Show(this, $"Удалить '{(empl["FirstName"] + " " + empl["LastName"])}'?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				var idx = _emplsGrid.SelectedIndex;
				// Удаляем сотрудника в БД
				_model.DeleteEmployee(empl);
				RefreshEmployees();
				_emplsGrid.SelectedIndex = idx < _emplsGrid.Items.Count ? idx : idx - 1;
			}
		}

		private void EmplSave_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsGrid.SelectedItem is DataRowView empl)
			{
				var depRow = _emplDepComboBox.SelectedItem as DataRowView;
				if (depRow == null)
				{
					MessageBox.Show(this, $"Не задан в департамент.");
					return;
				}
				// Обновляем сотрудника в БД
				_model.UpdateEmployee(empl, _emplFirstName.Text, _emplLastName.Text, depRow);

				if ((Guid)depRow["Id"] != (Guid)(_depsListBox.SelectedItem as DataRowView)["Id"])
				{
					MessageBox.Show(this, $"Сотрудник '{(empl["FirstName"] + " " + empl["LastName"])}' перенесен в департамент '{depRow["Name"]}'.");
				}
			}
		}
	}
}
