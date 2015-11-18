using System;
using System.Collections.Generic;

namespace UmbracoCloudFlareManager.Models
{
    public class Zones
    {
        public Int32 Count { get; set; }
        public Boolean HasMore { get; set; }
        public List<ZoneObject> Objs { get; set; }
    }
}