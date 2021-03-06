﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DBCourseDesign.Models;

namespace DBCourseDesign.Controllers
{
    public class staffController : ApiController
    {
        private FEMSContext db = new FEMSContext();

        /// <returns></returns>
        [HttpGet]
        [Route("api/staff/staffSheet")]
        public async Task<IHttpActionResult> GetStaffSheet()
        {
            return Ok(returnHelper.make(getDtoList()));
        }



        /// <summary>
        /// return all staff as a list of Dto
        /// </summary>
        /// <returns></returns>
        private List<staffDto> getDtoList()
        {
            var result = new List<staffDto>();
            //repairer
            foreach (var r in db.REPAIRER.Include(r => r.STAFF).Where(r => r.STAFF.IS_SUPER == "0"))
            {
                result.Add(new staffDto
                {
                    id = "ST" + r.ID,
                    name = r.STAFF.NAME,
                    accountID = r.STAFF.ACCOUNT_ID,
                    password = r.STAFF.PASSWORD,
                    status = "1",
                    detail = StaffItem.makeRow(r.STAFF.TEL_NUMBER, r.STAFF.ID_CARD_NUMBER,
                     r.STAFF.INSERT_TIME.ToString(), r.STAFF.UPDATE_TIME.ToString(),
                     "周一", "周日")
                });
            }

            foreach (var r in db.PATROL.Include(r => r.STAFF).Where(r => r.STAFF.IS_SUPER == "0"))
            {
                result.Add(new staffDto()
                {
                    id = "ST" + r.ID,
                    name = r.STAFF.NAME,
                    accountID = r.STAFF.ACCOUNT_ID,
                    password = r.STAFF.PASSWORD,
                    status = "0",
                    detail = StaffItem.makeRow(r.STAFF.TEL_NUMBER, r.STAFF.ID_CARD_NUMBER, r.STAFF.INSERT_TIME.ToString(), r.STAFF.UPDATE_TIME.ToString(), r.PATROL_START, r.PATROL_STOP)
                });
            }
            foreach (var r in db.DISPATCHER.Include(r => r.STAFF).Where(r => r.STAFF.IS_SUPER == "0"))
            {
                result.Add(new staffDto()
                {
                    id = "ST" + r.ID,
                    name = r.STAFF.NAME,
                    accountID = r.STAFF.ACCOUNT_ID,
                    password = r.STAFF.PASSWORD,
                    status = "2",
                    detail = StaffItem.makeRow(r.STAFF.TEL_NUMBER, r.STAFF.ID_CARD_NUMBER, r.STAFF.INSERT_TIME.ToString(), r.STAFF.UPDATE_TIME.ToString(), r.DISPATCH_START, r.DISPATCH_START)
                });
            }
            return result;
        }

        /// <summary>
        /// delete a staff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/staff/staffSheetDelete")]
        public async Task<IHttpActionResult> deleteStaffSheetRow(stringReceiver sR)
        {
            string id = sR.decoded();
            try
            {
                var staff = await db.STAFF.FindAsync(id);
                if (staff == null || staff.IS_SUPER != "0")
                    throw new Exception();
                staff.IS_SUPER = "-1";
                await db.SaveChangesAsync();
                NotificationController.NotificationCallbackMsg("开除员工" + "  编号" + staff.ID);
                return Ok(returnHelper.make(getDtoList()));
            }
            catch (Exception)
            {
                return Ok(returnHelper.fail());
            }
        }

        /// <summary>
        /// modify a staff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/staff/staffSheetModify")]
        public async Task<IHttpActionResult> modifyStaff(staffModifyReceiver input)
        {
            input.id = input.id.Substring(2);
            try
            {
                var staff = await db.STAFF.FindAsync(input.id);
                //cannot modify info about a super manager or an expelled
                if (staff == null || staff.IS_SUPER != "0")
                    throw new Exception();
                //repeated id
                if (input.accountID != staff.ACCOUNT_ID)
                {
                    if (db.STAFF.Count(s => s.ACCOUNT_ID == input.accountID) > 0)
                        throw new Exception();
                }

                staff.NAME = input.name;
                staff.PASSWORD = input.password;
                staff.TEL_NUMBER = input.telNumber;
                staff.ID_CARD_NUMBER = input.idCardNumber;
                staff.UPDATE_TIME = DateTime.Now;
                await db.Entry(staff).Reference(s => s.REPAIRER).LoadAsync();
                await db.Entry(staff).Reference(s => s.REPAIRER).LoadAsync();
                await db.Entry(staff).Reference(s => s.REPAIRER).LoadAsync();

                string status = "";
                if (staff.DISPATCHER != null)
                    status = "2";
                else if (staff.PATROL != null)
                    status = "0";
                else if (staff.REPAIRER != null)
                    status = "1";
                else
                    throw new Exception();

                if (status == input.status)
                {
                    if (input.status == "0")
                    {
                        staff.PATROL.PATROL_START = input.startTime;
                        staff.PATROL.PATROL_STOP = input.endTime;
                    }
                    else if (input.status == "2")
                    {
                        staff.DISPATCHER.DISPATCH_START = input.startTime;
                        staff.DISPATCHER.DISPATCH_STOP = input.endTime;
                    }
                }
                else
                    throw new Exception();

                await db.SaveChangesAsync();
                NotificationController.NotificationCallbackMsg("修改员工信息 员工姓名" + input.name + " 编号" + input.id);
                return Ok(returnHelper.make(getDtoList()));
            }
            catch (Exception e)
            {
                //return Ok(returnHelper.fail());
                throw;
            }
        }

        /// <summary>
        /// add staff
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/staff/staffSheetAdd")]
        public async Task<IHttpActionResult> addStaff(staffModifyReceiver input)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    var staff = new STAFF
                    {
                        ID = "1",
                        ID_CARD_NUMBER = input.idCardNumber,
                        INSERT_TIME = DateTime.Now,
                        IS_SUPER = "0",
                        NAME = input.name,
                        PASSWORD = input.password,
                        TEL_NUMBER = input.telNumber,
                        UPDATE_TIME = DateTime.Now,
                        ACCOUNT_ID = input.status == "0" ? "p" : input.status == "1" ? "r" : input.status == "2" ? "d" : "x"
                    };
                    db.STAFF.Add(staff);
                    await db.SaveChangesAsync();
                    if (input.status == "0")
                    {
                        var patrol = new PATROL
                        {
                            ID = staff.ID,
                            PATROL_START = input.startTime,
                            PATROL_STOP = input.endTime
                        };
                        db.PATROL.Add(patrol);
                    }
                    else if (input.status == "1")
                    {
                        var repairer = new REPAIRER
                        {
                            ID = staff.ID,
                        };
                        db.REPAIRER.Add(repairer);
                    }
                    else if (input.status == "2")
                    {
                        var dispatcher = new DISPATCHER
                        {
                            ID = staff.ID,
                            DISPATCH_START = input.startTime,
                            DISPATCH_STOP = input.endTime
                        };
                        db.DISPATCHER.Add(dispatcher);
                    }
                    await db.SaveChangesAsync();
                    trans.Commit();
                    NotificationController.NotificationCallbackMsg("新增员工" + input.name + " 编号" + staff.ID);
                    return Ok(new staffAddDto
                    {
                        data = getDtoList(),
                        info1 = "ok",
                        info2 = staff.ACCOUNT_ID
                    });
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return Ok(returnHelper.fail());
                }
            }
        }

        /// <summary>
        /// modify password
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/user/modifyPassword")]
        public async Task<IHttpActionResult> modifyPassword(passwordModifyDto input)
        {
            string id = input.id.Substring(2);
            var person = await db.STAFF.FindAsync(input.id);
            person.PASSWORD = input.newPassword;
            await db.SaveChangesAsync();
            NotificationController.NotificationCallbackMsg(person.NAME + " 修改密码");
            return Ok(returnHelper.make(""));
        }
    }
}