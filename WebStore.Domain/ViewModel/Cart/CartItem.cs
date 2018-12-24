using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModel.Cart
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
