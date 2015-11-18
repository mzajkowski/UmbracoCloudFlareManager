namespace UmbracoCloudFlareManager.Models
{
    public class UcfmConfigSettings
    {
        public bool EnableCloudflare { get; set; }
        public string CloudFlareEmail { get; set; }
        public string CloudFlareApiKey { get; set; }
        public string Domain { get; set; }
        public bool PurgeHomepage { get; set; }
        public int Homepage { get; set; }

        public object this[string propertyName]
        {
            get { return GetType().GetProperty(propertyName).GetValue(this, null); }
            set { GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}