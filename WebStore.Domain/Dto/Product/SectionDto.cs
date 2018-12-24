using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Dto.Product
{
    public class SectionDto : NamedOrderedEntity
    {
        public int? ParentId { get; set; }
    }
}
