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
    
    public partial class EQ_TYPE_ACCESSORY
    {
        public string EQ_TYPE_ID { get; set; }
        public string ACCESSORY_ID { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> INSERT_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    
        public virtual ACCESSORY ACCESSORY { get; set; }
        public virtual EQ_TYPE EQ_TYPE { get; set; }
    }
}