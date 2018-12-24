using System.Collections.Generic;

namespace WebStore.Domain.ViewModel.Product
{
    public class CatalogViewModel
    {
        public int? BrandId { get; set; }

        public int? SectionId { get; set; }

        public ProductsViewModel ProductsViewModel { get; set; }
    }
}
