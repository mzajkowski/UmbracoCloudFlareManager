namespace UmbracoCloudFlareManager.Models
{
    public class PurgeCacheResponse : ResponseBase
    {
        public PurgeCacheResult response { get; set; }
        public AttributesResult attributes { get; set; }
    }

    public class SingleFilePurgeCacheResponse : ResponseBase
    {
        public SingleFilePurgeCacheResult response { get; set; }
    }

    public class AttributesResult
    {
        public int cooldown { get; set; }
    }

    public class PurgeCacheResult
    {
        public string fpurge_ts { get; set; }
    }

    public class SingleFilePurgeCacheResult
    {
        public string vtxt_match { get; set; }
        public string url { get; set; }
    }
}