using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Dto.Product
{
    public class ProductDto : NamedOrderedEntity
    {
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public BrandDto Brand { get; set; }

        public SectionDto Section { get; set; }
    }
}
