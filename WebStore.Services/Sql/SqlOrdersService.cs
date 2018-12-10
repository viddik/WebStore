using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Models.Cart;
using WebStore.Models.Order;

namespace WebStore.Services.Sql
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;

        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _context.Orders.Include("User").Include("OrderItems").Where(o => o.User.UserName.Equals(userName)).Select(o => new OrderDto()
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Phone = o.Phone,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto()
                {
                    Id = oi.Id,
                    ProductId = oi.Product.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })
            }).ToList();
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _context.Orders.Include("OrderItems").FirstOrDefault(o => o.Id.Equals(id));

            if (order == null) return null;

            return new OrderDto()
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Date = order.Date,
                Phone = order.Phone,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto()
                {
                    Id = oi.Id,
                    ProductId = oi.Product.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })
            };
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.OrderViewModel.Address,
                    Name = orderModel.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in orderModel.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(item.ProductId));
                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();
                return GetOrderById(order.Id);
            }
        }
    }
}
