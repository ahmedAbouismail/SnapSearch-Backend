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
        


        /// <summary>
        ///     event handler for capturing photo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TakePhoto_OnClicked(object sender, EventArgs e)
        {
            //todo - ask "does phone have a camera?" !Important!
            //todo - check connectivity
            // wait result of captured photo
            var result = await MediaPicker.CapturePhotoAsync();

            // if photo is captured
            if (result != null)
            {
                // read captured photo
                var stream = await result.OpenReadAsync().ConfigureAwait(false);

                // save captured photo
                await LoadPhotoAsync(result);

                // send Image source to ResultsPageViewModel
                ResultsPageViewModel.PhotoPath = _photoPath;

                // show image on screen
                PickedImage.Source = ImageSource.FromStream(() => stream);

                // boot up CbirApiServices
                var content = new CbirApiServices();

                // run task of...
                await Task.Run(async () =>
                {

                    // CbirApiServices and get a dictionary from the result, save it to private variable
                    _postTaskResult = await content.PostRequestCbirResponseDictionary(result.FullPath);

                });

                // if Post Request Status Code = Ok...
                if ( content.CbirApiServicesStatusCode == HttpStatusCode.OK)
                {
                    // make Use Photo Button visible
                    UsePhoto.IsVisible = true;
                }

                #region code that is currently not used




                //  ResultsPageViewModel.ResultImage.Source = ImageSource.FromStream(() => stream);

                // Create Image (add data to the multiformdatacontent..)
                //content.Add(new StreamContent(stream), "input_img", result.FileName);

                // create new HttpClient
                //var httpClient = new HttpClient();



                //httpClient.Timeout = TimeSpan.FromMinutes(10);

                // Post request with captured photo
                //var response =
                //  await httpClient.PostAsync(
                //    "http://snapsearch.westeurope.cloudapp.azure.com:5000/uploadimage/5/", content);


                //response.Content.Headers.Add(@"Content-Length", respon);
                // response.Headers.Add("Request-Timeout", "10000");
                // var client = new RestClient("http://snapsearch.westeurope.cloudapp.azure.com:5000/uploadimage/11");

                //client.Timeout = -1;
                // add default header to the Rest client
                //client.AddDefaultHeader( "Accept","application/json" );
                //var request = new RestRequest(Method.POST);

                //request.AddFile("input_img", result.FullPath);

                //todo - fix following line of code
                // if await ExecuteAsync => app crashes
                //IRestResponse response = client.Execute(request);

                //var response = await client.ExecuteAsync(request);
                // write to dev StatusCode of server. 200 - Ok!
                //Console.WriteLine(response.StatusCode.ToString());

                //// if status code is OK...
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                // reveal use photo button
                // UsePhoto.IsVisible = true;

                // create Json string
                //var jsonString = await response.Content.ReadAsStringAsync();

                //var jsonString = response.Content;


                // deserialize Json string
                //var cbirResult = CbirApiResultModel.FromJson(jsonString);

                // for each key, value pair of cbirResult...
                //foreach (var kvpCbir in cbirResult.Values)
                //{
                // add its "link" value to CbirLinksList in the Results Page View Model
                //ResultsPageViewModel.CbirLinksList.Add(kvpCbir.Link.ToString());
                //}}

                #endregion
            }
        }
    

    /// <summary>
        ///     event handler for picking image from the gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ImageButton_OnClicked(object sender, EventArgs e)
        {
            //todo - check connectivity

            // Open Gallery
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                // with title
                Title = "Choose an image"
            });

            // if an image was picked...
            if (result != null)
            {
                // read the image data
                var stream = await result.OpenReadAsync().ConfigureAwait(false);

                // show image on screen
                PickedImage.Source = ImageSource.FromStream(() => stream);

                // save image
                await LoadPhotoAsync(result);

                // pass image to ResultsPageViewModel
                ResultsPageViewModel.PhotoPath = _photoPath;

                // boot up CbirApiServices
                var content = new CbirApiServices();

                // run task of...
                await Task.Run(async () =>
                {
                    // CbirApiServices and get a dictionary from the result, save it to private variable
                    _postTaskResult = await content.PostRequestCbirResponseDictionary(result.FullPath);

                });

                // if Post Request Status Code = Ok...
                if (content.CbirApiServicesStatusCode == HttpStatusCode.OK)
                {
                    // make Use Photo Button visible
                    UsePhoto.IsVisible = true;
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
            //int counter = 0;
            foreach (var cbirLinks in _postTaskResult.Values)
            {
                
                    //while(counter <= 9)
                    //{
                    //    ResultsPageViewModel.CbirLinksList.Insert(counter,cbirLinks.Link.ToString());
                    //    counter++;
                    //    break;
                    //}

                    ResultsPageViewModel.CbirLinksList.Add(cbirLinks.Link.ToString());
            }

            // switch to next Results Page
            await Navigation.PushAsync(new ResultsPage());

        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if(photo == null) { return; }

            //save file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            _photoPath = newFile;

        }
    }
}