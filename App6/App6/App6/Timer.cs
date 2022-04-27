using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Timers;

namespace App6
{
    public class Timer : INotifyPropertyChanged
    {
        Stopwatch stopwatch = new Stopwatch();
<<<<<<< Updated upstream:App6/App6/App6/TimerModel.cs
        private Timer time = new Timer();
        //private bool timerRunning;
        private string _stopWatchHours;

        public string StopWatchHours
=======
        private System.Timers.Timer time = new System.Timers.Timer();
        
        private string _hours;
        public string HoursCounter
>>>>>>> Stashed changes:App6/App6/App6/Timer.cs
        {
            get { return _stopWatchHours; }
            set
            {
                _stopWatchHours = value;
                OnPropertyChanged("StopWatchHours");
            }
        }

        private string _stopWatchMinutes;
        public string StopWatchMinutes
        {
            get { return _stopWatchMinutes; }
            set
            {
                _stopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes");
            }
        }

        private string _stopWatchSeconds;
        public string StopWatchSeconds
        {
            get { return _stopWatchSeconds; }
            set
            {
                _stopWatchSeconds = value;
                OnPropertyChanged("StopWatchSeconds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Timer()
        {
            stopwatch.Start();
            StopWatchHours = stopwatch.Elapsed.Hours.ToString();
            StopWatchMinutes = stopwatch.Elapsed.Minutes.ToString();
            StopWatchSeconds = stopwatch.Elapsed.Seconds.ToString();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                StopWatchHours = stopwatch.Elapsed.Hours.ToString();
                StopWatchMinutes = stopwatch.Elapsed.Minutes.ToString();
                StopWatchSeconds = stopwatch.Elapsed.Seconds.ToString();
                return true;

            });
        }
    }
}
