using System;
using App6.Services;
using App6.Views;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;

namespace App6
{
    public partial class App : Application
    {
        private static Database database;
        private static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydb.db"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
