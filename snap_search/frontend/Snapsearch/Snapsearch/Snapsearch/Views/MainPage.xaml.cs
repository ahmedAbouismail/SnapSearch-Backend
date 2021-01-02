using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Forms.Xaml;
using MasterDetailPage = Xamarin.Forms.PlatformConfiguration.iOSSpecific.MasterDetailPage;

namespace Snapsearch.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

          
        }

        private async void StartSearchingButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosingPhotoPage());
        }



    }
}