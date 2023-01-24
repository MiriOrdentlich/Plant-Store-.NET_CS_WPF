using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    readonly string s_OrderItems = "orderItems";

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> listOrderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (filter == null)
            return listOrderItems.Select(oi => oi).OrderBy(oi => oi?.Id);
        else
            return listOrderItems.Where(filter).OrderBy(oi => oi?.Id);
    }

    public DO.OrderItem Get(Func<DO.OrderItem?, bool> filter)
    {
        List<DO.OrderItem?> listOrderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem pro = listOrderItems.Where(filter).FirstOrDefault() ??
            throw new DO.DalDoesNotExistIdException(-1, "Order Item");
        return pro;
    }

    public int Add(DO.OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.FirstOrDefault(oi => oi?.Id == orderItem.Id) != null)
            throw new DO.DalAlreadyExistsIdException(orderItem.Id, "Order Item");

        orderItem.Id = Config.GetNextOrderItemId();

        listOrderItems.Add(orderItem);
        Config.SetNextOrderId(orderItem.Id + 1);

        XmlTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);

        return orderItem.Id;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.RemoveAll(oi => oi?.Id == id) == 0)
            throw new DO.DalDoesNotExistIdException(id, "Order Item");
        XmlTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.Id);
        Add(orderItem);
    }

    public DO.OrderItem GetByProductAndOrder(int orderId, int productId)
    {
        //search orderItesmList for order item that match the given product and order ids
        //if order item not found throw exception

        List<DO.OrderItem?> listOrderItems = XmlTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);
        return (listOrderItems.Where(oi => oi?.ProductID == productId && oi?.ProductID == orderId).OrderBy(oi => oi?.Id)).FirstOrDefault() ??
            throw new DO.DalDoesNotExistIdException(-1, "Order Item"); 
    }
}
