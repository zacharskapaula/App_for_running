using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Timers;

namespace App6
{
    public class TimerModel : INotifyPropertyChanged
    {
        Stopwatch stopwatch = new Stopwatch();
        private Timer time = new Timer();
        
        private string _hours;
        public string HoursCounter
        {
            get { return _hours; }
            set
            {
                _hours = value;
                OnPropertyChanged("HoursCounter");
            }
        }

        private string _minutes;
        public string MinutesCounter
        {
            get { return _minutes; }
            set
            {
                _minutes = value;
                OnPropertyChanged("MinutesCounter");
            }
        }

        private string _seconds;
        public string SecondsCounter
        {
            get { return _seconds; }
            set
            {
                _seconds = value;
                OnPropertyChanged("SecondsCounter");
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
        /*public void StopTimer()
        {
            stopwatch.Stop();
            HoursCounter = stopwatch.Elapsed.Hours.ToString();
            MinutesCounter = stopwatch.Elapsed.Minutes.ToString();
            SecondsCounter = stopwatch.Elapsed.Seconds.ToString();
        }*/

        public TimerModel()
        {
            stopwatch.Start();
            HoursCounter = stopwatch.Elapsed.Hours.ToString();
            MinutesCounter = stopwatch.Elapsed.Minutes.ToString();
            SecondsCounter = stopwatch.Elapsed.Seconds.ToString();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                HoursCounter = stopwatch.Elapsed.Hours.ToString();
                MinutesCounter = stopwatch.Elapsed.Minutes.ToString();
                SecondsCounter = stopwatch.Elapsed.Seconds.ToString();
                return true;

            });
        }

        

    }
}
