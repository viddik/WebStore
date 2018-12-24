using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.ViewModel.Product
{
    public class BrandViewModel : NamedOrderedEntity
    {
        /// <summary>
        /// Количество товаров бренда
        /// </summary>
        public int ProductsCount { get; set; }
    }
}
