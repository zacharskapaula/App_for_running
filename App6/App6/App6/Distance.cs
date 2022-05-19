using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using Xamarin.Essentials;


namespace App6
{
    public class Distance : INotifyPropertyChanged
    {

        //double distance = Location.CalculateDistance(Xamarin.Essentials.Location.CalculateDistance locationStart, latitudeEnd, longitudeEnd, Xamarin.Essentials.DistanceUnits units);
        
        

        private string startLocation;
        public string GetSatrtLocation
        {
            get { return startLocation; }
            set
            {
                startLocation = value;
                OnPropertyChanged("startLocation");
            }
        }

        public Distance()
        {
           



        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
