namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class repairSheetDto
    {
        public string title { get; set; }
        public string cover { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string details { get; set; }
        public string stuffNeeded { get; set; }
        public string telNumber { get; set; }
    }

    public partial class repairSheetReceiver
    {
        public string RSTid { get; set; }
        public string stfId { get; set; }
        public List<repairRequiredStuff> ls { get; set; }
    }

    public partial class repairRequiredStuff
    {
        public string type { get; set; }
        public string model { get; set; }
        public string number { get; set; }
        public string statue { get; set; }
        public string key { get; set; }
    }

    public partial class storageAndNum
    {
        public int no { get; set; }
        public string type { get; set; }
        public string model { get; set; }
        public int number { get; set; }
    }
    public partial class staffIdAndName
    {
        public string staffId { get; set; }
        public string staffName { get; set; }
    }
    public partial class eqNacNrepairerDto
    {
        public List<storageAndNum> equipType { get; set; }
        public List<storageAndNum> accessory { get; set; }
        public List<staffIdAndName> staff { get; set; }
    }
}