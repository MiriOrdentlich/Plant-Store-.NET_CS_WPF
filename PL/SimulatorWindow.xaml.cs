using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Simulator;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private Stopwatch stopWatch;
        private BackgroundWorker timerWorker;
        private bool isTimerRun;
        private bool canClose = false; //prevent user from being able to force close on the window (by clicking X)

        private int id;
        private BO.OrderStatus? oldStat;
        private BO.OrderStatus newStat;
        private DateTime startProccess;
        private DateTime finishProccess;
        private int delay;
        private string message;

        //Closing = 
        //private DispatcherTimer _timer;
        //private TimeSpan _time;

        //dependency properties for the textBlocks on the window:


        public string timerWatch
        {
            get { return (string)GetValue(timerWatchProperty); }
            set { SetValue(timerWatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timerWatch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerWatchProperty =
            DependencyProperty.Register("timerWatch", typeof(string), typeof(SimulatorWindow));


        public string oldStatus
        {
            get { return (string)GetValue(oldStatusProperty); }
            set { SetValue(oldStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for oldStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty oldStatusProperty =
            DependencyProperty.Register("oldStatus", typeof(string), typeof(SimulatorWindow));


        public string newStatus
        {
            get { return (string)GetValue(newStatusProperty); }
            set { SetValue(newStatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newStatusProperty =
            DependencyProperty.Register("newStatus", typeof(string), typeof(SimulatorWindow));


        public SimulatorWindow()
        {
            InitializeComponent();

            Closing += SimulatorWindow_Closing;

            stopWatch = new Stopwatch();
            stopWatch.Start();
            //isTimerRun = true;

            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += timerWorker_DoWork;
            timerWorker.ProgressChanged += timerWorker_ProgressChanged;
            timerWorker.RunWorkerCompleted += timerWorker_RunWorkerCompleted;
            timerWorker.WorkerReportsProgress = true;
            timerWorker.WorkerSupportsCancellation = true;
            timerWorker.RunWorkerAsync();

            
        }

        private void SimulatorWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = !canClose;
        }

        private void timerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Simulator.Simulator.Activate();

                Simulator.Simulator.RegisterRep1(doRep1);
                Simulator.Simulator.RegisterRep2(doRep2);
                Simulator.Simulator.RegisterRep3(doRep3);
                while (!timerWorker.CancellationPending)
                {
                    timerWorker.ReportProgress(0);
                    Thread.Sleep(1000);
                }
                stopWatch.Stop();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void timerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    timerWatch = stopWatch.Elapsed.ToString().Substring(0, 8);
                    break;
                case 1:
                    oldStatus = oldStat.ToString() + startProccess.ToString("g"); // "g" format string represents "MM/dd/yyyy h:mm tt"
                    newStatus = newStat.ToString() + finishProccess.ToString("g");
                    break;
                case 2:
                    MessageBox.Show(message + id.ToString());
                    break;
                case 3:
                    MessageBox.Show(message);

                    break;
                default:
                    break;
            }

        }

        private void timerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.UnregisterRep1(doRep1);
            Simulator.Simulator.UnregisterRep2(doRep2);
            Simulator.Simulator.UnregisterRep3(doRep3);
        }
            /*
            if (e.Cancelled == true)
            {
                // e.Result throw System.InvalidOperationException
                resultLabel.Content = "Canceled!";
            }
            else if (e.Error != null)
            {
                // e.Result throw System.Reflection.TargetInvocationException
                resultLabel.Content = "Error: " + e.Error.Message; //Exception Message
            }
            else
            {
                long result = (long)e.Result;
                if (result < 1000)
                    resultLabel.Content = "Done after " + result + " ms.";
                else
                    resultLabel.Content = "Done after " + result / 1000 + " sec.";
            }
            */

        
        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Active = false;
            timerWorker.CancelAsync();
            Simulator.Simulator.UnregisterRep1(doRep1);
            Simulator.Simulator.UnregisterRep2(doRep2);
            Simulator.Simulator.UnregisterRep3(doRep3);
            Close();
        }


        public void doRep1(BO.Order ord, BO.OrderStatus? _oldStat, DateTime _startProccess, BO.OrderStatus _newStat, DateTime _finishProccess)
        {
            id = ord.Id;
            oldStat = _oldStat;
            newStat = _newStat;
            startProccess = _startProccess;
            finishProccess = _finishProccess;
            delay = (int)(finishProccess - startProccess).TotalSeconds; //get the duration between the 2 time stamps

            timerWorker.ReportProgress(1);
        }

        public void doRep2(string msg)
        {
            message = msg;
            timerWorker.ReportProgress(2);
        }

        public void doRep3(string msg)
        {
            message = msg;
            timerWorker.ReportProgress(3);
        }
    }
}
