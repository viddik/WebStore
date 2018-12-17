﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.Dto.Order;
using WebStore.Interfaces.Services;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersService _ordersService;

        public CartController(ICartService cartService, IOrdersService ordersService)
        {
            _cartService = cartService;
            _ordersService = ordersService;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        /// <summary>
        /// Уменьшение количества товара id
        /// на единицу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Удаление товара id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Очистка корзины
        /// </summary>
        /// <returns></returns>
        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        /// <summary>
        /// Увеличение количества товара id
        /// на единицу
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult AddToCart(int id, string returnUrl)
        {
            _cartService.AddToCart(id);
            return Redirect(returnUrl);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            var detailsModel = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };

            if (ModelState.IsValid)
            {
                CreateOrderModel createModel = new CreateOrderModel();
                createModel.OrderViewModel = model;

                //var orderItems = _cartService.GetOrderItems();
                //if (orderItems == null || orderItems.Count == 0)
                //{
                //    ModelState.AddModelError("error", "InvalidModel");
                //    return View("Details", detailsModel);
                //}
                //createModel.OrderItems = orderItems.ToList();

                createModel.OrderItems = _cartService.GetOrderItems()?.ToList();

                var orderResult = _ordersService.CreateOrder(createModel, User.Identity.Name);
                _cartService.RemoveAll();

                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }
                        
            return View("Details", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}