using EmployeesCommon;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EmployeesWebAPIService
{
	public class EmployeesController : ApiController
	{
		// GET api/<controller>
		public IEnumerable<Employee> GetEmployees()
		{
			return new SqlData().GetEmployees();
		}

		// GET api/<controller>/sdfs3242
		public IHttpActionResult GetEmployee(Guid id)
		{
			var empl = new SqlData().GetEmployees(id);
			if (empl != null)
				return Ok(empl);

			return NotFound();
		}
	}
}
