using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public enum OrderStatus
{
    Confirmed,
    Shipped,
    Delivered
};

public enum Category 
{
    Chairs,
    Tables,
    BigStorage,
    SmallStorage,
    Beds,
    None
};
