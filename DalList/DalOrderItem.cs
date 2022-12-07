using DalApi;
using DO;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public int Add(OrderItem orderItem)
    {
        // search for orderItem in list:
        if (DataSource.OrderItemsList.Contains(orderItem)) // if found orderItem -> throw exception
            throw new DO.DalAlreadyExistsIdException(orderItem.Id, "Order Item");
        orderItem.Id = DataSource.nextOrderItemNumber;
        DataSource.OrderItemsList.Add(orderItem); // if orderItem isn't in list, add orderItem to list
        return orderItem.Id;
    }
    public OrderItem GetById(int id)
    {
        //search orderItemList for order item that match the given id
        //if order item not found throw exception
        OrderItem p = DataSource.OrderItemsList.Find(x => x?.Id == id) ??
            throw new DO.DalDoesNotExistIdException(id, "Order Item");
        ;
        return p;
    }
    public OrderItem GetByProductAndOrder(int productID, int orderID)
    {
        //search orderItemList for order item that match the given product and order ids
        //if order item not found throw exception
        OrderItem p = DataSource.OrderItemsList.Find(x => x?.ProductID == productID && x?.ProductID == orderID) ?? 
            throw new DO.DalDoesNotExistIdException(0, "Order Item"); //??????
        return p;
    }  
    public void Update(OrderItem orderItem)
    {
        // search for order item in list. if didn't find order item -> throw exception
        if (DataSource.OrderItemsList.RemoveAll(x => x?.Id == orderItem.Id) == 0)
            throw new DO.DalDoesNotExistIdException(orderItem.Id, "OrderItem");
        DataSource.OrderItemsList.Add(orderItem);
    }
    public void Delete(int id)
    {
        // search for order item in list and remove it from list. if didn't find order item -> throw exception
        if (DataSource.OrderItemsList.RemoveAll(x => x?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Order Item");
    }
    public IEnumerable<OrderItem?> GetAll()
    {
        return new List<OrderItem?>(DataSource.OrderItemsList);
    }   
    public IEnumerable<DO.OrderItem?> GetAllOrderProducts(int orderID)
    {
        List<OrderItem?> tempArr = new List<OrderItem?>();
        foreach (var item in DataSource.OrderItemsList)
        {
            if(item?.OrderID == orderID)
                tempArr.Add(item);
        }
        return tempArr;
    }
}
