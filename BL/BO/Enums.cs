using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum OrderStatus
{
    Initiated,
    Ordered,
    Paid,
    Shipped,
    Delivered,
}

public enum Category 
{
    Chairs,
    Tables,
    BigStorage,
    SmallStorage,
    Beds 
};

//public enum OrderItem
//{
//    OrderItem_A,
//}

//public enum DateTime
//{
//    DateTime_A,
//}
