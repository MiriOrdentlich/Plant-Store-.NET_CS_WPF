using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        if (DataSource.indexOrderItem == DataSource.OrderItemArr.Length) 
            throw new Exception("No place for new item");
        DataSource.OrderItemArr[DataSource.indexOrderItem++] = orderItem;
        return orderItem.Id;
    }
    public OrderItem GetByID(int id)
    {
        OrderItem temp;
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == id)
            {
                temp = DataSource.OrderItemArr[i];
                return temp;
            }
        }
        throw new Exception("Item doesn't exist");
    }
    public OrderItem GetByProductAndOrder(int product, int order)
    {
        OrderItem temp = new OrderItem();
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].ProductID == product && DataSource.OrderItemArr[i].OrderID == order)
            {
                temp = DataSource.OrderItemArr[i];
                return temp;
            }
        }
        throw new Exception("Item doesn't exist");
    }  
    public void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.indexOrderItem; i++)
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
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].Id == id)
            {
                DataSource.OrderItemArr[i] = DataSource.OrderItemArr[DataSource.indexOrderItem--];
                return;
            }
        }
        throw new Exception("Item doesn't exist");
    }
    public OrderItem[] GetAll()
    {
        OrderItem[] onlyOrderItems = new OrderItem[DataSource.indexOrderItem];
        for (int i = 0; i < onlyOrderItems.Length; i++)
        {
            onlyOrderItems[i] = DataSource.OrderItemArr[i];
        }
        return onlyOrderItems;
    }
    public OrderItem[] GetAllOrderProducts(int id)
    {
        OrderItem[] tempArr = new OrderItem[4]; // max product per order is 4 
        OrderItem helpOI;
        int j = 0; // index for tempArr 
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemArr[i].OrderID == id)
            {
                helpOI = DataSource.OrderItemArr[i];
                tempArr[j++] = helpOI;
            }
        }
        return tempArr;
    }
}
