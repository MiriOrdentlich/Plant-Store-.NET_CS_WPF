using BO;


namespace BlApi;

public interface IOrder
{
    //MANAGER METHODS:
    IEnumerable<OrderForList?> getOrdersList();
    BO.Order GetOrderInfo(int orderId);
    BO.Order UpdateOrderShipping(int orderId);
    BO.Order UpdateOrderDelivery(int orderId);
    BO.OrderTracking TrackOrder(int orderId);

}
