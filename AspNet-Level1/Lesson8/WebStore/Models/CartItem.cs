using System.Collections.Generic;
using System.Linq;

namespace WebStore.Models
{
	public class CartItem
	{
		public int ProductId { get; set; }

		public int Quantity { get; set; }
	}

	public class Cart
	{
		public List<CartItem> Items { get; set; } = new List<CartItem>();

		public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
	}
}
