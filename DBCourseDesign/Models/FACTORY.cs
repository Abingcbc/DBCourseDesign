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
    
    public partial class FACTORY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FACTORY()
        {
            this.ACCESSORY = new HashSet<ACCESSORY>();
            this.EQ_TYPE = new HashSet<EQ_TYPE>();
        }
    
        public string ID { get; set; }
        public string NAME { get; set; }
        public string PRINCIPAL { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> INSERT_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCESSORY> ACCESSORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQ_TYPE> EQ_TYPE { get; set; }
    }
}
