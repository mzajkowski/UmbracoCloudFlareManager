using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Xml.Serialization;
using Umbraco.Core.Logging;
using UmbracoCloudFlareManager.Models;

namespace UmbracoCloudFlareManager
{
    public class Config
    {
        public const string PluginFolder = "~/App_plugins/UmbracoCloudFlareManager";
        public const string ConfigFileName = "UmbracoCloudFlareManager.config";

        public static UcfmConfigSettings GetSettings()
        {
            return ConfigFile;
        }

        public static object GetSettingValue(string settingKey)
        {
            return ConfigFile[settingKey];
        }

        public static void SaveSettings(UcfmConfigSettings settings)
        {
            var configFile = HostingEnvironment.MapPath(string.Format("{0}/{1}", PluginFolder, ConfigFileName));
            if (configFile == null) return;

            if (File.Exists(configFile))
                File.Delete(configFile);

            var serializer = new XmlSerializer(typeof(UcfmConfigSettings));
            using (var w = new StreamWriter(configFile))
            {
                serializer.Serialize(w, settings);
            }

            HttpRuntime.Cache.Remove("umbracoCloudFlareManagerSettingsFile");
            EnsureConfig();
        }

        public static UcfmConfigSettings ConfigFile
        {
            get
            {
                var us = (UcfmConfigSettings)HttpRuntime.Cache["umbracoCloudFlareManagerSettingsFile"] ?? EnsureConfig();
                return us;
            }
        }

        private static UcfmConfigSettings EnsureConfig()
        {
            var settingsFile = HttpRuntime.Cache["umbracoCloudFlareManagerSettingsFile"];
            var fullPath = HostingEnvironment.MapPath(PluginFolder + "/" + ConfigFileName);

            if (settingsFile != null) return (UcfmConfigSettings)settingsFile;

            var temp = new UcfmConfigSettings { CloudFlareEmail = string.Empty, CloudFlareApiKey = string.Empty};
            if (fullPath == null) return temp;

            try
            {
                var configFile = HostingEnvironment.MapPath(string.Format("{0}/{1}", PluginFolder, ConfigFileName));
                var serializer = new XmlSerializer(typeof(UcfmConfigSettings));

                using (var fs = new FileStream(configFile, FileMode.Open))
                {
                    temp = (UcfmConfigSettings)serializer.Deserialize(fs);
                    HttpRuntime.Cache.Insert("umbracoCloudFlareManagerSettingsFile", temp, new CacheDependency(fullPath));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Warn<Config>("Unable to load the settings: {0}", () => ex);
            }

            return temp;
        }
    }
}