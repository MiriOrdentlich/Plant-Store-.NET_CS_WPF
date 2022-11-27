using BO;


namespace BlApi;

public interface IOrder
{
    //MANAGER METHODS:
    IEnumerable<OrderForList?> GetOrders();//also used for CLIENT (?)
    BO.Order GetOrderInfo(int orderId);
    BO.Order UpdateOrderShipping(int orderId);
    BO.Order UpdateOrderIfProvided(int orderId);
    void FollowOrder(int orderId);

}
