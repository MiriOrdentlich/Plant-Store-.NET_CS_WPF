using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem> //define empty CRUD interface for OrderItem entity
{
    //define methods that special to OrderItem
    public OrderItem GetByProductAndOrder(int orderId ,int productId);
    public IEnumerable<OrderItem?> GetAllOrderProducts(int orderId);

}
