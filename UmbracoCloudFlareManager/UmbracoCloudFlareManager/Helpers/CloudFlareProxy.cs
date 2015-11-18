using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;
using UmbracoCloudFlareManager.Enums;
using UmbracoCloudFlareManager.Extensions;
using UmbracoCloudFlareManager.Models;

namespace UmbracoCloudFlareManager.Helpers
{
    public static class CloudFlareProxy
    {
        private static readonly Uri BaseUrl = new Uri("https://www.cloudflare.com/api_json.html");

        public static T Execute<T>(RestRequest request) where T : new()
        {
            var config = Config.GetSettings();
            var client = new RestClient
            {
                BaseUrl = BaseUrl,
                Authenticator = new SimpleAuthenticator(PluginConstants.CloudFlareApi.Parameters.ApiKey, config.CloudFlareApiKey, PluginConstants.CloudFlareApi.Parameters.Email, config.CloudFlareEmail)
            };
            var response = client.Execute<T>(request);

            HandleRequestError(response);
            HandleResponseError(response.Data as ResponseBase);

            return response.Data;
        }

        private static void HandleRequestError(IRestResponse response)
        {
            bool success = true;
            string message = "";
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                message = String.Format("Network error. Response status: {0}. Error: {1}", response.ResponseStatus.ToString(), response.ErrorMessage);
                success = false;

            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                message = String.Format("Server error. HTTP Status code: {0}. Error: {1}", response.StatusCode.ToString(), response.Content);
                success = false;
            }
            if (response.ErrorException != null)
            {
                message = "Error retrieving response. Check inner details for more info.";
                success = false;
            }
            if (success) return;

            var proxyException = new ApplicationException(message, response.ErrorException);
            throw proxyException;
        }

        private static void HandleResponseError(ResponseBase response)
        {
            if (response.result == ResultKind.Success.GetStringValue()) return;

            var proxyException = new ApplicationException(response.msg);
            throw proxyException;
        }
    }
}