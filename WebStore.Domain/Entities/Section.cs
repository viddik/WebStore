using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    [Table("Sections")]
    public class Section : NamedOrderedEntity
    {
        /// <summary>
        /// Родительская секция (при наличии)
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// В сочетании с ParentId здесь создается связь
        /// таблица с самой собой по внешнему ключу
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Section ParentSection { get; set; }

        /// <summary>
        /// Коллекция товаров, принадлежащих секции
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }
    }
}
