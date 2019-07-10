using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class LoginDto
    {
        public string avatar { get; set; }
        public int deleted;
        public string password { get; set; }
        public string roleId { get; set; }
        public int status { get; set; }
        public string token;
        public string username { get; set; }
    }
}