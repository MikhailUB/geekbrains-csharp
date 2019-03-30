using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
	public class EmployesController : Controller
	{
		private static List<Employee> _employees = new List<Employee>
		{
			new Employee{ Id = 1, SurName = "Иванов", Name = "Андрей", Patronymic = "Иванович", Age = 25, StartWork = new DateTime(2016, 5, 15) },
			new Employee{ Id = 2, SurName = "Петров", Name = "Михаил", Patronymic = "Петрович", Age = 30, StartWork = new DateTime(2017, 3, 24) },
			new Employee{ Id = 3, SurName = "Сидоров", Name = "Олег", Patronymic = "Никифорович", Age = 35, StartWork = new DateTime(2018, 7, 8) },
		};

		public IActionResult Index()
		{
			return View(_employees);
		}

		public IActionResult Details(int id)
		{
			var empl = _employees.FirstOrDefault(e => e.Id == id);
			if (empl != null)
				return View(empl);
			
			return NotFound();
		}
	}
}
