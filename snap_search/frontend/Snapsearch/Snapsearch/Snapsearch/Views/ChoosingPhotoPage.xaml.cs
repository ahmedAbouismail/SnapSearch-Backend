using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosingPhotoPage : ContentPage
    {
        public ChoosingPhotoPage()
        {
            InitializeComponent();
        }

        async void TakePhoto_OnClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                PickedImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Choose an image"
                }
            );
            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                PickedImage.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}