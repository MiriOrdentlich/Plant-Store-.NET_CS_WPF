using BO;


namespace BlApi;

public interface IOrder
{
    //MANAGER METHODS:
    IEnumerable<OrderForList?> getOrdersList();//also used for CLIENT (?)
    BO.Order GetOrderInfo(int orderId);
    BO.Order UpdateOrderShipping(int orderId);
    BO.Order UpdateOrderDelivery(int orderId);
    BO.OrderTracking TrackOrder(int orderId);

}
