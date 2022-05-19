using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Timers;
using static App6.TimeItems;
using Xamarin.Forms.Internals;
using System.Runtime.CompilerServices;

namespace App6
{
    public class TimerModel : INotifyPropertyChanged
    {
        Stopwatch stopwatch = new Stopwatch();
        private Timer time = new Timer();
        //private bool timerRunning;
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
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
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
        DateTime _currentStartTime;
        public DateTime CurrentStartTime
        {
            get => _currentStartTime;
            set => SetProperty(ref _currentStartTime, value);
            
        }

     
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
            var item = new TimeItems
            {
                Start = CurrentStartTime,
                Stop = DateTime.Now
            };
        }

        /*public TimerModel(int i)
        {
            HoursCounter = stopwatch.Elapsed.Hours.ToString();
            MinutesCounter = stopwatch.Elapsed.Minutes.ToString();
            SecondsCounter = stopwatch.Elapsed.Seconds.ToString();
            stopwatch.Stop();
            
        } */
    }
}
