using DO;

namespace DalApi;

public interface IDal
{
    //define properties for the entities:
    IOrder Order { get; }
    IOrderItem OrderItem { get; }
    IProduct Product { get; }
}
