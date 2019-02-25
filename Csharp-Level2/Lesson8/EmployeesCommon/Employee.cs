using System;
using System.Runtime.Serialization;

namespace EmployeesCommon
{
	/// <summary>
	/// Класс Сотрудник
	/// </summary>
	[DataContract]
	public class Employee
	{
		[DataMember]
		public Guid Id { get; set; }
		[DataMember]
		public string FirstName { get; set; }
		[DataMember]
		public string LastName { get; set; }
		[DataMember]
		public Guid Department_Id { get; set; }

		public Employee() { }

		public Employee(Guid dep_id, string firstName = null, string lastName = null)
		{
			if (dep_id == default(Guid))
				throw new ArgumentNullException(nameof(dep_id));

			Id = Guid.NewGuid();
			Department_Id = dep_id;
			FirstName = firstName ?? "Имя";
			LastName = lastName ?? "Фамилия";
		}
	}
}
