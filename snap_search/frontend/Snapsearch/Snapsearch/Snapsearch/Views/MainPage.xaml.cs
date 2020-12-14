using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        private async void PH_ResultsPageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage());
        }

    }
}