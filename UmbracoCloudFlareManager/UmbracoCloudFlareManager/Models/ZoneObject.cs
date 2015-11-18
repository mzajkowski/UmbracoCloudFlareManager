using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UmbracoCloudFlareManager.Models
{
    public class ZoneObject
    {
        public Int64 ZoneId { get; set; }
        public Int64 UserId { get; set; }
        public String ZoneName { get; set; }
        public String DisplayName { get; set; }
        public String ZoneStatus { get; set; }
        public int ZoneMode { get; set; }
        public Int64 HostId { get; set; }
        public string ZoneType { get; set; }
        public string HostPubname { get; set; }
        public string HostWebsite { get; set; }
        public string Vtxt { get; set; }
        public List<string> Fqdns { get; set; }
        public string Step { get; set; }
        public string ZoneStatusClass { get; set; }
        public string ZoneStatusDesc { get; set; }
        public List<string> NsVanityMap { get; set; }
        public string OrigRegistrar { get; set; }
        public string OrigDnshost { get; set; }
        public string OrigNsNames { get; set; }
        public ZoneProps Props { get; set; }
        public ConfirmCode ConfirmCode { get; set; }
        public List<String> Allow { get; set; }
    }
}