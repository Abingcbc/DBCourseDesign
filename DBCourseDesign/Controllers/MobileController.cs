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
        [Route("api/mobile/login")]
        public IHttpActionResult PostLogin(MobileStaffReciever mobileStaffReciever)
        {
            var staff = db.STAFF.Where(s => s.ACCOUNT_ID == mobileStaffReciever.count_id && 
            s.PASSWORD == mobileStaffReciever.password).FirstOrDefault();
            if (staff == null)
            {
                return Ok(returnHelper.make(new mobileStaffDto()));
            }
            else
            {
                var patrol = db.PATROL.Find(staff.ID);
                if (patrol != null)
                {
                    return Ok(returnHelper.make(new mobileStaffDto()
                    {
                        name = staff.NAME,
                        id = staff.ID,
                        type = "巡检员"
                    }));
                }
                var repairer = db.REPAIRER.Find(staff.ID);
                if (repairer == null)
                {
                    return Ok(returnHelper.make(new mobileStaffDto()));
                }
                else
                {
                    return Ok(returnHelper.make(new mobileStaffDto()
                    {
                        name = staff.NAME,
                        id = staff.ID,
                        type = "维修员"
                    }));
                }
            }
        }

        [HttpPost]
        [Route("api/mobile/repairOrder")]
        public IHttpActionResult PostRepairOrder(MobileRepairOrderPutReciever mobileRepairOrderReciever)
        {
            REPAIR_ORDER repair_order = new REPAIR_ORDER()
            {
                ID = "0000",//因为数据库中有自增的触发器，所以ID可以随意输入任意字符串
                REPORT_PICTURE = mobileRepairOrderReciever.imgURL,
                REPAIR_TYPE = mobileRepairOrderReciever.problem_type,
                DESCRIPTION = mobileRepairOrderReciever.detail,
                STATUS = mobileRepairOrderReciever.status.ToString(),
                TEL_NUMBER = mobileRepairOrderReciever.phone,
                EQ_ID = mobileRepairOrderReciever.deviceID,
                DISPATCHER_ID = "",
                INSERT_BY = mobileRepairOrderReciever.id,
                UPDATE_BY = mobileRepairOrderReciever.id,
                INSERT_TIME = DateTime.Now,
                UPDATE_TIME = DateTime.Now
            };
            try
            {
                db.REPAIR_ORDER.Add(repair_order);
                db.SaveChanges();
                //NotificationController.NotificationCallbackMsg("")
                return StatusCode(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPut]
        [Route("api/mobile/repairOrder")]
        public IHttpActionResult PutRepairOrder(MobileRepairOrderPutReciever reciever)
        {
            var repair_order = db.REPAIR_ORDER.Find(reciever.id);
            if (repair_order == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                repair_order.EQ_ID = reciever.deviceID;
                repair_order.REPORT_PICTURE = reciever.imgURL;
                repair_order.DESCRIPTION = reciever.detail;
                repair_order.TEL_NUMBER = reciever.phone;
                repair_order.REPAIR_TYPE = reciever.problem_type;
                repair_order.STATUS = reciever.status.ToString();
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        [Route("api/mobile/patrolOrder")]
        public IHttpActionResult PostPatrolOrder(MobilePatrolOrderReciever reciever)
        {
            PATROL_LOG patrol_log = new PATROL_LOG()
            {
                ID = "0000",
                PATROL_ID = reciever.id,
                EQ_ID = reciever.deviceID,
                PATROL_TIME = reciever.time,
                PATROL_RESULT = reciever.status.ToString(),
                PATROL_PICTURE = reciever.imgURL,
                INSERT_BY = reciever.id,
                UPDATE_BY = reciever.id,
                INSERT_TIME = DateTime.Now,
                UPDATE_TIME = DateTime.Now
                //EQ_IN_USE = db.EQ_IN_USE.Find(mobilePatrolOrder.deviceID),
                //PATROL = db.PATROL.Find(mobilePatrolOrder.id)
            };
            try
            {
                db.PATROL_LOG.Add(patrol_log);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
        }

        [HttpPost]
        [Route("api/mobile/workOrder")]
        public IHttpActionResult PostWorkOrder(MobileWorkOrderReciever mobileWorkOrderReciever)
        {
            var work_order = db.WORK_ORDER.Find(mobileWorkOrderReciever.id);
            if (work_order == null)
            {
                return StatusCode(HttpStatusCode.ExpectationFailed);
            }
            else
            {
                try
                {
                    work_order.WORK_PICTURE = mobileWorkOrderReciever.imgURL;
                    work_order.STATUS = mobileWorkOrderReciever.status.ToString();
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.OK);
                }
                catch
                {
                    return StatusCode(HttpStatusCode.ExpectationFailed);
                }
            }
        }

        [HttpDelete]
        [Route("api/mobile/delete")]
        public IHttpActionResult DeleteRepairOrder(MobileRepairOrderDeleteReciever reciever)
        {
            var repair_order = db.REPAIR_ORDER.Find(reciever.id);
            if (repair_order == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                try
                {
                    db.REPAIR_ORDER.Remove(repair_order);
                    db.SaveChanges();
                    return StatusCode(HttpStatusCode.OK);
                }
                catch
                {
                    return StatusCode(HttpStatusCode.ExpectationFailed);
                }
            }
        }

        [HttpGet]
        [Route("api/mobile/getRepair")]
        public IHttpActionResult GetRepair(MobileRepairGetReciever reciever)
        {
            var regions = db.PATROL_REGION.Where(p => p.PATROL_ID == reciever.id).ToList();
            if (regions == null)
            {
                return Ok(returnHelper.make(new MobileRepairOrderDto[] { }));
            }
            else
            {
                LinkedList<MobileRepairOrderDto> result = new LinkedList<MobileRepairOrderDto>();
                foreach (var region in regions){
                    var repair_orders = db.REPAIR_ORDER.Join(db.EQ_IN_USE, r => r.EQ_ID, e => e.ID,
                        (r, e) => new
                        {
                            id = r.ID,
                            device_id = e.ID,
                            address = e.ADDRESS,
                            latitude = e.LATITUDE,
                            longtitude = e.LONGITUDE,
                            url = r.REPORT_PICTURE,
                            detail = r.DESCRIPTION,
                            phone = r.TEL_NUMBER
                        });
                    foreach (var repair_order in repair_orders)
                    {
                        var pos_dic = new Dictionary<string, decimal?>();
                        pos_dic.Add("latitude", repair_order.latitude);
                        pos_dic.Add("longtitude", repair_order.longtitude);
                        result.AddLast(new MobileRepairOrderDto()
                        {
                            id = repair_order.id,
                            device_id = repair_order.device_id,
                            address = repair_order.address,
                            position = pos_dic,
                            url = repair_order.url,
                            detail = repair_order.detail,
                            phone = repair_order.phone
                        });
                    }
                }
                return Ok(returnHelper.make(result));
            }
        }
        
    }
}
