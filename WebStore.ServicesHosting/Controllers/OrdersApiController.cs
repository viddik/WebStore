using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/orders")]
    public class OrdersApiController : Controller, IOrdersService
    {
        private readonly IOrdersService _ordersService;

        public OrdersApiController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        
        // Get api/orders/user/{username}
        [HttpGet("user/{userName}")]
        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _ordersService.GetUserOrders(userName);
        }

        // Get api/orders/id
        [HttpGet("{id}")]
        public OrderDto GetOrderById(int id)
        {
            return _ordersService.GetOrderById(id);

        }

        // Post api/orders/{userName?}
        [HttpPost("{userName?}")]
        public OrderDto CreateOrder([FromBody]CreateOrderModel orderModel, string userName)
        {
            return _ordersService.CreateOrder(orderModel, userName);
        }
    }
}