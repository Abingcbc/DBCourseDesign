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
    
    public partial class EQ_STORED
    {
        public string ID { get; set; }
        public System.DateTime PRODUCT_TIME { get; set; }
        public string STATUS { get; set; }
        public string EQ_TYPE_ID { get; set; }
        public string WAREHOUSE_ID { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public System.DateTime INSERT_TIME { get; set; }
        public System.DateTime UPDATE_TIME { get; set; }
    
        public virtual EQ_TYPE EQ_TYPE { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
    }
}
