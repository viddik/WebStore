using System.Collections.Generic;
using WebStore.Domain.Dto.Order;

namespace WebStore.Interfaces.Services
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
