using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Snapsearch.Models;
using Snapsearch.ViewModels;

namespace Snapsearch.Services
{
    /// <summary>
    /// handles httpClient (restClient) and post request through the Api.
    /// restSharp Nuget package
    /// </summary>
    public class CbirApiServices
    {
        // private CbirClient Instance
        private static CbirApiServices _cbirClientInstance;

        // private restClient
        private RestClient restClient;

        // public Status Code from the server. 
        public HttpStatusCode CbirApiServicesStatusCode;

        // initialize CbirApiServices Client Instance 
        public static CbirApiServices CbirClientInstance
        {
            get
            {
                // if there is no restClient...
                if (_cbirClientInstance == null)
                {
                    // create new restClient
                    _cbirClientInstance = new CbirApiServices();
                }

                // store connection privately
                return _cbirClientInstance;
            }
        }

        // service for using the api and settings
        public CbirApiServices()
        {
            // set endpoint for the restClient
            // todo - change 11 to int variable for settings page
            restClient = new RestClient("http://137.117.141.110/uploadimage/11");

            // don't time out connection
            restClient.Timeout = -1;

            // add headers to the request
            restClient.AddDefaultHeader("Accept", "application/json");

        }

        /// <summary>
        /// Manage Post Request and return a Dictionary of CbirApiResponseModels
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <returns>Dictionary of CbirApiResponseModels</returns>
        public async Task<IDictionary<string, CbirApiResponseModel>> PostRequestCbirResponseDictionaryAsync(
            string fullFilePath)
        {
            try
            {
                // create new Rest POST request
                var request = new RestRequest(Method.POST);

                // add file with key = input_img as form data
                request.AddFile("input_img", fullFilePath);

                // await reponse from post request
                var response =  await restClient.ExecuteAsync(request);

                // write status code to console for dev
                Console.WriteLine(response.StatusCode);

                // write status code of response to var
                CbirApiServicesStatusCode = response.StatusCode;

                // create new json string from response
                var jsonString = response.Content;

                // convert json string to c# code using CbirApiResponseModel
                var cbirResult = CbirApiResponseModel.FromJson(jsonString);

                // return dictionary of CbirApiResponseModels
                return cbirResult;

            }
            catch (Exception e)
            {
                // catch any exception and write to console for dev
                Console.WriteLine(e);
                return null;
            }

        }
    }
}
