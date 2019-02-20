using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Класс данных и логики приложения
	/// </summary>
	public class Model
	{
		/// <summary>
		/// Департаменты
		/// </summary>
		public ObservableCollection<Department> Departments { get; private set; }
		/// <summary>
		/// Сотрудники
		/// </summary>
		public ObservableCollection<Employee> Employees { get; private set; }

		public Model()
		{
			Departments = new ObservableCollection<Department>();
			CreateDepartment("Управление и финансы");
			CreateDepartment("Департамент развития");

			Employees = new ObservableCollection<Employee>();
			CreateEmployee(Departments[0], "Иван", "Петров");
			CreateEmployee(Departments[0], "Сергей", "Филипчук");
			CreateEmployee(Departments[1], "Светлана", "Сенчукова");
			CreateEmployee(Departments[1], "Дмитрий", "Захаров");
		}

		public Department CreateDepartment(string name = null)
		{
			var dep = new Department(name);
			Departments.Add(dep);
			return dep;
		}

		public void DeleteDepartment(Department dep)
		{
			for (int i = Departments.Count - 1; i > -1; i--)
			{
				if (Departments[i].Id == dep.Id)
					Departments.RemoveAt(i);
			}
		}

		public Employee CreateEmployee(Department dep, string firstName = null, string lastName = null)
		{
			var empl = new Employee(dep, firstName, lastName);
			Employees.Add(empl);
			return empl;
		}

		public void DeleteEmployee(Employee empl)
		{
			for (int i = Employees.Count - 1; i > -1; i--)
			{
				if (Employees[i].Id == empl.Id)
					Employees.RemoveAt(i);
			}
		}

		public List<Employee> GetEmployeesByDep(Department dep)
		{
			return (dep != null)
				? Employees.Where(e => e.Department.Id == dep.Id).ToList()
				: new List<Employee>();
		}
	}
}
