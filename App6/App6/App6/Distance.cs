using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace App6
{
    class Distance
    {
       

        public async void GetLoc()
        {
            Location result = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(10)));
            string res = $"Lat: { result.Latitude}, Lng: { result.Longitude}";
            
        }

        public Distance()
        {
            GetLoc();
            
            
        }
    }
}
