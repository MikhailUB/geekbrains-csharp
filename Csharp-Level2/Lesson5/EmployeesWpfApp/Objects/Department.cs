using System;

namespace EmployeesWpfApp
{
	/// <summary>
	/// Класс Департамент
	/// </summary>
	public class Department
	{
		public Guid Id { get; private set; }
		public string Name { get; set; }

		public Department()
		{
			Id = Guid.NewGuid();
		}

		public override string ToString()
		{
			return Name;
		}

		/// <summary>
		/// Сравнение департаментов выполняется по Id
		/// </summary>
		public override bool Equals(object obj)
		{
			return obj is Department ? (this == (Department)obj) : base.Equals(obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static bool operator ==(Department dep1, Department dep2)
		{
			return dep1?.Id == dep2?.Id;
		}

		public static bool operator !=(Department dep1, Department dep2)
		{
			return !(dep1 == dep2);
		}

	}
}
