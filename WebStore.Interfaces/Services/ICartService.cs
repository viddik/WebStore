using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.ViewModel.Cart;

namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void RemoveAll();

        void AddToCart(int id);

        CartViewModel TransformCart();

        IList<OrderItemDto> GetOrderItems();
    }
}
