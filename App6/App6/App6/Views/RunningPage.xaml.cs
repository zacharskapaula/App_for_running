﻿using System;
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

        

        Location start_location;
        public async void GetStartLoc()
        {
            start_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            Xamarin.Forms.Maps.Position map_position = new Xamarin.Forms.Maps.Position(start_location.Latitude, start_location.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(map_position, Xamarin.Forms.Maps.Distance.FromKilometers(0.5));
            mylocalMap.MoveToRegion(mapSpan);

            //distanceLabel.Text = start_location.ToString();

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
        Location on_road_location1;
        double distance_on_road;

        public async void OnRoadLocation()
        {
            on_road_location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));
            // = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(1)));

            Xamarin.Forms.Maps.Position point_on_road = new Xamarin.Forms.Maps.Position(on_road_location.Latitude, on_road_location.Longitude);
            polyline.Geopath.Add(point_on_road);
            
            distance_on_road += Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);
            //distance_on_road -= Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distance_on_road, 2)).ToString() + "km";
            //var speed = on_road_location.Speed * 3.6;
            //var speed = on_road_location.Speed;
            //speedLabel.Text = speed.ToString() + "km/h";
            //on_road_location.CalculateDistance
        }
        public async void OnRoad22222222Location()
        {
           
            distance_on_road = Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);
            
            //distance_on_road -= Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distance_on_road, 2)).ToString() + "km";
            //var speed = on_road_location.Speed * 3.6;
            //var speed = on_road_location.Speed;
            //speedLabel.Text = speed.ToString() + "km/h";
            //on_road_location.CalculateDistance
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
        /*public async void asdfg()
        {
            distance_on_road += Location.CalculateDistance(start_location, on_road_location, DistanceUnits.Kilometers);

            distanceLabel.Text = (Math.Round(distance_on_road, 2)).ToString() + "km";
        }*/
        
        public void CalculateDistance()
        {   

            //distance_on_road += Location.CalculateDistance(, ,DistanceUnits.Kilometers);
            distanceLabel.Text = (Math.Round(distance_on_road, 2)).ToString() + "km";

        }//, point_on_road, DistanceUnits.Kilometers


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

            //listView.ItemsSource = await db.GetTrainingAsync();
            //Database db = Database.GetInstance();
            //await db.SaveTimeAsync(new TimesT(trainingStartTime.Text, trainingStopTime.Text, hourLabel.Text));
            double start_point_lat = start_location.Latitude;
            double start_point_long = start_location.Longitude;
            double stop_point_lat = finish_location.Latitude;
            double stop_point_long = finish_location.Longitude;
            //double all_distance = 2;

            //Database db = Database.GetInstance();
            //await db.SaveDistanceAsync(new Models.DistanceT(start_point_lat, start_point_long, stop_point_lat, stop_point_long, all_distance));

            // dystans calosciowy wyswietlajacy sie w koncowym labelu
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

            BindingContext = new TimerModel(); 
            
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
            
          
        } 

        public  void TotalTime()
        {
            DateTime arg1 = DateTime.Parse(trainingStopTime.Text);
            DateTime arg2 = DateTime.Parse(trainingStartTime.Text);
            hourLabel.Text = (arg1 - arg2).ToString();

            
            // listView.ItemsSource = await db.GetTrainingAsync();

        }
  
    }



}