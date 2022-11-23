using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem> //define empty CRUD interface for OrderItem entity
{
    //define methods that special to OrderItem
    IEnumerable<OrderItem?> GetByProductAndOrder(int orderId ,int productId);
    IEnumerable<OrderItem?> GetAllOrderProducts(int orderId);

}
