using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
	public class CatalogController : Controller
    {
        private readonly IProductData _productData;

        public CatalogController(IProductData ProductData)
        {
            _productData = ProductData;
        }

        public IActionResult Shop(int? sectionId, int? brandId)
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId
            });

            var model = new CatalogViewModel
            {
                BrandId = brandId,
                SectionId = sectionId,
                Products = products.Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
            };

            return View(model);
        }
    }
}