using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        if (DataSource.Config.indexOrderItem == DataSource.OrderItemArr.Length)
            throw new Exception("No place for new item\n");
        for(int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == orderItem.Id)
                throw new Exception("Item already exist\n");
        }
        DataSource.OrderItemArr[DataSource.Config.indexOrderItem] = orderItem;
        return orderItem.Id;
    }
    public OrderItem GetByID(int id)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == id)
                return DataSource.OrderItemArr[i];
        }
        throw new Exception("Item doesn't exist\n");
    }
    public OrderItem GetByProductAndOrder(int product, int order)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].ProductID == product && DataSource.OrderItemArr[i].OrderID == order)
                return DataSource.OrderItemArr[i];
        }
        throw new Exception("Item doesn't exist\n");
    }  
    public void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == orderItem.Id)
                DataSource.OrderItemArr[i] = orderItem;
        }
        throw new Exception("Item doesn't exist\n");
    }
    public void Delete(int id)
    {

    }
    public IEnumerable<OrderItem?> GetAll()
    {

    }
}
