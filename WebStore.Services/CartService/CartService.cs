using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Filters;
using WebStore.Domain.ViewModel.Cart;
using WebStore.Domain.ViewModel.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IProductData _productData;

        private readonly ICartStore _cartStore;

        public CartService(IProductData productData, ICartStore cartStore)
        {
            _productData = productData;
            _cartStore = cartStore;
        }

        /// <summary>
        /// Уменьшает количество товара id
        /// на единицу
        /// </summary>
        /// <param name="id"></param>
        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;
                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }
            _cartStore.Cart = cart;
        }

        /// <summary>
        /// Полностью удаляет товар
        /// из корзины по id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
            _cartStore.Cart = cart;
        }

        /// <summary>
        /// Очищает корзину
        /// </summary>
        public void RemoveAll()
        {
            var cart = _cartStore.Cart;
            cart.Items.Clear();
            _cartStore.Cart = cart;
        }

        /// <summary>
        /// Увеличивает количество товара id
        /// на единицу
        /// </summary>
        /// <param name="id"></param>
        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
                item.Quantity++;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            _cartStore.Cart = cart;
        }

        /// <summary>
        /// Проебразование для представления
        /// </summary>
        /// <returns></returns>
        public CartViewModel TransformCart()
        {
            var products = _productData.GetProducts(
                new ProductFilter()
                {
                    Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
                }).Select(p => new ProductItemViewModel()
                    {
                        Id = p.Id,
                        ImageUrl = p.ImageUrl,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        Brand = p.Brand != null ? p.Brand.Name : string.Empty
                    }).ToList();

            var r = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(x => products.First(y => y.Id == x.ProductId), x => x.Quantity)
            };
            return r;
        }

        public IList<OrderItemDto> GetOrderItems()
        {
            var result = _productData.GetProducts(
                new ProductFilter()
                {
                    Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
                }).Select(p => new OrderItemDto()
                    {
                        ProductId = p.Id,
                        Price = p.Price
                    }).ToList();

            foreach (var item in result)
            {
                var cartItem = _cartStore.Cart.Items.First(ci => ci.ProductId == item.ProductId);
                if (cartItem != null)
                    item.Quantity = cartItem.Quantity;
            }

            return result;
        }
    }
}
