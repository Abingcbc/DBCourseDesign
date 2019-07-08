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
    
    public partial class EQ_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EQ_TYPE()
        {
            this.EQ_IN_USE = new HashSet<EQ_IN_USE>();
            this.EQ_STORED = new HashSet<EQ_STORED>();
            this.EQ_TYPE_ACCESSORY = new HashSet<EQ_TYPE_ACCESSORY>();
        }
    
        public string ID { get; set; }
        public string TYPE_NAME { get; set; }
        public string MODEL_NUMBER { get; set; }
        public decimal PRICE { get; set; }
        public string FACTORY_ID { get; set; }
        public string PICTURE { get; set; }
        public string INSERT_BY { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> INSERT_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQ_IN_USE> EQ_IN_USE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQ_STORED> EQ_STORED { get; set; }
        public virtual FACTORY FACTORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EQ_TYPE_ACCESSORY> EQ_TYPE_ACCESSORY { get; set; }
    }
}
