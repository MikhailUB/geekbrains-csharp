using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
	/// <summary>
	/// Сервис товаров
	/// </summary>
	public interface IProductData
	{
		IEnumerable<Section> GetSections();

		IEnumerable<Brand> GetBrands();

		IEnumerable<Product> GetProducts(ProductFilter filter);

		Product GetProductById(int id);
	}
}
