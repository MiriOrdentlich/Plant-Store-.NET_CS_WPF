using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }

        public string? CustomerMail { get; set; }
        public OrderItem? OrderDetails { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }

    }
}
