using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class NotificationDto
    {
        public string operation { get; set; }
        public string description { get; set; }
        public string time { get; set; }
        public NotificationDto(string _operation, string _description)
        {
            operation = _operation;
            description = _description;
            time = DateTime.Now.ToString();
        }
    }
}