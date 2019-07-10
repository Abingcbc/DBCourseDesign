using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class MobileRepairOrderPutReciever
    {
        public string deviceID { get; set; }
        public string imgURL { get; set; }
        public string detail { get; set; }
        public string phone { get; set; }
        public string problem_type { get; set; }
        public int status;
        public string id = "0000";
    }

    public class MobileRepairOrderPostReciever
    {
        public string deviceID { get; set; }
        public string imgURL { get; set; }
        public string detail { get; set; }
        public string phone { get; set; }
        public string problem_type { get; set; }
        public int status;
    }

    public class MobileRepairOrderDeleteReciever
    {
        public string id { get; set; }
    }

    public class MobileRepairGetReciever
    {
        public string id { get; set; }
    }
    
}