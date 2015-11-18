using System;
using System.Collections.Generic;
using RestSharp;
using UmbracoCloudFlareManager.Enums;
using UmbracoCloudFlareManager.Extensions;
using UmbracoCloudFlareManager.Models;

namespace UmbracoCloudFlareManager.Helpers
{
    public class CloudFlareApiHelper
    {
        private static readonly RestRequest Request = new RestRequest(Method.POST);

        public Zones GetDomains()
        {
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Method, PluginConstants.CloudFlareApi.Methods.LoadZones);

            var response = CloudFlareProxy.Execute<DomainResponse>(Request);
            return response.Response.Zones;
        }

        public List<DnsObject> GetRecords(string domain)
        {
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Method, PluginConstants.CloudFlareApi.Methods.LoadRecords);
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Domain, domain);

            var response = CloudFlareProxy.Execute<DnsRecordsResponse>(Request);
            return response.Response.Recs.Objs;
        }

        public PurgeCacheResponse PurgeCache(string domain)
        {
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Method, PluginConstants.CloudFlareApi.Methods.PurgeCache);
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Domain, domain);
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.PurgeCache, 1);

            var response = CloudFlareProxy.Execute<PurgeCacheResponse>(Request);
            return response.result == ResultKind.Success.GetStringValue() ? response : null;
        }

        public SingleFilePurgeCacheResult PurgeFileCache(string domain, string url)
        {
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Method, PluginConstants.CloudFlareApi.Methods.PurgeFile);
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Domain, domain);
            Request.AddParameter(PluginConstants.CloudFlareApi.Parameters.Url, url);

            var response = CloudFlareProxy.Execute<SingleFilePurgeCacheResponse>(Request);
            return response.result == ResultKind.Success.GetStringValue() ? response.response : null;
        }

        #region Singleton

        protected static volatile CloudFlareApiHelper _Instance = new CloudFlareApiHelper();
        protected static object SyncRoot = new Object();

        protected CloudFlareApiHelper()
        {
        }

        public static CloudFlareApiHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_Instance == null)
                            _Instance = new CloudFlareApiHelper();
                    }
                }

                return _Instance;
            }
        }

        #endregion
    }
}