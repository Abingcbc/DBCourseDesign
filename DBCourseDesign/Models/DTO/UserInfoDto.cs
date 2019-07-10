using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCourseDesign.Models.DTO
{
    public class UserInfoDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string telephone { get; set; }
        public string avatar { get; set; }
        public int status;
        public int deleted;
        public string roleId { get; set; }
        public UserRoleDto role = new UserRoleDto();
    }

    public class UserRoleDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string describe { get; set; }
        public int status;
        public int deleted;
        public Dictionary<string, string>[] permissions = new Dictionary<string, string>[]
        {
            new Dictionary<string, string>()
        };

        public UserRoleDto()
        {
            permissions[0].Add("roleId", "");
            permissions[0].Add("permissionId", "");
            permissions[0].Add("permissionName", "");
        }
    }
}