using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Implementations
{
	public class SqlProductData : IProductData
	{
		private readonly WebStoreContext _db;

		public SqlProductData(WebStoreContext db)
		{
			_db = db;
		}

		public IEnumerable<Section> GetSections() => _db.Sections
			.Include(s => s.Products)
			.AsEnumerable();

		public IEnumerable<Brand> GetBrands() => _db.Brands
			.Include(brand => brand.Products)
			.AsEnumerable();

		public IEnumerable<Product> GetProducts(ProductFilter filter)
		{
			IQueryable<Product> products = _db.Products;
			if (filter is null)
				return products.AsEnumerable();

			if (filter.SectionId != null)
				products = products.Where(product => product.SectionId == filter.SectionId);

			if (filter.BrandId != null)
				products = products.Where(product => product.BrandId == filter.BrandId);

			return products.AsEnumerable();
		}

		public Product GetProductById(int id)
		{
			return _db.Products
				.Include(prod => prod.Brand)
				.Include(prod => prod.Section)
				.FirstOrDefault(prod => prod.Id == id);
		}
	}
}
