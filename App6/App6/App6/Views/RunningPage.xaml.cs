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
<<<<<<< Updated upstream
=======
            stopButton.IsVisible = true;
            startButton.IsVisible = false;
            BindingContext = new Timer();
            
>>>>>>> Stashed changes

            BindingContext = new Timer();
        }

<<<<<<< Updated upstream
=======
        private void StopButton_Clicked(object sender, EventArgs e)
        {
            finishLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceLabel.IsVisible = false;
            welcomeLabel.IsVisible = false;
            
        } 
>>>>>>> Stashed changes
    }



}
