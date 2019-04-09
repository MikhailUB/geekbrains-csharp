using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
	public class EmployesController : Controller
	{
		private readonly IEmployeesData _employeesData;

		public EmployesController(IEmployeesData emplData)
		{
			_employeesData = emplData;
		}

		public IActionResult Index()
		{
			return View(_employeesData.GetAll());
		}

		public IActionResult Details(int id)
		{
			var empl = _employeesData.GetById(id);
			if (empl != null)
				return View(empl);
			
			return NotFound();
		}

		public IActionResult Edit(int? id)
		{
			Employee employee;
			if (id.HasValue)
			{
				employee = _employeesData.GetById(id.Value);
				if (employee is null)
					return NotFound();
			}
			else
			{
				employee = new Employee();
			}
			return View(employee);
		}

		[HttpPost]
		public IActionResult Edit(Employee employee)
		{
			if (!ModelState.IsValid)
				return View(employee);

			if (employee.Id > 0)
			{
				var dbEmployee = _employeesData.GetById(employee.Id);
				if (dbEmployee is null)
				{
					return NotFound();
				}
				dbEmployee.Name = employee.Name;
				dbEmployee.SurName = employee.SurName;
				dbEmployee.Patronymic = employee.Patronymic;
				dbEmployee.Age = employee.Age;
				dbEmployee.StartWork = employee.StartWork;
			}
			else
			{
				_employeesData.AddNew(employee);
			}
			_employeesData.SaveChanges();

			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var empl = _employeesData.GetById(id);
			if (empl is null)
				return NotFound();

			_employeesData.Delete(id);
			return RedirectToAction("Index");
		}
	}
}
