namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class EQStorageDto
    {
        public string id { get; set; }
        public string model { get; set; }
        public string type { get; set; }

    }

    //wyc
    public partial class detailedEQStorageDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string productTime { get; set; }
        public string status { get; set; }
        public string modelID { get; set; }
        public string price { get; set; }
        public string warehouseID { get; set; }
    }
}
