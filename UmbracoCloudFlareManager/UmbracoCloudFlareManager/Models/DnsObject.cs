namespace UmbracoCloudFlareManager.Models
{
    public class DnsObject
    {
        public string RecId { get; set; }
        public string RecTag { get; set; }
        public string ZoneName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }
        public string Prio { get; set; }
        public string Content { get; set; }
        public string DisplayContent { get; set; }
        public string Ttl { get; set; }
        public string TtlCeil { get; set; }
        public string SslId { get; set; }
        public string SslStatus { get; set; }
        public string SslExpiresOn { get; set; }
        public string AutoTtl { get; set; }
        public string ServiceMode { get; set; }
        public DnsProps Props { get; set; }
    }
}