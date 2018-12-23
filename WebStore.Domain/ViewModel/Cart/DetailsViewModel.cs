using WebStore.Domain.ViewModel.Order;

namespace WebStore.Domain.ViewModel.Cart
{
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }

        public OrderViewModel OrderViewModel { get; set; }
    }
}
