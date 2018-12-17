using System.Collections.Generic;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        /// <summary>
        /// Список секций
        /// </summary>
        /// <returns></returns>
        IEnumerable<SectionDto> GetSections();

        /// <summary>
        /// Список брендов
        /// </summary>
        /// <returns></returns>
        IEnumerable<BrandDto> GetBrands();

        /// <summary>
        /// Список товаров
        /// </summary>
        /// <param name="filter">Фильтр товаров</param>
        /// <returns></returns>
        IEnumerable<ProductDto> GetProducts(ProductFilter filter);

        /// <summary>
        /// Товар по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        ProductDto GetProductById(int id);
    }
}
