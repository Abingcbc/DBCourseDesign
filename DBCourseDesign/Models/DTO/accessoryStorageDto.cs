namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ACCESSORYStorageDto
    {
        public ACCESSORYStorageDto()
        {
        }
        public string id { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public int number { get; set; }
    }

    public partial class detailedAccessoryStorageDto
    {
        public detailedAccessoryStorageDto()
        {
        }
        public string key { get; set; }
        public string model { get; set; }
        public string type { get; set; }
        public decimal price { get; set; }
        public int num { get; set; }
        public string warehouse { get; set; }
        public string warehouseID { get; set; }
    }

    public partial class modelIdMappingDto
    {
        public modelIdMappingDto()
        {
        }
        public string accessory { get; set; }
        public string model { get; set; }
    }

    public partial class ACCESSORYDispatchReceiver
    {
        public ACCESSORYDispatchReceiver()
        {
        }
        public string id { get; set; }
        public string type { get; set; }
        public string model { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int num { get; set; }
    }

    public partial class addAccessoryReceiver
    {
        public addAccessoryReceiver()
        {
        }
        public string accessoryID { get; set; }
        public int num { get; set; }
        public string warehouse { get; set; }
    }


}
