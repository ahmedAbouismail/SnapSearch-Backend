using Snapsearch.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Snapsearch.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}