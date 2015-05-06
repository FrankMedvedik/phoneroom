using System;
using System.Windows.Threading;
using Silverlight.Base.MVVMBaseTypes;

namespace PhoneLogic.Core.ViewModels
{
    public class TimerViewModel : ViewModelBase 
    {
        public TimerViewModel()
        {
            Start();
        }

        public void Start()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            startTime = DateTime.Now;
            
        }

        public void Stop()
        {
            timer.Stop();
        }

        private DispatcherTimer timer;
        private DateTime startTime;

        public TimeSpan TimeFromStart
        {
            get { return DateTime.Now - startTime; }
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            NotifyPropertyChanged("TimeFromStart");
        }

        
    }
}