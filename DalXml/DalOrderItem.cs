using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    readonly string s_OrderItems = "orderItems";

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (filter == null)
            return listOrderItems.Select(pro => pro).OrderBy(pro => pro?.Id);
        else
            return listOrderItems.Where(filter).OrderBy(pro => pro?.Id);
    }

    public DO.OrderItem Get(Func<OrderItem?, bool> filter)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        DO.OrderItem pro = listOrderItems.Where(filter).FirstOrDefault() ??
            throw new DalDoesNotExistIdException(-1, "OrderItem");
        return pro;
    }

    public int Add(DO.OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.FirstOrDefault(pro => pro?.Id == orderItem.Id) != null)
            throw new Exception("id already exist"); //new DalAlreadyExistIdException(pr.ID, "OrderItem");

        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);

        return orderItem.Id;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_OrderItems);

        if (listOrderItems.RemoveAll(pro => pro?.Id == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "OrderItem");

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_OrderItems);
    }
    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.Id);
        Add(orderItem);
    }

    public OrderItem GetByProductAndOrder(int orderId, int productId)
    {
        
        //search orderItemList for order item that match the given product and order ids
        //if order item not found throw exception
        
    }
}
