using Snapsearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public ObservableCollection<HistoryItem> HistoryItems { get; } = new ObservableCollection<HistoryItem>();


        public HistoryPage()
        {
            InitializeComponent();

            

            BindingContext = this;

            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

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

        }

        private async void CollectionHistoryItem_Clicked(object sender, EventArgs e)
        {

            



        }
    }
}