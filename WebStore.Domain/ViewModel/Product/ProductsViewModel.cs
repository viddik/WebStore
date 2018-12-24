using System.Collections.Generic;

namespace WebStore.Domain.ViewModel.Product
{
    public class ProductsViewModel
    {
        public string Title { get; set; }

        public IEnumerable<ProductItemViewModel> Products { get; set; }
    }
}
