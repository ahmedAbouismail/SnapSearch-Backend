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

        public bool IsImagePicked = false;
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

                var content = new MultipartFormDataContent(); // Http 

                PickedImage.Source = ImageSource.FromStream(() => stream);
                
                content.Add(new StreamContent(await result.OpenReadAsync()), "input_img", result.FileName); // Http
               
                var httpClient = new HttpClient(); // Http

                var response = await httpClient.PostAsync("http://192.168.1.24:5000/uploadimage/10", content); //Http

                Console.WriteLine(response.StatusCode.ToString()); // attach to text or label 
                
                //Http
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    UsePhoto.IsVisible = true;
                    Console.WriteLine(response.Content);
                }

                

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
                //var stream = await result.OpenReadAsync();

                //PickedImage.Source = ImageSource.FromStream(() => stream);

                //UsePhoto.IsVisible = true;

                var stream = await result.OpenReadAsync();

                var content = new MultipartFormDataContent(); // Http 

                PickedImage.Source = ImageSource.FromStream(() => stream);

                content.Add(new StreamContent(await result.OpenReadAsync()), "input_img", result.FileName); // Http

                var httpClient = new HttpClient(); // Http

                var response = await httpClient.PostAsync("http://192.168.1.24:5000/uploadimage/10", content); //Http

                Console.WriteLine(response.StatusCode.ToString()); // attach to text or label 

                //Http
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    UsePhoto.IsVisible = true;
                    Console.WriteLine(response.Content);
                }

            }
        }


        private async void UsePhoto_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultsPage());
        }
    }
}