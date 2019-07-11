using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using Newtonsoft.Json;
using DBCourseDesign.Models;
using DBCourseDesign.Models.DTO;

namespace DBCourseDesign.Controllers
{
    public class NotificationController : ApiController
    {
        public static ConcurrentQueue<StreamWriter> clients = new ConcurrentQueue<StreamWriter>();
        
        
        public static void NotificationCallbackMsg(string operation, string description = " ")
        {
            WebApiConfig.log.Info(operation + " " + description);
        }

    }
}