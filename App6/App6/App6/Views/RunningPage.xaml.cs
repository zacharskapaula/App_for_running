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

using System.Runtime.CompilerServices;
using System.Threading;
using SQLite;
using Plugin.Geolocator;
using App6.Services;
using App6.Models;

namespace App6.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {
        double update_distance;
        public RunningPage()
        {
            InitializeComponent(); 

        }

        Polyline polyline = new Polyline
        {
            StrokeWidth = 8,
            StrokeColor = Color.FromHex("#15A826"),
            Geopath =
            {
              
            }

        };
        
        public void update_location(double x)
        {
            update_distance = update_distance + x;
            distanceLabel.Text = (Math.Round(update_distance, 2)).ToString() + "km";
        }

        Location start_location;
        public async void GetStartLoc()
        {
            start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            Xamarin.Forms.Maps.Position map_position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(map_position, Xamarin.Forms.Maps.Distance.FromKilometers(0.5));
            mylocalMap.MoveToRegion(mapSpan);

            Pin pin_start = new Pin
            {
                Label = "Start",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude)
            };
            mylocalMap.Pins.Add(pin_start);
            polyline.Geopath.Add(map_position);
            mylocalMap.MapElements.Add(polyline);
  
        }

        Location on_road_location;
        double distance_on_road;
        public async void OnRoadLocation()
        {
            on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            Xamarin.Forms.Maps.Position point_on_road = new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude);
            polyline.Geopath.Add(point_on_road);

            distance_on_road = Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);
            update_location(distance_on_road);

            start_location = on_road_location;
            speedLabel.Text = (on_road_location.Speed).ToString();
        }
        

        public void RoadLocationEverySeconds()
        {
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    OnRoadLocation();

                });

                if (stopButton.IsVisible == true)
                {
                    return true;
                }
                else return false;

            });
        }
    

        Location finish_location;
        public async void GetStopLoc()
        {
            finish_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            finish_location = new Location(finish_location.Latitude, finish_location.Longitude);
            Xamarin.Forms.Maps.Position finish_position =  new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude);
   
            Pin pin_stop = new Pin
            {
                Label = "Stop",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
            };
            mylocalMap.Pins.Add(pin_stop);
            polyline.Geopath.Add(finish_position);

            double distancet = Xamarin.Essentials.Location.CalculateDistance(on_road_location, finish_location, DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distancet, 2)).ToString() + "km";
   
            buttonRow.Height = 0;
            mapRow.Height = 260;

        }

        public async void DatabaseSave()
        {
            string starttime = trainingStartTime.Text;
            string hour = hourLabel.Text;
            string all_distance = distanceLabel.Text;
            //string advspeed = "10";
            Database db = Database.GetInstance();
            await db.SaveStatisticAsync(new Statistic(starttime, hour, distanceLabel.Text, speedLabel.Text));
           
        }

        public void StartButton_Clicked(object sender, EventArgs e)
        {
            
            DateTime startTime = DateTime.Now;
            string time = startTime.ToString();
            trainingStartTime.Text = time;
            welcomeLabel.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceNameLabel.IsVisible = true;
            speedNameLabel.IsVisible = true;
            timeNameLabel.IsVisible = true;
            distanceLabel.IsVisible = true;
            speedLabel.IsVisible = true;
            startButton.IsVisible = false;
            stopButton.IsVisible = true;
            GetStartLoc();
            RoadLocationEverySeconds();
            

            BindingContext = new TrainingTimer(); 
            
        }
 
        public void StopButton_Clicked(object sender, EventArgs e)
        {
            DateTime myDate = DateTime.Now;
            string thatTime = myDate.ToString();
            trainingStopTime.Text = thatTime;
            //finishLabel.IsVisible = true;
            distanceLabel.IsVisible = true;
            distanceNameLabel.IsVisible = true;
            speedLabel.IsVisible = true;
            speedNameLabel.IsVisible = true;
            timerLabel.IsVisible = false;
            hourLabel.IsVisible = true;
            timeNameLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            welcomeLabel.IsVisible = false;
            TotalTime();
            GetStopLoc();
            DatabaseSave();


        } 

        public  void TotalTime()
        {
            DateTime arg1 = DateTime.Parse(trainingStopTime.Text);
            DateTime arg2 = DateTime.Parse(trainingStartTime.Text);
            hourLabel.Text = (arg1 - arg2).ToString();
        }
  
    }



}