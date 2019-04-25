using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = Domain.Entities.User.RoleAdmin)]
	public class HomeController : Controller
	{
		private readonly IProductData _productData;

		public HomeController(IProductData productData)
		{
			_productData = productData;
		}

		public IActionResult Index() => View();

		public IActionResult ProductList() => View(_productData.GetProducts(null));
	}
}
