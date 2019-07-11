namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class workSheetDto
    {
        public string id { get; set; }
        public string equipID { get; set; }
        public string repairerID { get; set; }
        public string repairArea { get; set; }
        public string statue { get; set; }
        public string dispatcherID { get; set; }
        public string work_picture { get; set; }
        public string dispatcherName { get; set; }
        public string repairerName { get; set; }
    }
}