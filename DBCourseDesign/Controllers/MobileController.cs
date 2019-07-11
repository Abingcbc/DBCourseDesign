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
        [Route("api/mobile/postrepairOrder")]
        public IHttpActionResult PostRepairOrder(MobileRepairOrderPutReciever mobileRepairOrderReciever)
        {
            var eq = db.EQ_IN_USE.Find(mobileRepairOrderReciever.deviceID);
            if (eq.STATUS == "1")
            {
                return Ok(returnHelper.make("fail2"));
            }
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
                eq.STATUS = "1";
                db.SaveChanges();
                NotificationController.NotificationCallbackMsg("增", mobileRepairOrderReciever.id+"添加了保修单");
                return Ok(returnHelper.make("success"));
            }
            catch (Exception)
            {
                return Ok(returnHelper.make("fail"));
            }
        }

        [HttpPost]
        [Route("api/mobile/putrepairOrder")]
        public IHttpActionResult PutRepairOrder(MobileRepairOrderPutReciever reciever)
        {
            var repair_order = db.REPAIR_ORDER.Find(reciever.id);
            if (repair_order == null)
            {
                return Ok(returnHelper.make("fail"));
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
                NotificationController.NotificationCallbackMsg("改", "更改了保修单");
                return Ok(returnHelper.make("success"));
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
                NotificationController.NotificationCallbackMsg("增", reciever.id+"增加了巡检单");
                return Ok(returnHelper.make("success"));
            }
            catch
            {
                return Ok(returnHelper.make("fail"));
            }
        }

        [HttpPost]
        [Route("api/mobile/workOrder")]
        public IHttpActionResult PostWorkOrder(MobileWorkOrderReciever mobileWorkOrderReciever)
        {
            var work_order = db.WORK_ORDER.Find(mobileWorkOrderReciever.id);
            if (work_order == null)
            {
                return Ok(returnHelper.make("fail"));
            }
            else
            {
                try
                {
                    work_order.WORK_PICTURE = mobileWorkOrderReciever.imgURL;
                    work_order.STATUS = mobileWorkOrderReciever.status.ToString();
                    db.SaveChanges();
                    NotificationController.NotificationCallbackMsg("改", "更改了工单");
                    return Ok(returnHelper.make("success"));
                }
                catch
                {
                    return Ok(returnHelper.make("fail"));
                }
            }
        }

        [HttpPost]
        [Route("api/mobile/delete")]
        public IHttpActionResult DeleteRepairOrder(MobileRepairOrderDeleteReciever reciever)
        {
            var repair_order = db.REPAIR_ORDER.Find(reciever.id);
            if (repair_order == null)
            {
                return Ok(returnHelper.make("fail"));
            }
            else
            {
                try
                {
                    NotificationController.NotificationCallbackMsg("删", repair_order.INSERT_BY+"删除了保修单");
                    db.REPAIR_ORDER.Remove(repair_order);
                    db.SaveChanges();
                    return Ok(returnHelper.make("success"));
                }
                catch
                {
                    return Ok(returnHelper.make("fail"));
                }
            }
        }

        [HttpGet]
        [Route("api/mobile/getRepair")]
        public IHttpActionResult GetRepair(string id)
        {
            var temp = db.PATROL_REGION.Where(p => p.PATROL_ID == id).ToList();
            var regions = db.PATROL_REGION.Where(p => p.PATROL_ID == id).Select(r => r.REGION_ID).ToList();
            if (regions == null)
            {
                return Ok(returnHelper.make(new MobileRepairOrderDto[] { }));
            }
            else
            {
                List<MobileRepairOrderDto> result = new List<MobileRepairOrderDto>();
                    var repair_orders_all = db.REPAIR_ORDER.Join(db.EQ_IN_USE.Where(q => regions.Contains(q.REGION_ID)), r => r.EQ_ID, e => e.ID,
                        (r, e) => new
                        {
                            id = r.ID,
                            device_id = e.ID,
                            address = e.ADDRESS,
                            latitude = e.LATITUDE,
                            longtitude = e.LONGITUDE,
                            url = r.REPORT_PICTURE,
                            detail = r.DESCRIPTION,
                            phone = r.TEL_NUMBER,
                            status = r.STATUS
                        });
                    var repair_orders = repair_orders_all.Where(r => r.status == "0");
                    foreach (var repair_order in repair_orders)
                    {
                        var pos_dic = new Dictionary<string, decimal?>();
                        pos_dic.Add("latitude", repair_order.latitude);
                        pos_dic.Add("longtitude", repair_order.longtitude);
                        result.Add(new MobileRepairOrderDto()
                        {
                            id = repair_order.id,
                            device_id = repair_order.device_id,
                            device_type = db.EQ_TYPE.Find(db.EQ_IN_USE.Find(repair_order.device_id).TYPE_ID).TYPE_NAME,
                            device_model = db.EQ_TYPE.Find(db.EQ_IN_USE.Find(repair_order.device_id).TYPE_ID).MODEL_NUMBER,
                            address = repair_order.address,
                            position = pos_dic,
                            url = repair_order.url,
                            detail = repair_order.detail,
                            phone = repair_order.phone
                        });
                    }
                return Ok(returnHelper.make(result));
            }
        }

        [HttpGet]
        [Route("api/mobile/getWork")]
        public IHttpActionResult GetWork(string id)
        {
            var work_orders = db.WORK_ORDER.Include("EQ_IN_USE").Where(w => w.REPAIRER_ID == id).ToList();
            if (work_orders == null)
            {
                return Ok(returnHelper.make(""));
            }
            else
            {
                List<MobileWorkOrderDto> result = work_orders.Select(r => new MobileWorkOrderDto(
                    r.ID, r.EQ_ID, r.EQ_IN_USE.ADDRESS, r.EQ_IN_USE.LONGITUDE, r.EQ_IN_USE.LATITUDE, r.WORK_PICTURE, 
                    db.EQ_TYPE.Find(r.EQ_IN_USE.TYPE_ID).TYPE_NAME, db.EQ_TYPE.Find(r.EQ_IN_USE.TYPE_ID).MODEL_NUMBER)).ToList();
                return Ok(returnHelper.make(result));
            }
        }

        [HttpGet]
        [Route("api/mobile/accessory")]
        public IHttpActionResult GetAccessory(string id)
        {
            var accessories = db.EQ_TYPE_ACCESSORY.Where(e => e.ACCESSORY_ID == id).Join(
                db.ACCESSORY, e => e.ACCESSORY_ID, a => a.ID,
                (e, a) => a.TYPE_NAME).ToList();
            if (accessories == null)
            {
                return Ok(returnHelper.make(""));
            }
            else
            {
                return Ok(returnHelper.make(accessories));
            }
        }

        [HttpGet]
        [Route("api/mobile/deviceModel")]
        public IHttpActionResult GetDeviceModel(string device_type)
        {
            var device_models = db.EQ_TYPE.Where(a => a.TYPE_NAME == device_type).Select(a => a.MODEL_NUMBER).ToList();
            if (device_models == null)
            {
                return Ok(returnHelper.make(""));
            }
            else
            {
                return Ok(returnHelper.make(device_models));
            }
        }
        
    }
}
