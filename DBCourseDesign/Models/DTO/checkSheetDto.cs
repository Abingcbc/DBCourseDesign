namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class checkSheetDto
    {
        public string id { get; set; }
        public string patrolID { get; set; }
        public string patrolName { get; set; }
        public string eqID { get; set; }
        public string checkArea { get; set; }
        public string checkTime { get; set; }
        public string checkPic { get; set; }
    }

    public partial class deleteCheckSheetDto
    {
        public ICollection<checkSheetDto> data;
        public string deleteInfo;
    }
}