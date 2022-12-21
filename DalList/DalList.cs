
using DalApi;

namespace Dal;

sealed internal class DalList : IDal
{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();

}
