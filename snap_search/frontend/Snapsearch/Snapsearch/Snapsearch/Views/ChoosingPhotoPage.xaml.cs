using Snapsearch.Services;
using System;
using System.Net;
using System.Net.Http;
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

        /// <summary>
        ///     event handler for capturing photo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TakePhoto_OnClicked(object sender, EventArgs e)
        {
            //todo - ask "does phone have a camera?" !Important!
            // wait result of captured photo
            var result = await MediaPicker.CapturePhotoAsync();

            // if photo is captured
            if (result != null)
            {
                // read captured photo
                var stream = await result.OpenReadAsync();

                // create Data content for Http
                var content = new MultipartFormDataContent();

                // Show image on screen
                PickedImage.Source = ImageSource.FromStream(() => stream);

                // Create Image (add data to the multiformdatacontent..)
                content.Add(new StreamContent(await result.OpenReadAsync()), "input_img", result.FileName);

                // create new HttpClient
                var httpClient = new HttpClient(); // Http

                // Post request with captured photo
                // important: the url to the api is the IP-Adress of your computer and not localhost !
                var response = await httpClient.PostAsync("http://192.168.1.24:5000/uploadimage/10", content); //Http

                // get StatusCode of server. 200 - Ok!
                Console.WriteLine(response.StatusCode.ToString());

                // if status code is OK...
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // reveal use photo button
                    UsePhoto.IsVisible = true;

                    // create Json string
                    var jsonString = await response.Content.ReadAsStringAsync();

                    // deserialize Json string
                    var cbirResult = CbirResult.FromJson(jsonString);
                }

            }
        }

        /// <summary>
        ///     event handler for picking image from the gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ImageButton_OnClicked(object sender, EventArgs e)
        {

            // Open Gallery
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                // with title
                Title = "Choose an image"
            }
            );

            // if an image was picked...
            if (result != null)
            {
                // read the image data
                var stream = await result.OpenReadAsync();

                // create multipart form data content for http
                var content = new MultipartFormDataContent();

                // show image on screen
                PickedImage.Source = ImageSource.FromStream(() => stream);

                // get data ready for post request to api
                content.Add(new StreamContent(await result.OpenReadAsync()), "input_img", result.FileName);

                // create new httpclient
                var httpClient = new HttpClient(); // Http

                // send post request to api
                // important: the url to the api is the IP-Adress of your computer and not localhost !
                var response = await httpClient.PostAsync("http://192.168.1.24:5000/uploadimage/10", content); //Http

                // write status code to console (for dev)
                Console.WriteLine(response.StatusCode.ToString());

                // if status code from server is 200 OK...
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // make use photo button visible 
                    UsePhoto.IsVisible = true;

                    // create Json string
                    var jsonString = await response.Content.ReadAsStringAsync();

                    // deserialize Json string
                    var cbirResult = CbirResult.FromJson(jsonString);


                }
            }
        }


        private async void UsePhoto_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage());
        }
    }
}