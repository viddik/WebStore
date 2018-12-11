using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    public class NamedOrderedEntity : NamedEntity, IOrderedEntity
    {
        [Required()]
        public int Order { get; set; }
    }
}
