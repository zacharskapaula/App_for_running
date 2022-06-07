using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App6.ViewModels;
using App6.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Views
{
    public partial class StatisticPage : ContentPage
    {
        //TrainingView.ItemSource = training;


        public StatisticPage()
        {
            InitializeComponent();
  
        }

        

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Database db = Database.GetInstance();
            //Database db1 = Database.GetInstance();
            TrainingView.ItemsSource = await db.GetDistanceAsync();
            //await DisplayAlert("Uwaga", "Dystans", "ok");
            //TrainingView.ItemsSource = await db.GetTimeAsync();

            //binding dla kilku rzeczy na raz zeby wyswietlalo sie w jednym wierszu
        }
    }
}