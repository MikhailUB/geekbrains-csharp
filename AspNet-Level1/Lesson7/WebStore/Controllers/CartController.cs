﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartService _cartService;
		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		public IActionResult Details()
		{
			return View(_cartService.TransformCart());
		}

		public IActionResult DecrementFromCart(int id)
		{
			_cartService.DecrementFromCart(id);
			return RedirectToAction("Details");
		}

		public IActionResult RemoveFromCart(int id)
		{
			_cartService.RemoveFromCart(id);
			return RedirectToAction("Details");
		}

		public IActionResult RemoveAll()
		{
			_cartService.RemoveAll();
			return RedirectToAction("Details");
		}

		public IActionResult AddToCart(int id)
		{
			_cartService.AddToCart(id);
			return RedirectToAction("Details");
		}
	}
}
