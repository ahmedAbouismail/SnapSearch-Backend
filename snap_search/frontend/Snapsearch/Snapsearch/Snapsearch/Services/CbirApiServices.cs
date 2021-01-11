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
    public class CbirApiServices
    {
        private static CbirApiServices _cbirClientInstance;

        public static CbirApiServices CbirClientInstance
        {
            get
            {
                if (_cbirClientInstance == null)
                {
                    _cbirClientInstance = new CbirApiServices();
                }

                return _cbirClientInstance;
            }
        }

        private RestClient restClient;

        public CbirApiServices()
        {
            restClient = new RestClient("http://snapsearch.westeurope.cloudapp.azure.com:5000/uploadimage/11");

            restClient.Timeout = -1;

            restClient.AddDefaultHeader("Accept", "application/json");

        }

        public HttpStatusCode CbirApiServicesStatusCode;


        public async Task<IDictionary<string, CbirApiResponseModel>> PostRequestCbirResponseDictionary(
            string fullFilePath)
        {
            try
            {
                var request = new RestRequest(Method.POST);

                request.AddFile("input_img", fullFilePath);

                var response =  restClient.Execute(request);

                Console.WriteLine(response.StatusCode);

                CbirApiServicesStatusCode = response.StatusCode;

                var jsonString = response.Content;

                var cbirResult = CbirApiResponseModel.FromJson(jsonString);

                return await Task.FromResult(cbirResult).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            
        }
    }
}
