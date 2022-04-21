using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App6.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunningPage : ContentPage
    {


        public RunningPage()
        {
            InitializeComponent();

        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            welcomeLabel.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceLabel.IsVisible = true;
            stopButton.IsVisible = true;
            startButton.IsVisible = false;
            BindingContext = new TimerModel();
            BindingContext = new Distance();

        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            finishLabel.IsVisible = true;
            stopButton.IsVisible = false;
            startButton.IsVisible = false;
            timerLabel.IsVisible = true;
            distanceLabel.IsVisible = false;
            welcomeLabel.IsVisible = false;
            
        }
    }



}
