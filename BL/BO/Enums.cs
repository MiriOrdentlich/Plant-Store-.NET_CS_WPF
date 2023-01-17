using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    Flowering, 
    Ferns,
    CactiAndSucculents,
    PalmsAndTrees, 
    HangingAndClimbing, 
    Edibles, 
    None 
}
