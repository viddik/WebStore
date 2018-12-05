using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    [Table("Brands")]
    public class Brand : NamedOrderedEntity
    {
        /// <summary>
        /// Коллекция товаров, принадлежащих бренду
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
