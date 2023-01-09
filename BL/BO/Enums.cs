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
    [Description("Cacti & succulents")] CactiAndSucculents,
    [Description("Palms & trees")] PalmsAndTrees, 
    [Description("Hanging & climbing")] HangingAndClimbing, 
    Edibles, 
    None 
}
