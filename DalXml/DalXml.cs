using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{ 
    private DalXml() { }
    public static IDal Instance { get; } = new DalXml();
    public IOrder Order { get; } = new DalOrder();
    public IProduct Product { get; } = new DalProduct();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
    public IUser User { get; } = new DalUser();
}
