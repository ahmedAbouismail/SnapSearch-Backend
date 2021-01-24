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



        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);


            LogoSvgGrid.TranslationX = (width - 231) / 2;
            SnapsearchImageSvgGrid.TranslationX = (width - 318) / 2;

            GradientFrame.HeightRequest = height;
            GradientFrame.WidthRequest = width;

            LittleFrame.HeightRequest = 300;
        }

        private async void StartSearchingButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChoosingPhotoPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }



    }
}