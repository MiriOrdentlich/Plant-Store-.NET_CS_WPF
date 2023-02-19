using System.Diagnostics;
using System.Threading;
namespace Simulator;

static internal class Simulator
{
    private static readonly BlApi.IBl bl = BlApi.Factory.Get()!;
    private static Func<Thread> Report(BO.Order ord, BO.OrderStatus oldStat, DateTime startTime, BO.OrderStatus newStat, DateTime finishTime) { }
    //private static Thread Report(BO.Order, DateTime time);
    private static volatile bool Active;
    public static void Activate()
    {
        new Thread(()=>
        {
            Active = true;
            while(Active)
            {
                //get the next order to simulate: the order with status "Confirmed" and which
                //has the oldest confirm date
                int? orderId = bl.Order.GetOldest();
                if (orderId != null)
                {
                    BO.Order ord = bl.Order.GetOrderInfo(orderId ?? -1);
                    Random random = new();
                    int delay = random.Next(3, 10);//the time it takes to complete the task
                    DateTime time = DateTime.Now + new TimeSpan(delay * 1000);
                    Report(ord, time);
                    Thread.Sleep(delay * 1000);
                    Report(finished);
                    bl.Order.UpdateOrderShipping(orderId ?? -1);
                }
                Thread.Sleep(1 * 1000);
            }
        }).Start();
    }

    static public void Register(Delegate method, object arg)
    {
        Report += method;
    }

    static public void Unregister(Delegate method, params object[] args) { }


    //static public void BeginInvoke(Delegate method, params object[] args)
    //{
    //    //if (!isTimerRun)
    //    //{
    //    //    stopwatch.Restart();
    //    //    isTimerRun = true;

    //    //    timerThread = new Thread(runTimer);
    //    //    timerThread.Start();
    //    //}
    //    while(true)
    //    {

    //    }
    //}
}
