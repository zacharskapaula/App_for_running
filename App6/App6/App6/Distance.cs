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

        public async void GetLoc()
        {
            Location start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(2)));
            string start_location_result = $"Lat: { start_location.Latitude}, Lng: { start_location.Longitude}";

            Location location_ontheroad = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(2)));
            string res = $"Lat: { location_ontheroad.Latitude}, Lng: { location_ontheroad.Longitude}";


            double lat = 34.23;
            double longt = 54.23;

            if (lat != start_location.Latitude && longt != start_location.Longitude)
            {
                double route = start_location.Latitude - lat;
                double route2 = start_location.Latitude - longt;

            }
        }

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
            GetLoc();

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
