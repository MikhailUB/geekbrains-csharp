using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Implementations
{
	public class InMemoryProductData : IProductData
	{
		public IEnumerable<Section> GetSections() => TestData.Sections;

		public IEnumerable<Brand> GetBrands() => TestData.Brands;

		public IEnumerable<Product> GetProducts(ProductFilter filter)
		{
			IEnumerable<Product> products = TestData.Products;
			if (filter == null)
				return products;

			if (filter.BrandId != null)
				products = products.Where(product => product.BrandId == filter.BrandId);

			if (filter.SectionId != null)
				products = products.Where(product => product.SectionId == filter.SectionId);

			return products;
		}

		public Product GetProductById(int id)
		{
			return TestData.Products.FirstOrDefault(prod => prod.Id == id);
		}
	}
}
