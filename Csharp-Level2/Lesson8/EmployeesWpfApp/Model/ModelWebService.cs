using EmployeesCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EmployeesWpfApp
{
	public class ModelWebService
	{
		private const string DepsPath = "api/departments";
		private const string EmplsPath = "api/employees";
		private HttpClient _client;

		/// <summary>
		/// Департаменты
		/// </summary>
		public List<Department> Departments { get; set; }
		/// <summary>
		/// Сотрудники
		/// </summary>
		public List<Employee> Employees { get; set; }
		/// <summary>
		/// Флаг проблемы подключения к веб-сервису
		/// </summary>
		public bool InvalidWebService { get; private set; }

		public ModelWebService(string uriString)
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri(uriString)
			};
			_client.DefaultRequestHeaders.Accept.Clear();
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			try
			{
				Departments = GetDepartments();
				Employees = GetEmployees();
			}
			catch
			{
				InvalidWebService = true;
			}
		}

		private List<Department> GetDepartments()
		{
			var json = _client.GetStringAsync(_client.BaseAddress + DepsPath).Result;
			return JsonConvert.DeserializeObject<List<Department>>(json);
		}

		private List<Employee> GetEmployees()
		{
			var json = _client.GetStringAsync(_client.BaseAddress + EmplsPath).Result;
			return JsonConvert.DeserializeObject<List<Employee>>(json);
		}

		public List<Employee> GetEmployeesByDep(Guid depId)
		{
			return Employees.Where(e => e.Department_Id == depId).ToList();
		}
	}
}
