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
    /// get a
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>logical layer order</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order GetOrderInfo(int orderId)
    {
        try
        {
            //EXCEPTION??? 
            var order = dal.Order.GetById(orderId);//get order from data by the given ID
            return GetBoOrder(order);
        }
        catch(Exception ex)
        {
            throw new Exception();
        }
       
    }

    /// <summary>
   /// 
   /// </summary>
   /// <returns></returns>
   /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<BO.OrderForList?> getOrdersList()
    {
        var doOrderList = dal.Order.GetAll(); //get orders list from data layer
        return from doOrder in doOrderList
               let orderItems = dal.OrderItem.GetAll().Select(x => x?.OrderID == doOrder?.Id)
               select new BO.OrderForList()
               {
                   Id = doOrder.Value.Id,
                   CustomerName = doOrder.Value.CustomerName,
                   AmountOfItems = orderItems.Count(),
                   TotalPrice = orderItems.Sum(x => x?.Price * x?.amount),
                   Status = GetOrderStatus(doOrder)             
               };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        var order = dal.Order.GetById(orderId);
        if(GetOrderStatus(order) == OrderStatus.Confirmed) //order stage is confirmed => order hasn't shipped yet
        {

        }
        return new BO.Order();
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
    /// <returns>status of order from logical layer</returns>
    private BO.OrderStatus GetOrderStatus(DO.Order order)
    {
        if (order.DeliveryDate != null)
            return BO.OrderStatus.Delivered;
        if (order.ShipDate != null)
            return BO.OrderStatus.Shipped;
        if (order.OrderDate != null)
            return BO.OrderStatus.Confirmed;
        else
            throw new Exception();
    }

    /// <summary>
    /// help method. convert order from data layer to a new order in logical layer
    /// </summary>
    /// <param name="order">order from data layer</param>
    /// <returns>order from logical layer</returns>
    private BO.Order GetBoOrder(DO.Order order)
    {
        var orderItems = dal.OrderItem.GetAllOrderProducts(order.Id);
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
            Items = (from doOrderItem in orderItems
                    select new BO.OrderItem() //convert orderItems items from DO to BO
                    {
                        Id = doOrderItem.Value.Id,
                        ProductID = doOrderItem.Value.ProductID,
                        Name = dal.Product.GetById(doOrderItem.Value.ProductID).Name,
                        Price = doOrderItem.Value.Price,
                        Amount = doOrderItem.Value.Amount,
                        TotalPrice = doOrderItem.Value.Price * doOrderItem.Value.Amount //logical considerations 
                    }).ToList()
        };
    }
}
