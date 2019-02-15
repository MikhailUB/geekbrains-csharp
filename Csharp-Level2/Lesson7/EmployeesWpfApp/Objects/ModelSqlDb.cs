using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeesWpfApp
{
	public class ModelSqlDb
	{
		private DataTable _depsTable;
		private DataTable _emplsTable;

		public DataView Departments { get => _depsTable?.DefaultView; }
		/// <summary>
		/// Сотрудники
		/// </summary>
		public DataView Employees { get => _emplsTable?.DefaultView; }
		/// <summary>
		/// Флаг проблемы подключения к БД
		/// </summary>
		public bool InvalidDatabase { get => !(_depsTable?.Columns.Count > 0); }

		private SqlDataAdapter _adapterDeps;
		private SqlDataAdapter _adapderEmpls;

		public ModelSqlDb(string connectionName)
		{
			_depsTable = new DataTable(nameof(Departments));
			_emplsTable = new DataTable(nameof(Employees));

			var connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
				//@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=my_employees_db;Integrated Security=True;Pooling=False";
			var connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				connection.Close();
			}
			catch
			{
				return;
			}
			_adapterDeps = CreateDepsAdapter(connection);
			_adapterDeps.Fill(_depsTable);

			_adapderEmpls = CreateEmplsAdapter(connection);
			_adapderEmpls.Fill(_emplsTable);
		}

		private static SqlDataAdapter CreateDepsAdapter(SqlConnection connection)
		{
			var adapter = new SqlDataAdapter();
			// Select
			var cmd = new SqlCommand("SELECT Id, Name FROM Departments;", connection);
			adapter.SelectCommand = cmd;
			// Insert
			cmd = new SqlCommand("INSERT INTO Departments (Id, Name) VALUES (@id, @name);", connection);
			cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			cmd.Parameters.Add("name", SqlDbType.NVarChar, 50, "Name");
			adapter.InsertCommand = cmd;

			// Update
			cmd = new SqlCommand("UPDATE Departments SET Name = @name where Id = @id;", connection);
			var param = cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			param.SourceVersion = DataRowVersion.Original;
			cmd.Parameters.Add("name", SqlDbType.NVarChar, 50, "Name");
			adapter.UpdateCommand = cmd;

			// Delete
			cmd = new SqlCommand("DELETE FROM Departments where Id = @id;", connection);
			param = cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			param.SourceVersion = DataRowVersion.Original;
			adapter.DeleteCommand = cmd;
			return adapter;
		}

		private static SqlDataAdapter CreateEmplsAdapter(SqlConnection connection)
		{
			var adapter = new SqlDataAdapter();
			// Select
			var cmd = new SqlCommand("SELECT Id, FirstName, LastName, Department_Id FROM Employees;", connection);
			adapter.SelectCommand = cmd;
			// Insert
			cmd = new SqlCommand("INSERT INTO Employees (Id, FirstName, LastName, Department_Id) VALUES (@id, @firstName, @lastName, @depId);", connection);
			cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			cmd.Parameters.Add("firstName", SqlDbType.NVarChar, 50, "FirstName");
			cmd.Parameters.Add("lastName", SqlDbType.NVarChar, 50, "LastName");
			cmd.Parameters.Add("depId", SqlDbType.UniqueIdentifier, -1, "Department_Id");
			adapter.InsertCommand = cmd;

			// Update
			cmd = new SqlCommand("UPDATE Employees SET FirstName = @firstName, LastName = @lastName, Department_Id = @depId where Id = @id;", connection);
			var param = cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			param.SourceVersion = DataRowVersion.Original;
			cmd.Parameters.Add("firstName", SqlDbType.NVarChar, 50, "FirstName");
			cmd.Parameters.Add("lastName", SqlDbType.NVarChar, 50, "LastName");
			cmd.Parameters.Add("depId", SqlDbType.UniqueIdentifier, -1, "Department_Id");
			adapter.UpdateCommand = cmd;

			// Delete
			cmd = new SqlCommand("DELETE FROM Employees where Id = @id;", connection);
			param = cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier, -1, "Id");
			param.SourceVersion = DataRowVersion.Original;
			adapter.DeleteCommand = cmd;
			return adapter;
		}

		public void CreateDepartment(string name = null)
		{
			_depsTable.Rows.Add(Guid.NewGuid(), name ?? ("Новый департамент" + Departments.Count));
			_adapterDeps.Update(_depsTable);
		}

		public void UpdateDepartment(DataRowView depRow, string name)
		{
			depRow["Name"] = name;
			_adapterDeps.Update(_depsTable);
		}

		public void DeleteDepartment(DataRowView depRow)
		{
			depRow.Row.Delete();
			_adapterDeps.Update(_depsTable);
		}

		public void CreateEmployee(DataRowView depRow, string firstName = null, string lastName = null)
		{
			_emplsTable.Rows.Add(Guid.NewGuid(), firstName ?? ("Имя" + Employees.Count), lastName ?? ("Фамилия" + Employees.Count), depRow["Id"]);
			_adapderEmpls.Update(_emplsTable);
		}

		public void UpdateEmployee(DataRowView emplRow, string firstName, string lastName, DataRowView depRow)
		{
			emplRow["FirstName"] = firstName;
			emplRow["LastName"] = lastName;
			emplRow["Department_Id"] = depRow["Id"];
			_adapderEmpls.Update(_emplsTable);
		}

		public void DeleteEmployee(DataRowView emplRow)
		{
			emplRow.Row.Delete();
			_adapderEmpls.Update(_emplsTable);
		}

		public void FilterEmployeesByDep(DataRowView depRow)
		{
			Employees.RowFilter = $"[Department_Id] = '{depRow["Id"]}'";
		}
	}
}
