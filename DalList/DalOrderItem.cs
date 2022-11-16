using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        if (DataSource.Config.indexOrderItem == DataSource.OrderItemArr.Length) 
            throw new Exception("No place for new item");
        DataSource.OrderItemArr[DataSource.Config.indexOrderItem++] = orderItem;
        return orderItem.Id;
    }
    public OrderItem GetByID(int id)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == id)
                return DataSource.OrderItemArr[i];
        }
        throw new Exception("Item doesn't exist");
    }
    public OrderItem GetByProductAndOrder(int product, int order)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].ProductID == product && DataSource.OrderItemArr[i].OrderID == order)
                 return DataSource.OrderItemArr[i];
        }
        throw new Exception("Item doesn't exist");
    }  
    public void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == orderItem.Id)
            {
                DataSource.OrderItemArr[i] = orderItem;
                return;
            }
        }
        throw new Exception("Item doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == id)
            {
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[--DataSource.Config.indexOrderItem];
                return;
            }
        }
        throw new Exception("Item doesn't exist");
    }
    public OrderItem[] GetAll()
    {
        OrderItem[] onlyOrderItems = new OrderItem[DataSource.Config.indexOrderItem];
        for (int i = 0; i < onlyOrderItems.Length; i++)
        {
            onlyOrderItems[i] = DataSource.OrderItemArr[i];
        }
        return onlyOrderItems;
    }
}
