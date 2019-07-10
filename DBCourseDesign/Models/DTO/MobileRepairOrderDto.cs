using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class MobileRepairOrderDto
    {
        public string id { get; set; }
        public string device_id { get; set; }
        public string address { get; set; }
        public Dictionary<string, double> position { get; set; }
        public string url { get; set; }
        public string detail { get; set; }
        public string phone { get; set; }
    }
}