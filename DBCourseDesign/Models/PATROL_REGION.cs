//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PATROL_REGION
    {
        public string PATROL_ID { get; set; }
        public string REGION_ID { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> INSERT_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    
        public virtual PATROL PATROL { get; set; }
        public virtual REGION REGION { get; set; }
    }
}
