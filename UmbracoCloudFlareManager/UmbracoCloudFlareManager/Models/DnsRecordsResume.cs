using System;
using System.Collections.Generic;

namespace UmbracoCloudFlareManager.Models
{
    public class DnsRecordsResume
    {
        public Int32 Count { get; set; }
        public Boolean HasMore { get; set; }
        public List<DnsObject> Objs { get; set; }
    }
}