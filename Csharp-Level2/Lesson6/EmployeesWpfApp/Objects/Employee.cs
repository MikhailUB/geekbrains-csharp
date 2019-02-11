using System;
using System.ComponentModel;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Класс Сотрудник
	/// </summary>
	public class Employee : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Guid Id { get; private set; }

		private string _firstName;
		public string FirstName
		{
			get => _firstName;
			set
			{
				_firstName = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
			}
		}
		private string _lastName;
		public string LastName
		{
			get => _lastName;
			set
			{
				_lastName = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LastName)));
			}
		}

		public string FullName => $"{FirstName} {LastName}";

		private Department _department;
		public Department Department
		{
			get => _department;
			set
			{
				_department = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Department)));
			}
		}


		public Employee(Department department, string firstName = null, string lastName = null)
		{
			Id = Guid.NewGuid();
			Department = department ?? throw new ArgumentNullException(nameof(department));
			FirstName = firstName ?? "Имя";
			LastName = lastName ?? "Фамилия";
		}
	}
}
