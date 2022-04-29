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


namespace App6.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {


        public RunningPage()
        {
            InitializeComponent();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            welcomeLabel.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceLabel.IsVisible = true;

            BindingContext = new TimerModel();
        }

    }



}
