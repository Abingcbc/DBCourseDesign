using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBCourseDesign.Models;
using DBCourseDesign.Models.DTO;

namespace DBCourseDesign.Controllers
{
    public class UserController : ApiController
    {
        FEMSContext db = new FEMSContext();

        //api/user/info
        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            string token = this.Request.Headers.GetValues("Access-Token").First();
            if (!AuthController.token_id.ContainsKey(token))
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            string id = AuthController.token_id[token];
            var staff = db.STAFF.Find(id);
            if (staff == null)
            {
                return Ok(new UserInfoDto());
            }
            else
            {
                UserInfoDto userInfoDto = new UserInfoDto();
                userInfoDto.id = token;
                userInfoDto.name = staff.NAME;
                userInfoDto.username = staff.ACCOUNT_ID;
                userInfoDto.password = staff.PASSWORD;
                userInfoDto.telephone = staff.TEL_NUMBER;
                userInfoDto.status = 1;
                userInfoDto.deleted = 0;
                userInfoDto.role.status = 1;
                userInfoDto.role.deleted = 0;
                var dispathcer = db.DISPATCHER.Find(id);
                if (dispathcer == null)
                {
                    var repairer = db.REPAIRER.Find(id);
                    if (repairer == null)
                    {
                        var patrol = db.PATROL.Find(id);
                        userInfoDto.role.start = patrol.PATROL_START;
                        userInfoDto.role.end = patrol.PATROL_STOP;
                    }
                    else
                    {
                        userInfoDto.role.start = "周一";
                        userInfoDto.role.end = "周日";
                    }
                }
                else
                {
                    userInfoDto.role.start = dispathcer.DISPATCH_START;
                    userInfoDto.role.end = dispathcer.DISPATCH_STOP;
                }
                if (staff.IS_SUPER == "1")
                {
                    userInfoDto.avatar = AuthController.ADMIN_AVATAR;
                    userInfoDto.roleId = "admin";
                    userInfoDto.role.id = "admin";
                    userInfoDto.role.name = "管理员";
                    userInfoDto.role.describe = "拥有所有权限";
                    userInfoDto.role.permissions[0]["roleId"] = "admin";
                    userInfoDto.role.permissions[0]["permissionId"] = "super";
                    userInfoDto.role.permissions[0]["permissionName"] = "超级权限";
                }
                else
                {
                    userInfoDto.avatar = AuthController.USER_AVATAR;
                    userInfoDto.roleId = "user";
                    userInfoDto.role.id = "user";
                    userInfoDto.role.name = "用户";
                    userInfoDto.role.describe = "拥有部分权限";
                    userInfoDto.role.permissions[0]["roleId"] = "user";
                    userInfoDto.role.permissions[0]["permissionId"] = "";
                    userInfoDto.role.permissions[0]["permissionName"] = "部分权限";
                }
                Dictionary<string, UserInfoDto> result = new Dictionary<string, UserInfoDto>();
                result.Add("result", userInfoDto);
                return Ok(result);
            }
        }
    }
}
