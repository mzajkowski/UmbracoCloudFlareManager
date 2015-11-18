namespace UmbracoCloudFlareManager.Models
{
    public class Request
    {
        public string Act { get; set; }
        public string A { get; set; }       // The method to execute
        public string Tkn { get; set; }     // The API key
        public string Email { get; set; }   // The email associated to the API key
        public string Type { get; set; }
        public string Z { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Ttl { get; set; }
        public string ServiceMode { get; set; }
    }
}