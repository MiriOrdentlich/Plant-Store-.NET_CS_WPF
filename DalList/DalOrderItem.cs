using DO;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        if (DataSource.indexOrderItem == DataSource.OrderItemsList.Length) 
            throw new Exception("No place for new item");//the array is full
        DataSource.OrderItemsList[DataSource.indexOrderItem++] = orderItem;
        return orderItem.Id;
    }
    public OrderItem GetByID(int id)
    {
        OrderItem temp;
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].Id == id) //search the wanted order item
            {
                temp = DataSource.OrderItemsList[i];
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
            if (DataSource.OrderItemsList[i].ProductID == product && DataSource.OrderItemsList[i].OrderID == order)
            {
                temp = DataSource.OrderItemsList[i];
                return temp;
            }
        }
        throw new Exception("Item doesn't exist");
    }  
    public void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].Id == orderItem.Id)
            {
                DataSource.OrderItemsList[i] = orderItem;
                return;
            }
        }
        throw new Exception("Item doesn't exist");
    }
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.indexOrderItem; i++)
        {
            if (DataSource.OrderItemsList[i].Id == id)
            {
                DataSource.OrderItemsList[i] = DataSource.OrderItemsList[DataSource.indexOrderItem--];
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
            onlyOrderItems[i] = DataSource.OrderItemsList[i];
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
            if (DataSource.OrderItemsList[i].OrderID == id)
            {
                helpOI = DataSource.OrderItemsList[i];
                tempArr[j++] = helpOI;
            }
        }
        return tempArr;
    }
}
