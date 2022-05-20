using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Timers;

using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.IO;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using System.Xml.Serialization;
using TcxTools;
using System.Runtime.CompilerServices;

namespace App6.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {
        public RunningPage()
        {
            InitializeComponent(); 

        }

        Location start_location;
        public async void GetStartLoc()
        {
            start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            //start_location = new Location(45.52989617735217, -122.67512739528975);
            distanceLabel.Text = start_location.ToString();

            Pin pin_start = new Pin
            {
                Label = "Start",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude)

            };
            mylocalMap.Pins.Add(pin_start);
        }

       
        Location finish_location;
        public async void GetStopLoc()
        {
            finish_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            finish_location = new Location(finish_location.Latitude, finish_location.Longitude);
            distanceLabel.Text = finish_location.ToString();

            double distance = Location.CalculateDistance(start_location, finish_location, DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distance, 2)).ToString() + "km";
            Pin pin_stop = new Pin
            {
                Label = "Stop",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
            };
            mylocalMap.Pins.Add(pin_stop);
        }

         Location on_road_location;
         public async void OnRoadLocation()
        {
            on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude);
            speedLabel.Text = on_road_location.ToString();
            
        }
        public void RoadLocationEverySeconds()
         {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    OnRoadLocation();
                    
                });
                return true; 
            });
            
            //string on_road_location_coordinate = $"Lat: {on_road_location.Latitude}, Lng: {on_road_location.Longitude}";   
        } 

        public void RoadDrawing()
        {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    OnRoadLocation();

                });
                return true;
            });
            //Xamarin.Forms.Maps.Position pos = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude);
            Polyline polyline = new Polyline
            {

                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#15A826"),
                //FillColor = Color.FromHex("#881BA1E2"),
                
                Geopath = {
                     new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude),
                     new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude),

                     new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
                 }
            };
            mylocalMap.MapElements.Add(polyline);

        }

        public void SetMap()
        {

        }

        public void StartButton_Clicked(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;
            string time = startTime.ToString();
            trainingStartTime.Text = time;
            welcomeLabel.IsVisible = false;
            timerLabel.IsVisible = true; 
            distanceLabel.IsVisible = true;
            speedLabel.IsVisible = true;
            startButton.IsVisible = false;
            stopButton.IsVisible = true;
            GetStartLoc(); 
            RoadLocationEverySeconds();
            //OnRoadLocation();

            BindingContext = new TimerModel(); 
            
        }
 
        public void StopButton_Clicked(object sender, EventArgs e)
        {
            DateTime myDate = DateTime.Now;
            string thatTime = myDate.ToString();
            trainingStopTime.Text = thatTime;
            finishLabel.IsVisible = true;
            trainingStartTime.IsVisible = true;
            trainingStopTime.IsVisible = true;
            hourLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            timerLabel.IsVisible = false;
            distanceLabel.IsVisible = true;
            speedLabel.IsVisible = false;
            welcomeLabel.IsVisible = false;
            TotalTime();
            GetStopLoc();
            //RoadDrawing();
        } 
        public void TotalTime()
        {
            DateTime arg1 = DateTime.Parse(trainingStopTime.Text);
            DateTime arg2 = DateTime.Parse(trainingStartTime.Text);
            hourLabel.Text = (arg1 - arg2).ToString();
        }

        /* public void CountDistance()
        {
            if (finish_location == start_location)
            {
                distanceLabel.Text = "ZERO";
            }
            //finish_location = new Location(45.52989617735217, -122.67512739528975);
            else
            {
                double distance = Location.CalculateDistance(start_location, finish_location, DistanceUnits.Kilometers);
                distanceLabel.Text = (Math.Round(distance, 2)).ToString() + "km";
            }
        } */

        
    }



}