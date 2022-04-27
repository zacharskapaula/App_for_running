using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using App6.Models;
using Xamarin.Forms;

namespace App6.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        
        private string name;
        private int age;
        private int height;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
               // && !String.IsNullOrEmpty(age);
        }

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


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Age = Age,
                Height = Height
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
