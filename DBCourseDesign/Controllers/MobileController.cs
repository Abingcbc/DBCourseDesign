using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DBCourseDesign.Models;
using DBCourseDesign.Models.DTO;
using Newtonsoft.Json;

namespace DBCourseDesign.Controllers
{
    public class MobileController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        [HttpPost]
        [ResponseType(typeof(returnDto<mobileStaffDto>))]
        public IHttpActionResult PostLogin(MobileStaffReciever mobileStaffReciever)
        {
            var staff = db.STAFF.Where(s => s.ACCOUNT_ID == mobileStaffReciever.count_id && 
            s.PASSWORD == mobileStaffReciever.password).FirstOrDefault();
            if (staff == null)
            {
                return Ok(new returnDto<mobileStaffDto>(new mobileStaffDto()));
            }
            else
            {
                var patrol = db.PATROL.Find(staff.ID);
                if (patrol != null)
                {
                    return Ok(new returnDto<mobileStaffDto>(new mobileStaffDto()
                    {
                        name = staff.NAME,
                        id = staff.ID,
                        type = "巡检员"
                    }));
                }
                var repairer = db.REPAIRER.Find(staff.ID);
                if (repairer == null)
                {
                    return Ok(new returnDto<mobileStaffDto>(new mobileStaffDto()));
                }
                else
                {
                    return Ok(new returnDto<mobileStaffDto>(new mobileStaffDto()
                    {
                        name = staff.NAME,
                        id = staff.ID,
                        type = "维修员"
                    }));
                }
            }
        }
    }
}
