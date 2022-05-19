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

        public async void OnRoadLocation()
        {
            Location on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(2)));
            string on_road_location_coordinate = $"Lat: {on_road_location.Latitude}, Lng: {on_road_location.Longitude}";

        }

        public async void GetLoc()
        {
            Location start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(2)));
            string start_location_coordinate = $"Lat: {start_location.Latitude}, Lng: {start_location.Longitude}";
            var finish_location = new Location(37.414611524314694, -122.077323759498);
            double distance = Location.CalculateDistance(start_location, finish_location, DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distance, 2)).ToString() + "km";
            var speed = distance / 10;
            speedLabel.Text = (Math.Round(speed, 2)).ToString() + "km/h";
            Polyline polyline = new Polyline
            {
                StrokeWidth = 8,
                StrokeColor = Color.FromHex("#15A826"),
                //FillColor = Color.FromHex("#881BA1E2"),
                Geopath = {
                    new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude),
                    new Xamarin.Forms.Maps.Position(37.4209742851841, -122.0836660168546),

                    new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
                }
            };

            Pin pin_start = new Pin
            {
                Label = "Start",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude)

            };
            Pin pin_stop = new Pin
            {
                Label = "Stop",
                Type = PinType.Place,
                Position = new Xamarin.Forms.Maps.Position(finish_location.Latitude, finish_location.Longitude)
            };
            mylocalMap.MapElements.Add(polyline);
            mylocalMap.Pins.Add(pin_start);
            mylocalMap.Pins.Add(pin_stop);
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
            GetLoc();
            
            BindingContext = new TimerModel(); 
            
        }
 
        public void StopButton_Clicked(object sender, EventArgs e)
        {
            DateTime myDate = DateTime.Now;
            string thatTime = myDate.ToString();
            trainingStopTime.Text = thatTime;
            finishLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceLabel.IsVisible = false;
            welcomeLabel.IsVisible = false;
            TotalTime();


        }
        public void TotalTime()
        {
            DateTime arg1 = DateTime.Parse(trainingStopTime.Text);
            DateTime arg2 = DateTime.Parse(trainingStartTime.Text);
            hourLabel.Text = (arg1 - arg2).ToString();
            //mozewyjdzie.ToString() = hourLabel.Text;
            /*DateTime startTime = DateTime.Now;
            string time = startTime.ToString();
            //DateTime hour = thatTime - startTime
            DateTime myDate = DateTime.Now;
            string thatTime = myDate.ToString();
            string hour = (myDate - startTime).ToString();
            hourLabel.Text = hour; */
        }


    }



}