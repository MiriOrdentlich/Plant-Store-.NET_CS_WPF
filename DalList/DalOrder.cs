using DO;
using DalApi;
namespace Dal;

internal class DalOrder : IOrder
{

    public int Add(Order order) //create
    {
        // search for order in list:
        if(DataSource.OrdersList.Find(x => x?.Id == order.Id) != null) // if found order -> throw exception
            throw new DO.DalAlreadyExistsIdException(order.Id, "Order");
        order.Id = DataSource.nextOrderNumber;
        DataSource.OrdersList.Add(order); // if order isn't in list, add order to list
        return order.Id;
    }
    public Order GetById(int id) //Request
    {
        //search orderList for order that match the given id
        //if order not found throw exception
        return DataSource.OrdersList.Find(x => x?.Id == id) ??
            throw new DO.DalDoesNotExistIdException(id, "Order");
    }
    public void Update(Order order)
    {
        // search for order in list. if didn't find order -> throw exception else 
        if (DataSource.OrdersList.RemoveAll(x => x?.Id == order.Id) == 0)
            throw new DO.DalDoesNotExistIdException(order.Id, "Order");
        DataSource.OrdersList.Add(order);
    }
    public void Delete(int id)
    {
        // search for order in list. if didn't find order -> throw exception
        if ( DataSource.OrdersList.RemoveAll(x => x?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Order");
    }
    public IEnumerable<Order?> GetAll()
    {
        return new List<Order?>(DataSource.OrdersList);
    }
}
    

