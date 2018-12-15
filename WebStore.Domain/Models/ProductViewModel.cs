using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Base;

namespace WebStore.Models
{
    public class ProductViewModel : NamedOrderedEntity
    {
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
    }
}
