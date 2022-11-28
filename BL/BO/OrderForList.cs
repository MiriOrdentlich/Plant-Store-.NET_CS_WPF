using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public double TotalPrice { get; set; } //string??
    public int AmountOfItems { get; set; }
    public OrderStatus Status { get; set; } 

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
