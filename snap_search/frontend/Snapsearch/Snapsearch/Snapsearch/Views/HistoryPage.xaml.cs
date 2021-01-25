using Snapsearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using Snapsearch.ViewModels;
using Snapsearch.Services;
using System.Net;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private IDictionary<string, CbirApiResponseModel> _postTaskResult;

        public ObservableCollection<HistoryItem> HistoryItems { get; set; } = new ObservableCollection<HistoryItem>();

        


        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = this;
     
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            PostRequestProgressBar.IsVisible = false; 

            HistoryItems.Clear();

            var rootDirectory = FileSystem.AppDataDirectory;

            foreach (var file in System.IO.Directory.GetFiles(rootDirectory))
            {
                var dateCreated = System.IO.Directory.GetCreationTime(file);
                
                HistoryItems.Add(new HistoryItem
                {
                    Path = file,
                    DateCreated = dateCreated.ToString()
                }) ;
            }

            HistoryItems = new ObservableCollection<HistoryItem>(HistoryItems.Reverse());

        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                string previous = (e.PreviousSelection.FirstOrDefault() as HistoryItem)?.Path;
                string current = (e.CurrentSelection.FirstOrDefault() as HistoryItem)?.Path;

            ResultsPageViewModel.PhotoPath = current;

            var content = new CbirApiServices();

            PostRequestProgressBar.IsVisible = true;

            await PostRequestProgressBar.ProgressTo(0.10, 500, Easing.Linear);

            _postTaskResult = await content.PostRequestCbirResponseDictionaryAsync(current);

            await PostRequestProgressBar.ProgressTo(0.90, 1000, Easing.Linear);

            // if Post Request Status Code = Ok...
            if (content.CbirApiServicesStatusCode == HttpStatusCode.OK)
            {
                await PostRequestProgressBar.ProgressTo(1, 1, Easing.Linear);

                ResultsPageViewModel.CbirLinksList.Clear();

                foreach (var cbirLinks in _postTaskResult.Values)
                {
                    ResultsPageViewModel.CbirLinksList.Add(cbirLinks.Link.ToString());
                }

                // switch to next Results Page
                await Navigation.PushAsync(new ResultsPage());
            }
            // else display error
            else
            {
                await DisplayAlert("Connection Error", "Server seems to be offline... \nPlease try again later.", "OK");
                await Navigation.PopToRootAsync();
            }

        }
    }
}