/*
Болотов Михаил
Урок 8. Обзор сервис-ориентированной архитектуры приложений (SOA).
	
Изменить WPF-приложение для ведения списка сотрудников компании (из урока 7), используя веб-сервисы.
Разделите приложение на две части. Первая часть – клиентское приложение, отображающее данные.
Вторая часть – веб-сервис и код, связанный с извлечением данных из БД. Приложение реализует только просмотр данных.
При разработке приложения допустимо использовать любой из рассмотренных типов веб-сервисов.
	1. Создать таблицы Employee и Department в БД MSSQL Server и заполнить списки сущностей начальными данными;
	2. Для списка сотрудников и списка департаментов предусмотреть визуализацию (отображение);
	3. Разработать формы для отображения отдельных элементов списков сотрудников и департаментов.
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
		private ModelWebService _model;

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			_model = new ModelWebService("http://localhost:50344/");
			DataContext = _model;
			if (_model.InvalidWebService)
			{
				MessageBox.Show("Не удалось подключиться к веб-сервису.\nПриложение будет работать с ошибками.");
			}
		}

		private void DepOpen_Click(object sender, RoutedEventArgs e)
		{
			if (_depsListBox.SelectedItem is EmployeesCommon.Department dep)
			{
				new DepartmentWindow(dep).ShowDialog();
			}
		}

		private void DepsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (_depsListBox.SelectedItem is EmployeesCommon.Department dep)
			{
				_emplsGrid.ItemsSource = _model.GetEmployeesByDep(dep.Id);
				_emplsGrid.SelectedIndex = 0;
			}
		}

		private void EmplOpen_Click(object sender, RoutedEventArgs e)
		{
			if (_emplsGrid.SelectedItem is EmployeesCommon.Employee empl &&
				_depsListBox.SelectedItem is EmployeesCommon.Department dep)
			{
				new EmployeeWindow(empl, dep.Name).ShowDialog();
			}
		}
	}
}
