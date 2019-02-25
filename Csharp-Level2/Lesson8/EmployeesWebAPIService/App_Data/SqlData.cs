using EmployeesCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeesWebAPIService
{
	public class SqlData
	{
		private const string ConnectionName = "my_employees_db";
		private readonly string _connString;

		public SqlData()
		{
			_connString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
				//@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=my_employees_db;Integrated Security=True;Pooling=False";
			var connection = new SqlConnection(_connString);
		}

		public List<Department> GetDepartments(Guid id = default(Guid))
		{
			var deps = new List<Department>();

			var sql = "SELECT Id, Name FROM Departments";
			if (id != default(Guid))
				sql += " where id = @id";

			using (var connection = new SqlConnection(_connString))
			{
				connection.Open();
				using (var cmd = new SqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@id", id);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
							deps.Add(new Department { Id = (Guid)reader["Id"], Name = (string)reader["Name"] });
					}
				}
			}
			return deps;
		}

		public List<Employee> GetEmployees(Guid id = default(Guid))
		{
			var empls = new List<Employee>();

			var sql = "SELECT Id, FirstName, LastName, Department_Id FROM Employees";
			if (id != default(Guid))
				sql += " where id = @id";

			using (var connection = new SqlConnection(_connString))
			{
				connection.Open();
				using (var cmd = new SqlCommand(sql, connection))
				{
					cmd.Parameters.AddWithValue("@id", id);
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
							empls.Add(new Employee { Id = (Guid)reader["Id"], FirstName = (string)reader["FirstName"],
								LastName = (string)reader["LastName"], Department_Id = (Guid)reader["Department_Id"] });
					}
				}
			}
			return empls;
		}
	}
}
