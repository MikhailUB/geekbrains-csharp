using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();

		public IActionResult ContactUs() => View();

		public IActionResult Checkout() => View();

		public IActionResult Blog() => View();

		public IActionResult BlogSingle() => View();

		public IActionResult NotFound() => View();
	}
}
