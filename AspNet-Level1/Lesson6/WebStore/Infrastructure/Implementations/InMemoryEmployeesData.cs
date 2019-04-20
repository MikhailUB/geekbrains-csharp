using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Implementations
{
	public class InMemoryEmployeesData : IEmployeesData
	{
		private readonly List<Employee> _employees = new List<Employee>
		{
			new Employee{ Id = 1, SurName = "Иванов", Name = "Андрей", Patronymic = "Иванович", Age = 25, StartWork = new DateTime(2016, 5, 15) },
			new Employee{ Id = 2, SurName = "Петров", Name = "Михаил", Patronymic = "Петрович", Age = 30, StartWork = new DateTime(2017, 3, 24) },
			new Employee{ Id = 3, SurName = "Сидоров", Name = "Олег", Patronymic = "Никифорович", Age = 35, StartWork = new DateTime(2018, 7, 8) },
		};

		public IEnumerable<Employee> GetAll() => _employees;

		public Employee GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

		public void AddNew(Employee employee)
		{
			if (employee is null)
				throw new ArgumentNullException(nameof(employee));

			if (_employees.All(e => e.Id != employee.Id))
			{
				employee.Id = _employees.Count > 0 ? _employees.Max(e => e.Id) + 1 : 1;
				_employees.Add(employee);
			}
		}

		public void Delete(int id)
		{
			var employee = GetById(id);
			if (!(employee is null))
				_employees.Remove(employee);
		}

		public void SaveChanges()
		{
		}
	}
}
