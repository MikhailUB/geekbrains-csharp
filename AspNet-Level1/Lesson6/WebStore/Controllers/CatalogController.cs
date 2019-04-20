﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Map;
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
                Products = products.Select(p => p.CreateViewModel())
            };

            return View(model);
        }
    }
}