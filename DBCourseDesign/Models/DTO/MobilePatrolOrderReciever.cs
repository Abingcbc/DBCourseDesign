using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class MobilePatrolOrderReciever
    {
        public string deviceID { get; set; }
        public string id { get; set; }
        public string imgURL { get; set; }
        public int status;
        public string time { get; set; }
    }

    public class MobilePatrolOrderDeleteReciever
    {
        public string id { get; set; }
    }
}