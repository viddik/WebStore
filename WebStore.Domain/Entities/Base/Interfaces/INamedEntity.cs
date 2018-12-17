using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; set; }
    }
}
