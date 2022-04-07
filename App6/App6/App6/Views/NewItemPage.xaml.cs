using System;
using System.Collections.Generic;
using System.ComponentModel;
using App6.Models;
using App6.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}