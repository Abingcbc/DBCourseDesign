namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;
    public partial class WAREHOUSEPreviewDto
    {
        public WAREHOUSEPreviewDto()
        {
        }

        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public partial class WAREHOUSEDetailDto
    {
        public WAREHOUSEDetailDto()
        {
        }
        public string name { get; set; }
        public string detailedAddress { get; set; }
        public string address { get; set; }
    }

    public partial class WAREHOUSEStorageDto
    {
        public WAREHOUSEStorageDto()
        {
        }
        public virtual ICollection<ACCESSORYStorageDto> accessory { get; set; }
        public virtual ICollection<EQStorageDto> equipment { get; set; }
    }

}
