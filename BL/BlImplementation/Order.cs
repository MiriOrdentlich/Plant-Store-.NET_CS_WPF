using BlApi;
using BO;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal dal = new Dal.DalList();

    /// <summary>
    /// method gets dates of different statuses in oreder to follow order. 
    /// </summary>
    /// <param name="orderId">used to find the order from data to track</param>
    /// <returns>list of tuples: (date, what happens in this date)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.OrderTracking TrackOrder(int orderId)
    {
        var order = dal.Order.GetById(orderId); //EXCEPTION??? //get order from data by the given ID
        BO.OrderTracking trackOrder = new BO.OrderTracking()
        {
            Id = orderId,
            Status = GetOrderStatus(order), //current status 
            Tracking = new List<Tuple<DateTime?, string>>() //create empty tuple list
        };

        //add values to tuple list Tracking:
        if(order.OrderDate != null) //else => order isn't confirmed => order isn't shipped (no status to add)
        {
            trackOrder.Tracking.Add(new(order.OrderDate, "The order has been confirmed"));
            if (order.ShipDate != null) //else => order isn't shipped => order isn't delivered 
            {
                trackOrder.Tracking.Add(new(order.ShipDate, "The order has been shipped"));
                if (order.DeliveryDate != null)
                    trackOrder.Tracking.Add(new(order.DeliveryDate, "The order has been delivered"));
            }
        }  
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
        var order = dal.Order.GetById(orderId); //EXCEPTION??? //get order from data by the given ID
        var orderItems = dal.OrderItem.GetAllOrderProducts(orderId);
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

    /// <summary>
    /// help method. use dates of order (in data layer) in order to get status of order
    /// </summary>
    /// <param name="order">order from data layer</param>
    /// <returns>status of order from logic layer</returns>
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
    /// help method. convert order from data layer to a new order in logic layer
    /// </summary>
    /// <param name="order">order from data layer</param>
    /// <returns>order from logic layer</returns>
    private BO.Order GetBoOrder(DO.Order order)
    {
        return new BO.Order()
        {
            Id = order.Id,
            OrderDate = order.DeliveryDate,
            ShipDate = order.ShipDate,
            DeliveryDate = order.DeliveryDate,
            CustomerAddress = order.CustomerAddress,
            CustomerName = order.CustomerName,
            CustomerEmail = order.CustomerEmail,
            Status = GetOrderStatus(order),
        };
    }
}
