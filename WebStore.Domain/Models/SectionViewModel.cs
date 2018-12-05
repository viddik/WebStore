using System.Collections.Generic;
using WebStore.Domain.Entities.Base;

namespace WebStore.Models
{
    public class SectionViewModel : NamedOrderedEntity
    {
        /// <summary>
        /// Дочерние секции
        /// </summary>
        public List<SectionViewModel> ChildSections { get; set; }

        /// <summary>
        /// Родительская секция
        /// </summary>
        public SectionViewModel ParentSection { get; set; }

        public SectionViewModel()
        {
            ChildSections = new List<SectionViewModel>();
        }
    }
}
