namespace UmbracoCloudFlareManager.Models
{
    public class DnsProps
    {
        public int Proxiable { get; set; }
        public int CloudOn { get; set; }
        public int CfOpen { get; set; }
        public int Ssl { get; set; }
        public int ExpiredSsl { get; set; }
        public int ExpiringSsl { get; set; }
        public int PendingSsl { get; set; }
        public int VanityLock { get; set; }
    }
}