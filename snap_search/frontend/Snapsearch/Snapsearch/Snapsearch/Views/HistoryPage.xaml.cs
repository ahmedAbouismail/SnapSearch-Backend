using Snapsearch.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public ObservableCollection<HistoryItem> HistoryItems { get; } = new ObservableCollection<HistoryItem>();
        //readonly List<Item> items;
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

            //items = new List<Item>();

            foreach (var file in System.IO.Directory.GetFiles(rootDirectory))
            {
                HistoryItems.Add(new HistoryItem
                {
                    Path = file
                });
            }

        }


    }
}