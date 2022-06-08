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

        public StatisticPage()
        {
            InitializeComponent();
  
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Database db = Database.GetInstance();
           
            TrainingView.ItemsSource = await db.GetStatisticAsync();
            
        }
    }
}