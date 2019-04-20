using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Map
{
	public static class BrandsViewModelMapper
	{
		public static void CopyTo(this BrandViewModel model, Brand brand)
		{
			brand.Name = model.Name;
			brand.Order = model.Order;
		}

		public static Brand Create(this BrandViewModel model)
		{
			var brand = new Brand();
			model.CopyTo(brand);
			return brand;
		}

		public static void CopyTo(this Brand brand, BrandViewModel model, int productsCount = 0)
		{
			model.Id = brand.Id;
			model.Name = brand.Name;
			model.Order = brand.Order;
			model.ProductsCount = productsCount;
		}

		public static BrandViewModel CreateModel(this Brand brand, int productsCount = 0)
		{
			var model = new BrandViewModel();
			brand.CopyTo(model, productsCount);
			return model;
		}
	}
}
