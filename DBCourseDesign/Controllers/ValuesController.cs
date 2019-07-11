using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;

namespace DBCourseDesign.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        //[RequireHttps]
        public IEnumerable<string> Post()
        {
            NotificationController.NotificationCallbackMsg("操作名", "描述");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[RequireHttps]
        public string Get(int id)
        {
            return "value";
        }
        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
