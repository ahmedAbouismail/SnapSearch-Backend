using System;
using System.Net.Http;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace TakeAndUploadPhoto
{
    public partial class MainPage
    {

        private MediaFile _mediaFile;
        public MainPage()
        {
            InitializeComponent();
        }
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
            LocalPathLabel.Text = _mediaFile.Path;

            //get preview of picked image 
            FileImage.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

        }

        /// <summary>
        /// Event Handler for taking a photo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            //if there is no camera or pick photo isn't supported...
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
            {
                //display error message to user
                await DisplayAlert("No Camera", "Application can not find the camera", "Ok");
                return;
            }

            //store photo in the directory called sample and name it myImage
            _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
            {
                Directory = "Sample",
                Name = "myImage.jpg"
            });

            // if there is no media return
            if (_mediaFile == null)
                return;
            //show path of media file on the app
            LocalPathLabel.Text = _mediaFile.Path;

            //show preview of image on app
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
        }


        private async void UploadPhoto_Clicked(object sender, EventArgs e)
        {
            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
            "\"file\"", 
            $"\"{_mediaFile.Path}\"");

            var httpClient = new HttpClient();

            var uploadServiceBaseAddress = " ";

            var httpResponsMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

            RemotePathLabel.Text = await httpResponsMessage.Content.ReadAsStringAsync();

        }
    }
}
