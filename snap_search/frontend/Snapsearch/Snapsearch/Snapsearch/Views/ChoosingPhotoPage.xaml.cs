using Snapsearch.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RestSharp;
using Snapsearch.Models;
using Snapsearch.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Snapsearch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChoosingPhotoPage : ContentPage
    {
        public ChoosingPhotoPage()
        {
            InitializeComponent();

            

        }

        // private dictionary for results of Post request
        private IDictionary<string, CbirApiResponseModel> _postTaskResult = new Dictionary<string, CbirApiResponseModel>();

        // private string for saving the picked image
        private string _photoPath;
        public static string PhotoPath;

        /// <summary>
        ///     event handler for capturing photo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TakePhoto_OnClicked(object sender, EventArgs e)
        {

            // wait result of captured photo
            var result = await MediaPicker.CapturePhotoAsync();

            // if photo is captured
            if (result != null)
            {
                try
                {
                    // read captured photo
                    var stream = await result.OpenReadAsync();
                    
                    

                    //LogoImageSvgGrid.IsVisible = false;
                    GenericImageSvgGrid.IsVisible = false;
                    HintLabel.IsVisible = false;

                    // show image on screen
                    //PickedImage.Source = ImageSource.FromStream(() => stream);
                    PickedImageFrame.Content = new Image
                    {
                        Source = ImageSource.FromStream(() => stream),
                        Aspect = Aspect.AspectFill,

                    };

                    PickedImageFrame.IsVisible = true;
                    PickedImageHintLabel.IsVisible = true;
                    ContinueHintLabel.IsVisible = true;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("No Camera", ":( No camera available." + ex.Message, "OK");
                }
                // save captured photo
                await LoadPhotoAsync(result);

                // send Image source to ResultsPageViewModel
                ResultsPageViewModel.PhotoPath = _photoPath;

                // if there is no internet access...
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    // display alert message
                    await DisplayAlert("No Internet", "Check your connection", "OK");
                    await Navigation.PopToRootAsync();
                }
                // else...
                else
                {
                    PostRequestProgressBar.IsVisible = true;
                    await PostRequestProgressBar.ProgressTo(0.10, 100, Easing.Linear);

                    // boot up CbirApiServices
                    var content = new CbirApiServices();

                    await PostRequestProgressBar.ProgressTo(0.20, 500, Easing.Linear);

                    // CbirApiServices and get a dictionary from the result, save it to private variable
                    _postTaskResult = await content.PostRequestCbirResponseDictionaryAsync(result.FullPath); //async

                    await PostRequestProgressBar.ProgressTo(0.90, 1000, Easing.Linear);

                    // if Post Request Status Code = Ok...
                    if (content.CbirApiServicesStatusCode == HttpStatusCode.OK)
                    {
                        // make Use Photo Button visible
                        UsePhoto.IsVisible = true;
                        UsePhotoButtonSvgGrid.IsVisible = true;

                        PostRequestProgressBar.IsVisible = false;
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
    

    /// <summary>
        ///     event handler for picking image from the gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            

            // Open Gallery
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                // with title
                Title = "Choose an image"
            });

            // if an image was picked...
            if (result != null)
            {
                //LogoImageSvgGrid.IsVisible = false;
                HintLabel.IsVisible = false;

                // read the image data
                var stream = await result.OpenReadAsync();

                // show image on screen
                //PickedImage.Source = ImageSource.FromStream(() => stream);
                PickedImageFrame.Content = new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    Aspect = Aspect.AspectFill,

                };

                PickedImageFrame.IsVisible = true;
                PickedImageHintLabel.IsVisible = true;
                ContinueHintLabel.IsVisible = true;

                // save image
                await LoadPhotoAsync(result);

                
                GenericImageSvgGrid.IsVisible = false;
                HintLabel.IsVisible = false;

                // pass image to ResultsPageViewModel
                ResultsPageViewModel.PhotoPath = _photoPath;

                // if there is no internet access...
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    // display alert message
                    await DisplayAlert("No Internet", "Check your connection", "OK");
                    return;
                }
                // else...
                else
                {
                    // boot up CbirApiServices
                    var content = new CbirApiServices();

                    PostRequestProgressBar.IsVisible = true;

                    await PostRequestProgressBar.ProgressTo(0.10, 500, Easing.Linear);

                    // CbirApiServices and get a dictionary from the result, save it to private variable
                    _postTaskResult = await content.PostRequestCbirResponseDictionaryAsync(result.FullPath); //async

                    await PostRequestProgressBar.ProgressTo(0.90, 1000, Easing.Linear);

                    // if Post Request Status Code = Ok...
                    if (content.CbirApiServicesStatusCode == HttpStatusCode.OK)
                    {
                        await PostRequestProgressBar.ProgressTo(1, 1, Easing.Linear);

                        // make Use Photo Button visible
                        UsePhoto.IsVisible = true;
                        UsePhotoButtonSvgGrid.IsVisible = true;

                        PostRequestProgressBar.IsVisible = false;
                    }
                    // else display error
                    else
                    {
                        await DisplayAlert("Connection Error", "Server seems to be offline... \nPlease try again later.", "OK");
                        await Navigation.PopToRootAsync();
                    }
                }

                #region code that is currently not used


                // create multipart form data content for http
                //var content = new MultipartFormDataContent();


                //// ResultsPageViewModel.ResultImage.Source = ImageSource.FromStream(() => stream);

                //// get data ready for post request to api
                //content.Add(new StreamContent(await result.OpenReadAsync()), "input_img", result.FileName);

                //// create new httpclient
                //var httpClient = new HttpClient(); // Http

                //// send post request to api
                //var response = await httpClient.PostAsync("http://snapsearch.westeurope.cloudapp.azure.com:5000/uploadimage/10", content);

                //// write status code to console (for dev)
                //Console.WriteLine(response.StatusCode.ToString());

                //// if status code from server is 200 OK...
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    // make use photo button visible 
                //    UsePhoto.IsVisible = true;

                //    // create Json string
                //    var jsonString = await response.Content.ReadAsStringAsync();


                //    // deserialize Json string
                //    var cbirResult = CbirApiResponseModel.FromJson(jsonString);

                //    _postTaskResult = cbirResult;

                //    // for each key, value pair of cbirResult...
                //    //foreach (var kvpCbir in cbirResult.Values)
                //    {
                //        // add its "link" value to CbirLinksList in the Results Page View Model
                //         //ResultsPageViewModel.CbirLinksList.Add(kvpCbir.Link.ToString());
                //    };
                //}

                #endregion
            }
        }

        // When Use Photo Button is clicked...
        private async void UsePhoto_OnClicked(object sender, EventArgs e)
        {
            ResultsPageViewModel.CbirLinksList.Clear();

            foreach (var cbirLinks in _postTaskResult.Values)
            {
                ResultsPageViewModel.CbirLinksList.Add(cbirLinks.Link.ToString());
            }

            // switch to next Results Page
            await Navigation.PushAsync(new ResultsPage());

        }

        // saving image to the phone
        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if(photo == null) { return; }

            //save file into local storage
            var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            _photoPath = newFile;

            MockDataStore.HistoryItemList.Add(_photoPath);
        }   

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            PickedImageFrame.IsVisible = false;
            PickedImageHintLabel.IsVisible = false;
            ContinueHintLabel.IsVisible = false;

            UsePhoto.IsVisible = false;
            UsePhotoButtonSvgGrid.IsVisible = false;

            PostRequestProgressBar.IsVisible = false;

            Connectivity.ConnectivityChanged += Connectivity_ChangedEvent;

            LogoImageSvgGrid.IsVisible = true;
            GenericImageSvgGrid.IsVisible = true;
            HintLabel.IsVisible = true;

        }

        private void Connectivity_ChangedEvent(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                NoConnectionLabel.FadeTo(0).ContinueWith((result) => { });
            }
            else
            {
                NoConnectionLabel.FadeTo(1).ContinueWith((result) => { });
            }
        }

        protected async override void  OnDisappearing()
        {
            base.OnDisappearing();

            Connectivity.ConnectivityChanged -= Connectivity_ChangedEvent;
            PickedImageFrame.IsVisible = false;
            PickedImage.IsVisible = false;
        }
    }
}