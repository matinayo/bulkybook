using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models.ViewModel
{
    public class OrderDetailsVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
