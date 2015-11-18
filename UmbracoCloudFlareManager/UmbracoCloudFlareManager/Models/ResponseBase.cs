using System;

namespace UmbracoCloudFlareManager.Models
{
    public abstract class ResponseBase
    {
        // Request made
        public Request request { get; set; }

        // Result Status. Mapped in {CloudFlare.API.Enums.ResultKind}: "success" / "error"
        public String result { get; set; }

        // Result description, with the error message if available
        public String msg { get; set; }
    }

    public class SimpleResponse : ResponseBase
    { }
}