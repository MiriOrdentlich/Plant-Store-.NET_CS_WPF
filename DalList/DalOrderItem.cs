using DalApi;
using DO;
using System.Linq;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem orderItem)
    {
        // search for orderItem in list:
        if (DataSource.OrderItemsList.Contains(orderItem)) // if found orderItem -> throw exception
            throw new Exception("Order item already exists");
        DataSource.OrderItemsList.Add(orderItem); // if orderItem isn't in list, add orderItem to list
        return orderItem.Id;
    }

    public OrderItem GetByID(int id)
    {
        //search orderItemList for order item that match the given id
        //if order item not found throw exception
        OrderItem p = DataSource.OrderItemsList.Find(x => x?.Id == id) ?? throw new Exception("Id Number doesn't exist");
        return p;
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
