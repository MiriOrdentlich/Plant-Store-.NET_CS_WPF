﻿using BO;
using DO;
using System.Security.Cryptography;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private static readonly DalApi.IDal dal = DalApi.Factory.Get()!;

    /// <summary>
    /// returns to simulator the id of the order with status confirmed that has
    /// the oldest 'confirmed' date.
    /// </summary>
    /// <returns>Order Id</returns>
    public int? GetNextOrder()
    {
        try
        {
            //get all confirmed orders (order get confirmed when created), sort them by confimed date and get the fi
            var confirmedOrder = dal.Order.GetAll(order => order?.DeliveryDate == null).OrderBy(ord => sorterDate(ord)).FirstOrDefault();
            return confirmedOrder?.Id;
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }


    /// <summary>
    /// help method for GetNextOrder method
    /// </summary>
    /// <param name="order"></param>
    /// <returns>datetime to sort by</returns>
    private DateTime? sorterDate(DO.Order? order)
    {

        if (order?.ShipDate != null)
            return order?.ShipDate;
        if (order?.OrderDate != null)
            return order?.OrderDate;
        return null;
    }


    /// <summary>
    /// method gets dates of different statuses in oreder to follow order. 
    /// </summary>
    /// <param name="orderId">used to find the order from data to track</param>
    /// <returns>list of tuples: (date, what happens in this date)</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.OrderTracking TrackOrder(int orderId)
    {
        try
        {
            if (orderId < 100000)
                throw new BlInvalidEntityException("Order ID", 1);
            var order = dal.Order.Get(x => x?.Id == orderId); //get order from data by the given ID
            BO.OrderTracking trackOrder = new BO.OrderTracking()
            {
                Id = orderId,
                Status = GetOrderStatus(order), //current status 
                Tracking = new List<Tuple<DateTime?, string>>() //create empty tuple list
            };

            //add values to tuple list Tracking:
            if (order.OrderDate != null) //else => order isn't confirmed => order isn't shipped (no status to add)
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
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// get a DO.Order ID and return its BO.Order version
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns>logical layer order</returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order GetOrderInfo(int orderId)
    {
        try
        {
            if (orderId < 100000)
                throw new BlInvalidEntityException("Order ID", 1);
            var order = dal.Order.Get(x => x?.Id == orderId);//get order from data by the given ID
            return GetBoOrder(order);
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// get order of list and return a OrderForList
    /// </summary>
    /// <returns></returns> OrderForList type
    public IEnumerable<BO.OrderForList?> getOrdersList()
    {
        return from doOrder in dal.Order.GetAll() //get orders list from data layer
               let orderItems = dal.OrderItem.GetAll(x => x?.OrderID == doOrder?.Id) //get all order items from data layer that belong to the current order
               select new BO.OrderForList()
               {
                   Id = doOrder?.Id ?? 0,
                   CustomerName = doOrder?.CustomerName,
                   AmountOfItems = orderItems.Sum(x => x?.Amount ?? 0),
                   TotalPrice = orderItems.Sum(x => x?.Price ?? 0 * x?.Amount ?? 0),
                   Status = GetOrderStatus((DO.Order)doOrder)  //NEED TO CHECK                 
               };
    }

    /// <summary>
    /// update order to Delivered => order arrived to client
    /// </summary>
    /// <param name="orderId">order to ship</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public BO.Order UpdateOrderDelivery(int orderId)
    {
        try
        {
            if (orderId < 100000)
                throw new BlInvalidEntityException("Order ID", 1);
            var doOrder = dal.Order.Get(x => x?.Id == orderId);
            if (GetOrderStatus(doOrder) == OrderStatus.Confirmed)//can't deliver order if haven't shipped it yet
                throw new BO.BlInvalidEntityException("Order", 2, "shipped");
            if (GetOrderStatus(doOrder) == OrderStatus.Shipped) //order stage is shipped => order haven't been delivered yet
            {
                var boOrder = GetBoOrder(doOrder);
                doOrder.DeliveryDate = DateTime.Now;
                boOrder.DeliveryDate = DateTime.Now;
                boOrder.Status = OrderStatus.Delivered;
                dal.Order.Update(doOrder);
                return boOrder;
            }
            else
                throw new BO.BlInvalidEntityException("Order", 3, "delivered"); //exception order already delivered
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// update order to Shipped => order sent to client
    /// </summary>
    /// <param name="orderId">id of order to ship</param>
    /// <returns></returns>
    /// <exception cref="BlInvalidEntityException"></exception>
    /// <exception cref="BO.BlInvalidEntityException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public BO.Order UpdateOrderShipping(int orderId)
    {
        try
        {
            if (orderId < 100000)
                throw new BlInvalidEntityException("Order ID", 1);
            var doOrder = dal.Order.Get(x => x?.Id == orderId);
            if (GetOrderStatus(doOrder) == OrderStatus.Confirmed) //order stage is confirmed => order hasn't shipped yet
            {
                var boOrder = GetBoOrder(doOrder);
                doOrder.ShipDate = DateTime.Now;
                boOrder.ShipDate = DateTime.Now;
                boOrder.Status = OrderStatus.Shipped;
                dal.Order.Update(doOrder);
                return boOrder;
            }
            else
                throw new BO.BlInvalidEntityException("Order", 3, "shipped");
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }

    /// <summary>
    /// help method. use dates of order (in data layer) in order to get status of order
    /// </summary>
    /// <param name="order">order from data layer</param>
    /// <returns>status of order from logical layer</returns>
    private BO.OrderStatus GetOrderStatus(DO.Order? order)
    {
        if (order?.DeliveryDate != null)
            return BO.OrderStatus.Delivered;
        if (order?.ShipDate != null)
            return BO.OrderStatus.Shipped;
        return BO.OrderStatus.Confirmed;
    }

    /// <summary>
    /// help method. convert order from data layer to a new order in logical layer
    /// </summary>
    /// <param name="order">order from data layer</param>
    /// <returns>order from logical layer</returns>
    private BO.Order GetBoOrder(DO.Order order)
    {
        try
        {
            if (order.Id < 100000)
                throw new BlInvalidEntityException("Order ID", 1);

            var doOrderItems = dal.OrderItem.GetAll(x => x?.OrderID == order.Id);
            var boOrderItems = (from doOrderItem in doOrderItems
                                let productId = doOrderItem?.ProductID ?? 0
                                select new BO.OrderItem() //convert orderItems items from DO to BO
                                {
                                    Id = doOrderItem?.Id ?? 0,
                                    ProductID = doOrderItem?.ProductID ?? 0,
                                    Name = dal.Product.Get(x => x?.Id == productId).Name,
                                    Price = doOrderItem?.Price ?? 0,
                                    Amount = doOrderItem?.Amount ?? 0,
                                    TotalPrice = doOrderItem?.Price ?? 0 * doOrderItem?.Amount ?? 0 //logical considerations 
                                }).ToList();

            return new BO.Order()
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryDate = order.DeliveryDate,
                CustomerAddress = order.CustomerAddress,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                Status = GetOrderStatus(order),
                Items = boOrderItems,
                TotalPrice = boOrderItems.Sum(x => x.TotalPrice)
            };
        }
        catch (DO.DalDoesNotExistIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message, ex);
        }
    }
}
