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
using System.Threading;

namespace App6.Views
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {
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
        //mylocalMap.MapElements.Add(polyline);


        Location start_location;
        public async void GetStartLoc()
        {
            start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            Xamarin.Forms.Maps.Position map_position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(map_position, Xamarin.Forms.Maps.Distance.FromKilometers(0.5));
            mylocalMap.MoveToRegion(mapSpan);

            distanceLabel.Text = start_location.ToString();

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
        public async void OnRoadLocation()
        {
            on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            speedLabel.Text = on_road_location.ToString();
            Xamarin.Forms.Maps.Position point_on_road = new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude);
            polyline.Geopath.Add(point_on_road);
        }

        bool shouldRun = false;
        CancellationTokenSource cancellation;
        public void RoadLocationEverySeconds()
        {
            
            CancellationTokenSource cts = this.cancellation = new CancellationTokenSource();
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                if (this.cancellation != null)
                    Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    OnRoadLocation();
                    
                });
                return shouldRun = true;
            });
        }
 
        Location finish_location;
        public async void GetStopLoc()
        {
            finish_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            finish_location = new Location(finish_location.Latitude, finish_location.Longitude);
            Xamarin.Forms.Maps.Position finish_position =  new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude);

            distanceLabel.Text = finish_location.ToString();

            //double distance = Location.CalculateDistance(start_location, finish_location, DistanceUnits.Kilometers);
            //distanceLabel.Text = (Math.Round(distance, 2)).ToString() + "km";
            Pin pin_stop = new Pin
            {
                Label = "Stop",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
            };
            mylocalMap.Pins.Add(pin_stop);
            polyline.Geopath.Add(finish_position);

            if (this.cancellation != null)
                Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
            shouldRun = false;
        }

        public async void CountDistance()
        {
            try
            {
                if (on_road_location != null)
                {

                    on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
                    new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude);

                    Location OnTrip = new Location(on_road_location.Latitude, on_road_location.Longitude);

                    double distance = Xamarin.Essentials.Location.CalculateDistance(start_location, OnTrip, DistanceUnits.Kilometers);
                    double distancef = Xamarin.Essentials.Location.CalculateDistance(OnTrip, finish_location, DistanceUnits.Kilometers);
                    double t = distancef + distance ;
                    distanceLabel.Text = (Math.Round(t, 2)).ToString() + "km";
                    
                }
            }
            catch (Exception ex)
            {
                // Unable to get location

                await DisplayAlert(ex.Source, "ERROR: " + ex.Message, "OK");

            }
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

            BindingContext = new TimerModel(); 
            
        }
 
        public void StopButton_Clicked(object sender, EventArgs e)
        {
            DateTime myDate = DateTime.Now;
            string thatTime = myDate.ToString();
            trainingStopTime.Text = thatTime;
            finishLabel.IsVisible = true;
            //trainingStartTime.IsVisible = true;
            //trainingStopTime.IsVisible = true;
            hourLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            timerLabel.IsVisible = false;
            distanceLabel.IsVisible = true;
            speedLabel.IsVisible = false;
            welcomeLabel.IsVisible = false;
            TotalTime();
            GetStopLoc();
            CountDistance();
            //Xamarin.Forms.Device.StartTimer() = false;
        } 

        public void TotalTime()
        {
            DateTime arg1 = DateTime.Parse(trainingStopTime.Text);
            DateTime arg2 = DateTime.Parse(trainingStartTime.Text);
            hourLabel.Text = (arg1 - arg2).ToString();
        }
  
    }



}