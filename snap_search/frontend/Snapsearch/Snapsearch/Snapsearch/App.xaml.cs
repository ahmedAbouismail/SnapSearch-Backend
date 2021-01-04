using Snapsearch.Services;
using Snapsearch.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace Snapsearch
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
