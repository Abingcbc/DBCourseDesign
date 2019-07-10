namespace DBCourseDesign.Models
{
    using System;
    using System.Collections.Generic;
    public partial class MapEqDto
    {
        public MapEqDto()
        {
        }

        public decimal? lat { get; set; }
        public decimal? lon { get; set; }
        public string detailedAddress { get; set; }
        public string type { get; set; }
        public string model { get; set; }
    }

    public partial class MapWarehouseDto
    {
        public MapWarehouseDto()
        {
        }

        public decimal? lat { get; set; }
        public decimal? lon { get; set; }
        public string detailedAddress { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public partial class MapDto
    {
        public MapDto()
        {
        }

        public List<MapWarehouseDto> warehouse;
        public List<MapEqDto> usingEquipment;
    }
}
