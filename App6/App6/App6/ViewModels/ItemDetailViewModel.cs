using System;
using System.Diagnostics;
using System.Threading.Tasks;
using App6.Models;
using Xamarin.Forms;

namespace App6.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string name;
        private int age;
        private int height;

        public string Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public int Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        public int Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Name = item.Name;
                Age = item.Age;
                Height = item.Height;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
