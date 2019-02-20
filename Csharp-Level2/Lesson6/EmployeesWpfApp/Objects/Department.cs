using System;
using System.ComponentModel;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Класс Департамент
	/// </summary>
	public class Department : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public Guid Id { get; private set; }

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
			}
		}

		public Department(string name = null)
		{
			Id = Guid.NewGuid();
			Name = name ?? "Новый департамент";
		}
	}
}
