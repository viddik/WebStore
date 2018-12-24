using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.ViewModel.Product
{
    public class ProductItemViewModel : NamedOrderedEntity
    {
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Brand { get; set; }

    }
}
