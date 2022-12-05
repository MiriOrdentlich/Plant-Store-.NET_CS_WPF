using BlApi;
using BO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.OrderTracking TrackOrder(int orderId)
    {
        var order = dal.Order.GetById(orderId); //EXCEPTION??? 
        BO.OrderTracking trackOrder = new BO.OrderTracking()
        {
            Id = orderId,
            Status = GetOrderStatus(order),
            Tracking = new List<Tuple<DateTime, string>>()
        };
        if (order.DeliveryDate != null)
            return BO.OrderStatus.Delivered;
        if (order.ShipDate != null)
            return BO.OrderStatus.Shipped;
        else //(order.OrderDate != null)
            return BO.OrderStatus.Confirmed
        return trackOrder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order GetOrderInfo(int orderId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
   /// 
   /// </summary>
   /// <returns></returns>
   /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<BO.OrderForList?> getOrdersList()
    {
        var DOorderList = dal.Order.GetAll(); //get orders list from data layer
        return from DOorder in DOorderList
               let 
               select new BO.OrderForList()
               {
                   Id = DOorder.Value.Id,
                   CustomerName = DOorder.Value.CustomerName,
                   TotalPrice = DOorder,
                   AmountOfItems = sbyte,
                   Status =                   
               };
        //IEnumerable<BO.OrderForList?> BOorderList = new List<BO.OrderForList>();
        //foreach (var DOorder in DOorderList) 
        //{
        //    BOorderList
        //}
        ////BO.OrderForList
        //return l;
    }

    /// <summary>
    /// help method
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus GetOrderStatus(DO.Order order)
    {
        if (order.DeliveryDate != null)
            return BO.OrderStatus.Delivered;
        if (order.ShipDate != null)
            return BO.OrderStatus.Shipped;
        else //(order.OrderDate != null)
            return BO.OrderStatus.Confirmed;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateOrderShipping(int orderId)
    {
        throw new NotImplementedException();
    }
}
