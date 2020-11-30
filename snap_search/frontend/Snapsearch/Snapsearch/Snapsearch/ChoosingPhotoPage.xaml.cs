using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Snapsearch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosingPhotoPage : ContentPage
    {
        private MediaFile _mediaFile;
        public ChoosingPhotoPage()
        {
            InitializeComponent();
            
        }

        private MediaElement _mediaElement;
        /// <summary>
        /// Event Handler for choosing a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChoosePhoto_Clicked(object sender, EventArgs e)
        {
            //initialize the CrossMedia plugin
            await CrossMedia.Current.Initialize();

            //if device is not supported...
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //inform user with message
                await DisplayAlert("Not Supported", "Your Device does not support this feature.", "Ok");
                return;
            }

            //wait for the user to pick a photo
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            //if no photo is picked return back to App
            if (_mediaFile == null)
                return;
            //set text to path of the media file 
    

            //get preview of picked image 
            FileImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

        }

        /// <summary>
        /// Event Handler for taking a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UploadPhoto_Clicked(object sender, EventArgs e)
        {
            
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = " ";

            var httpResponsMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

        

        }

        private async void TakePhoto_OnClicked_(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("No Camera", "App can not detect the camera", "Ok");
                return;
            }

            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "myImage.jpg"
            });

            if (_mediaFile == null)
                return;

            await DisplayAlert("File Location", _mediaFile.Path, "OK");

            FileImage.Source = ImageSource.FromStream(() =>
                _mediaFile.GetStream());
        }
    }
}
