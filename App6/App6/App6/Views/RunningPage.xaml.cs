using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.IO;
using GoogleApi.Entities.Maps.StaticMaps.Request;
using System.Xml.Serialization;
using TcxTools;


namespace App6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {
        private readonly Trackpoint[] pointsRoute1;
        private readonly Trackpoint[] pointsRoute2;



        private Pin pinRoute1 = new Pin
        {
            Label = "Other locatin",
            Position = new Xamarin.Forms.Maps.Position(49.9751808132475, 21.986640240919986)

        };

        private Pin pinRoute2 = new Pin
        {
            Label = "Other locatin 2",
            Position = new Xamarin.Forms.Maps.Position(52.25093412599486, 21.017928115650736)
        };


        public RunningPage()
        {
            InitializeComponent();
            mylocalMap.Pins.Add(pinRoute1);
            mylocalMap.Pins.Add(pinRoute2);
            var index = 0;

            pointsRoute1 = GetTrackingRoute("route1.tcx");
            pointsRoute2 = GetTrackingRoute("route2.tcx");


            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                pinRoute1.Position = new Xamarin.Forms.Maps.Position(pointsRoute1[index].Position.LatitudeDegrees, pointsRoute1[index].Position.LongitudeDegrees);

                pinRoute2.Position = new Xamarin.Forms.Maps.Position(pointsRoute2[index].Position.LatitudeDegrees, pointsRoute2[index].Position.LongitudeDegrees);

                if (index == 0)
                {
                    mylocalMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinRoute1.Position, Xamarin.Forms.Maps.Distance.FromKilometers(1)));
                }

                index += 5;

                return pointsRoute1.Length >= index;
            });
        }

        

        private Trackpoint[] GetTrackingRoute(string filename)
        {
            using (var stream = Embedded.Load(filename))
            {
                var serializer = new XmlSerializer(typeof(TrainingCenterDatabase));
                var @object = serializer.Deserialize(stream);

                var database = (TrainingCenterDatabase)@object;
                return database.Activities.Activity[0].Lap[0].Track.ToArray();


            }
        }

    }



}