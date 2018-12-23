using WebStore.Domain.ViewModel.Cart;

namespace WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
