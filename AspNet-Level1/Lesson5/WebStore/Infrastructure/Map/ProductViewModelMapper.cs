using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Map
{
	public static class ProductViewModelMapper
    {
        public static void CopyTo(this Product product, ProductViewModel model)
        {
            model.Id = product.Id;
            model.Name = product.Name;
            model.Order = product.Order;
            model.ImageUrl = product.ImageUrl;
            model.Price = product.Price;
        }

        public static ProductViewModel CreateViewModel(this Product product)
        {
            var model = new ProductViewModel();
            product.CopyTo(model);
            return model;
        }

        public static void CopyTo(this ProductViewModel model, Product product)
        {
            product.Name = model.Name;
            product.Order = model.Order;
            product.ImageUrl = model.ImageUrl;
            product.Price = model.Price;
        }

        public static Product Create(this ProductViewModel model)
        {
            var product = new Product();
            model.CopyTo(product);
            return product;
        }
    }
}
