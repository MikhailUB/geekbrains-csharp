using EmployeesCommon;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EmployeesWebAPIService
{
	public class DepartmentsController : ApiController
	{
		// GET api/<controller>
		public IEnumerable<Department> GetDepartments()
		{
			return new SqlData().GetDepartments();
		}

		// GET api/<controller>/sdfs3242
		public IHttpActionResult GetDepartment(Guid id)
		{
			var dep = new SqlData().GetDepartments(id);
			if (dep != null)
				return Ok(dep);

			return NotFound();
		}
	}
}
