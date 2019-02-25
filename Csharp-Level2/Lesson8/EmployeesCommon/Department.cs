using System;
using System.Runtime.Serialization;

namespace EmployeesCommon
{
	/// <summary>
	/// Класс Департамент
	/// </summary>
	[DataContract]
	public class Department
	{
		[DataMember]
		public Guid Id { get; set; }
		[DataMember]
		public string Name { get; set; }

		public Department(string name = null)
		{
			Id = Guid.NewGuid();
			Name = name ?? "Новый департамент";
		}
	}
}
