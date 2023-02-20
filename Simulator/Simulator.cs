using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Simulator;

static public class Simulator
{
    private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
    private static readonly Random random = new Random();
    public delegate void Report1(BO.Order ord, BO.OrderStatus? oldStat, DateTime startTime, BO.OrderStatus newStat, DateTime finishTime); //got an order from pl
    public static Report1 report1;
    public delegate void Report2(string msg);
    public static Report2 report2;
    public delegate void Report3(string msg);
    public static Report3 report3;

    public static volatile bool Active;
    public static void Activate()
    {
        new Thread(()=>
        {
            Active = true;
            while(Active)
            {
                // get the next order to simulate: the order with status "Confirmed" which
                // has the oldest confirm date
                int? orderId = bl.Order.GetNextOrder();
                if (orderId != null)
                {
                    BO.Order ord = bl.Order.GetOrderInfo(orderId ?? -1);
                    int delay = random.Next(3, 11);//the time it takes to complete the task
                    DateTime time = DateTime.Now + new TimeSpan(delay * 1000);
                    if (ord.Status == BO.OrderStatus.Confirmed)
                    {
                        report1(ord, ord.Status, DateTime.Now, BO.OrderStatus.Shipped, time);
                        Thread.Sleep(delay * 1000);
                        bl.Order.UpdateOrderShipping(orderId ?? -1);
                    }
                    else //=> if(ord.Status == BO.OrderStatus.Shipped)
                    {
                        report1(ord, ord.Status, DateTime.Now, BO.OrderStatus.Delivered, time);
                        Thread.Sleep(delay * 1000);
                        bl.Order.UpdateOrderDelivery(orderId ?? -1);
                    }
                    if(Active)
                        report2("Finished updating the order id: ");
                }
                else
                    Active = false;
                Thread.Sleep(1000);
            }
            report3("Finished simulation");
        }).Start();
    }

    public static void RegisterRep1(Report1 rep)
    {
        report1 += rep;
    }
    public static void UnregisterRep1(Report1 rep)
    {
        report1 -= rep;

    }
    public static void RegisterRep2(Report2 rep)
    {
        report2 += rep;
    }
    public static void UnregisterRep2(Report2 rep)
    {
        report2 -= rep;

    }
    public static void RegisterRep3(Report3 rep)
    {
        report3 += rep;

    }
    public static void UnregisterRep3(Report3 rep)
    {
        report3 -= rep;
    }

}


