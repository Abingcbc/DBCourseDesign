using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class MobileWorkOrderDto
    {
        public string id { get; set; }
        public string device_id { get; set; }
        public string device_type { get; set; }
        public string device_model { get; set; }
        public string address { get; set; }
        public Dictionary<string, decimal?> position { get; set; }
        public string url { get; set; }

        public MobileWorkOrderDto(string _id, string _device_id, string _address, 
            decimal? _longtitude, decimal? _latitude, string _url, string _device_type, string _device_model)
        {
            id = _id;
            device_id = _device_id;
            address = _address;
            url = _url;
            position.Add("latitude", _latitude);
            position.Add("longtitude", _longtitude);
            device_type = _device_type;
            device_model = _device_model;
        }
    }
}