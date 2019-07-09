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
    public class AuthController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        private Dictionary<string, string> token_id = new Dictionary<string, string>();
        
        //api/auth/login
        [HttpPost]
        public IHttpActionResult PostLogin(LoginReciever loginReciever)
        {
            Dictionary<string, LoginDto> response = new Dictionary<string, LoginDto>();
            var staff = db.STAFF.Where(s => s.ACCOUNT_ID == loginReciever.username && 
            s.PASSWORD == loginReciever.password).FirstOrDefault();
            if (staff == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            if (staff.IS_SUPER == "1")
            {
                response.Add("result", new LoginDto()
                {
                    avatar = "https://i.loli.net/2019/07/09/5d248db928ee641756.jpg",
                    deleted = 0,
                    password = "",
                    roleId = "admin",
                    status = 1,
                    token = (new Random().Next()).ToString(),
                    username = loginReciever.username
                });
                token_id.Add(response["result"].token, staff.ID);
                return Ok(response);
            }
            var dispatcher = db.DISPATCHER.Find(staff.ID);
            if (dispatcher == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            } else
            {
                response.Add("result", new LoginDto()
                {
                    avatar = "https://i.loli.net/2019/07/09/5d248f778b63b46075.png",
                    deleted = 0,
                    password = "",
                    roleId = "admin",
                    status = 1,
                    token = (new Random().Next()).ToString(),
                    username = loginReciever.username
                });
                token_id.Add(response["result"].token, staff.ID);
                return Ok(response);
            }
        }

        //api/auth/logout
        [HttpPost]
        public void PostLogout(LogoutReciever logoutReciever)
        {
            token_id.Remove(logoutReciever.token);
        }
    }
}
