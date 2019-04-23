using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Map;
using WebStore.ViewModels;

namespace WebStore.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		private readonly IProductData _productData;

		public BrandsViewComponent(IProductData productData)
		{
			_productData = productData;
		}

		public IViewComponentResult Invoke()
		{
			var brands = GetBrands();

			return View(brands);
		}

		private IEnumerable<BrandViewModel> GetBrands()
		{
			var brands = _productData.GetBrands();

			return brands.Select(b => b.CreateModel());
		}
	}
}
