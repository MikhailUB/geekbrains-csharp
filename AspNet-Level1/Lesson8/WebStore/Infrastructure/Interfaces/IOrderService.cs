using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
	public interface IOrderService
	{
		IEnumerable<Order> GetUserOrders(string userName);

		Order GetOrderById(int id);

		Order CreateOrder(OrderViewModel orderModel, CartViewModel cartModel, string userName);
	}
}
