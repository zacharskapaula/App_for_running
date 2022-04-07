using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void startButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RunningPage());
        }
    }
}