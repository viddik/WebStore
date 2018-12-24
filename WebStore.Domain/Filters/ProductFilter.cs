using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Filters
{
    public class ProductFilter
    {
        /// <summary>
        /// Секция, к которой принадлежит товар
        /// </summary>
        public int? SectionId { get; set; }

        /// <summary>
        /// Бренд товара
        /// </summary>
        public int? BrandId { get; set; }

        /// <summary>
        /// Идентификаторы товаров
        /// </summary>
        public IList<int> Ids { get; set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public int? PageSize { get; set; }
    }
}
