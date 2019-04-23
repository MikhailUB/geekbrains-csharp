using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Implementations
{
	public class CookieCartService : ICartService
	{
		private readonly IProductData _productData;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _cartName;

		private Cart Cart
		{
			get
			{
				var httpContext = _httpContextAccessor.HttpContext;
				var cookie = httpContext.Request.Cookies[_cartName];

				Cart cart;
				if (cookie is null)
				{
					cart = new Cart();
					httpContext.Response.Cookies.Append(_cartName, JsonConvert.SerializeObject(cart));
				}
				else
				{
					try
					{
						cart = JsonConvert.DeserializeObject<Cart>(cookie);
					}
					catch
					{
						cart = new Cart();
					}
					httpContext.Response.Cookies.Delete(_cartName);
					httpContext.Response.Cookies.Append(_cartName, cookie, new CookieOptions
					{
						Expires = DateTime.Now.AddDays(1)
					});
				}

				return cart;
			}
			set
			{
				var httpContext = _httpContextAccessor.HttpContext;

				var json = JsonConvert.SerializeObject(value);
				httpContext.Response.Cookies.Delete(_cartName);
				httpContext.Response.Cookies.Append(_cartName, json, new CookieOptions
				{
					Expires = DateTime.Now.AddDays(1)
				});
			}
		}

		public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
		{
			_productData = productData;
			_httpContextAccessor = httpContextAccessor;

			var user = httpContextAccessor.HttpContext.User;
			var userName = user.Identity.IsAuthenticated ? user.Identity.Name : null;
			_cartName = $"cart{userName}";
		}

		public void DecrementFromCart(int id)
		{
			var cart = Cart;

			var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
			if (item != null)
			{
				if (item.Quantity > 0)
					item.Quantity--;
				if (item.Quantity == 0)
					cart.Items.Remove(item);

				Cart = cart;
			}
		}

		public void RemoveFromCart(int id)
		{
			var cart = Cart;

			var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
			if (item != null)
			{
				cart.Items.Remove(item);
				Cart = cart;
			}
		}

		public void RemoveAll()
		{
			var cart = Cart;
			cart.Items.Clear();
			Cart = cart;
		}

		public void AddToCart(int id)
		{
			var cart = Cart;
			var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

			if (item != null)
				item.Quantity++;
			else
				cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });

			Cart = cart;
		}

		public CartViewModel TransformCart()
		{
			var products = _productData.GetProducts(new ProductFilter
			{
				Ids = Cart.Items.Select(item => item.ProductId).ToList()
			});

			var productsViewModels = products.Select(p => new ProductViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Order = p.Order,
				Price = p.Price,
				ImageUrl = p.ImageUrl,
				Brand = p.Brand?.Name
			});

			return new CartViewModel
			{
				Items = Cart.Items.ToDictionary(item => productsViewModels.First(p => p.Id == item.ProductId), item => item.Quantity)
			};

		}
	}
}
