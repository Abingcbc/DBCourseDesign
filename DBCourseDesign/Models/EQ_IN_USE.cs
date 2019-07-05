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
    
    public partial class EQ_IN_USE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EQ_IN_USE()
        {
            this.PATROL_LOG = new HashSet<PATROL_LOG>();
            this.REPAIR_ORDER = new HashSet<REPAIR_ORDER>();
            this.WORK_ORDER = new HashSet<WORK_ORDER>();
        }
    
        public string ID { get; set; }
        public System.DateTime PRODUCTION_TIME { get; set; }
        public System.DateTime INSTALL_TIME { get; set; }
        public string OWNER { get; set; }
        public string MANAGER { get; set; }
        public string STATUS { get; set; }
        public string ADDRESS { get; set; }
        public Nullable<decimal> LONGITUDE { get; set; }
        public Nullable<decimal> LATITUDE { get; set; }
        public string QR_CODE { get; set; }
        public string REGION_ID { get; set; }
        public string TYPE_ID { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public System.DateTime INSERT_TIME { get; set; }
        public System.DateTime UPDATE_TIME { get; set; }
    
        public virtual REGION REGION { get; set; }
        public virtual EQ_TYPE EQ_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PATROL_LOG> PATROL_LOG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REPAIR_ORDER> REPAIR_ORDER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORK_ORDER> WORK_ORDER { get; set; }
    }
}
