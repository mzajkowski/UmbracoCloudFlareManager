using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using UmbracoCloudFlareManager.Helpers;
using UmbracoCloudFlareManager.Models;
using System.Linq;

namespace UmbracoCloudFlareManager.Controllers
{
    [PluginController("UmbracoCloudFlareManager")]
    public class DashboardController : UmbracoAuthorizedJsonController
    {
        public UcfmConfigSettings GetSettings()
        {
            return Config.GetSettings();
        }

        [HttpPost]
        public void SaveSettings(UcfmConfigSettings settings)
        {
            Config.SaveSettings(settings);
        }

        public KeyValuePair<bool, string> GetConnection()
        {
            //// Legacy code - helps only when testing / developing using proper domains configured on CloudFlare
            //var currentDomain = Request.RequestUri.Host.ToLower();

            var currentDomain = Config.GetSettingValue("Domain");
            if (currentDomain == null || string.IsNullOrWhiteSpace(currentDomain.ToString()))
            {
                return new KeyValuePair<bool, string>(false,
                    "Please configure domain which you want to use on CloudFlare account.");
            }

            try
            {
                var cloudFlareApiHelper = CloudFlareApiHelper.Instance;

                var domains = cloudFlareApiHelper.GetDomains();
                if (domains.Count <= 0)
                    return new KeyValuePair<bool, string>(false, "Your CloudFlare account doesn't have any domain or there was an error during retrieving them.");

                //// Legacy code, but could be helpful in later development
                //var matchedDomain = domains.Objs.FirstOrDefault(obj => currentDomain.Contains(obj.ZoneName));
                //if (matchedDomain == null)
                //    return new KeyValuePair<bool, string>(false,
                //        string.Format(
                //            "Check if domain [{0}] is configured in your CloudFlare account or the DNS record for this domain exists.",
                //            currentDomain));

                var matchedDomain = domains.Objs.FirstOrDefault(obj => currentDomain.ToString() == obj.ZoneName);
                if (matchedDomain == null)
                {
                    return new KeyValuePair<bool, string>(false, "Configured domain doesn't exist on CloudFlare account.");
                }

                var records = cloudFlareApiHelper.GetRecords(matchedDomain.ZoneName);

                return new KeyValuePair<bool, string>(records.Count > 0,
                    string.Format("Retrieved {0} DNS records for configured domain ({1}).", records.Count, currentDomain));
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false,
                    string.Format("There was an error during processing domain ({0}). Error message: {1}", currentDomain, ex.Message));
            }
        }

        [HttpPost]
        public void PurgeUrls(string[] urls)
        {
            var currentDomain = Config.GetSettingValue("Domain");
            if (currentDomain != null && !string.IsNullOrWhiteSpace(currentDomain.ToString()))
            {
                foreach (var url in urls)
                {
                    CloudFlareApiHelper.Instance.PurgeFileCache(currentDomain.ToString(), url);
                }
            }
        }
    }
}