using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    readonly string s_Orders = "orders";
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (filter == null)
            return listOrders.Select(ord => ord).OrderBy(ord => ord?.Id);
        else
            return listOrders.Where(filter).OrderBy(ord => ord?.Id);
    }

    public DO.Order Get(Func<Order?, bool> filter)
    {
        List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        DO.Order ord = listOrders.Where(filter).FirstOrDefault() ??
            throw new DalDoesNotExistIdException(-1, "Order");
        return ord;
    }

    public int Add(DO.Order order)
    {
        List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (listOrders.FirstOrDefault(ord => ord?.Id == order.Id) != null)
           throw new DO.DalAlreadyExistsIdException(order.Id, "Order");

        order.Id= Config.GetNextOrderId();
        listOrders.Add(order);
        Config.SetNextOrderId(order.Id +1);
        XmlTools.SaveListToXMLSerializer(listOrders, s_Orders);

        return order.Id;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XmlTools.LoadListFromXMLSerializer<DO.Order>(s_Orders);

        if (listOrders.RemoveAll(ord => ord?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Order");

        XmlTools.SaveListToXMLSerializer(listOrders, s_Orders);
    }

    public void Update(DO.Order order)
    {
        Delete(order.Id);
        Add(order);
    }
}


