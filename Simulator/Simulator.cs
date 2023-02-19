using System.Diagnostics;
using System.Threading;
namespace Simulator;

static internal class Simulator
{
    private static volatile Stopwatch stopwatch = Stopwatch.StartNew();
    private static volatile Thread timerThread;
    private static volatile bool isTimerRun;
    static public void stopFunc()
    {
        //stopwatch.ElapsedMilliseconds;
        if(isTimerRun)
        {
            stopwatch.Stop();
            isTimerRun = false;
        }
    }

    static public void BeginInvoke(Delegate method, params object[] args)
    {
        //if (!isTimerRun)
        //{
        //    stopwatch.Restart();
        //    isTimerRun = true;

        //    timerThread = new Thread(runTimer);
        //    timerThread.Start();
        //}
        while(true)
        {

        }
    }
}
