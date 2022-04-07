using System.ComponentModel;
using App6.ViewModels;
using Xamarin.Forms;

namespace App6.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}