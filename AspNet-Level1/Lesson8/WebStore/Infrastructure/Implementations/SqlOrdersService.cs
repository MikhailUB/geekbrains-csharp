using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Implementations
{
	public class SqlOrdersService : IOrderService
	{
		private readonly WebStoreContext _db;
		private readonly UserManager<User> _userManager;

		public SqlOrdersService(WebStoreContext db, UserManager<User> userManager)
		{
			_db = db;
			_userManager = userManager;
		}

		public IEnumerable<Order> GetUserOrders(string userName) =>
			_db.Orders
				.Include(order => order.User)
				.Include(order => order.OrderItems)
				.Where(order => order.User.UserName == userName)
				.ToArray();

		public Order GetOrderById(int id)
		{
			return _db.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
		}

		public Order CreateOrder(OrderViewModel orderModel, CartViewModel cartModel, string userName)
		{
			var user = _userManager.FindByNameAsync(userName).Result;

			using (var transaction = _db.Database.BeginTransaction())
			{
				var order = new Order
				{
					Name = orderModel.Name,
					Address = orderModel.Address,
					Phone = orderModel.Phone,
					User = user,
					Date = DateTime.Now
				};
				_db.Orders.Add(order);

				foreach (var (productModel, quantity) in cartModel.Items)
				{
					var product = _db.Products.FirstOrDefault(p => p.Id == productModel.Id);
					if (product is null)
						throw new InvalidOperationException($"Товар с идентификатором {productModel.Id} в базе данных не найден.");

					var orderItem = new OrderItem
					{
						Order = order,
						Price = product.Price,
						Quantity = quantity,
						Product = product
					};
					_db.OrderItems.Add(orderItem);
				}
				_db.SaveChanges();
				transaction.Commit();

				return order;
			}
		}
	}
}
