namespace UmbracoCloudFlareManager
{
    public class PluginConstants
    {
        #region CloudFlare API

        public class CloudFlareApi
        {
            #region Methods

            public class Methods
            {
                public const string Stats = "stats";
                public const string LoadZones = "zone_load_multi";
                public const string LoadRecords = "rec_load_all";
                public const string ZoneCheck = "zone_check";
                public const string ZoneIps = "zone_ips";
                public const string IpThreatScore = "ip_lkup";
                public const string ZoneSettings = "zone_settings";

                public const string AddRecord = "rec_new";
                public const string EditRecord = "rec_edit";
                public const string DeleteRecord = "rec_delete";

                public const string SecurityLevel = "sec_lvl";
                public const string CacheLevel = "cache_lvl";
                public const string DevMode = "devmode";
                public const string PurgeCache = "fpurge_ts";
                public const string PurgeFile = "zone_file_purge";
                public const string ZoneGrab = "zone_grab";
                public const string SetIpV6 = "ipv46";
                public const string Async = "async";
                public const string Minify = "minify";
                public const string Mirage2 = "mirage2";
            }

            #endregion

            #region Parameters

            public class Parameters
            {
                public const string ApiKey = "tkn";
                public const string Email = "email";
                public const string Method = "a";
                public const string Domain = "z";
                public const string Interval = "interval";
                public const string Zones = "zones";
                public const string Hours = "hours";
                public const string Class = "class";
                public const string Geo = "geo";
                public const string Ip = "ip";
                public const string Type = "type";
                public const string Name = "name";
                public const string Content = "content";
                public const string Ttl = "ttl";
                public const string Id = "id";
                public const string ServiceMode = "service_mode";
                public const string DevMode = "v";
                public const string PurgeCache = "v";
                public const string Url = "url";
                public const string ZoneId = "zid";
                public const string Key = "key";
                public const string V = "v";
            }

            #endregion
        }

        #endregion
    }
}