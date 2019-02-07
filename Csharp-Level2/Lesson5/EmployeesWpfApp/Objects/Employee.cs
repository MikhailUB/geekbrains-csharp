using System;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Класс Сотрудник
	/// </summary>
	public class Employee
	{
		public Guid Id { get; private set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName => $"{FirstName} {LastName}";
		public Department Department { get; set; }

		public Employee(Department department)
		{
			Id = Guid.NewGuid();
			Department = department ?? throw new ArgumentNullException(nameof(department));
		}

		public override string ToString()
		{
			return FullName;
		}
	}
}
