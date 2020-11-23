using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Snapsearch
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartSearchingButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ChoosingPhotoPage()));
        }

        private async void PH_ResultsPageButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new ResultsPage()));
        }
    }
}
