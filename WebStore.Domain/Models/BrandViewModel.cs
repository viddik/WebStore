using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Models
{
    public class BrandViewModel : NamedOrderedEntity
    {
        /// <summary>
        /// Количество товаров бренда
        /// </summary>
        public int ProductsCount { get; set; }
    }
}
