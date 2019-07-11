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
}