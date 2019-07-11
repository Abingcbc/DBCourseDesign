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
        
        
        public static void NotificationCallbackMsg(string operation, string description)
        {
            foreach (var client in clients)
            {
                try
                {
                    var a = JsonConvert.SerializeObject(returnHelper.make(new NotificationDto(operation, description)));
                    client.WriteLine(JsonConvert.SerializeObject(returnHelper.make(new NotificationDto(operation, description))));
                    client.Flush();
                    client.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }

        [HttpGet]
        public HttpResponseMessage Subscribe(HttpRequestMessage request)
        {
            var response = request.CreateResponse();
            response.Content = new PushStreamContent((a, b, c) =>
            {
                OnStreamAvailable(a, b, c);
            }, "text/event-stream");
            return response;
        }

        private void OnStreamAvailable (Stream stream, HttpContent content, TransportContext context)
        {
            var client = new StreamWriter(stream);
            clients.Enqueue(client);
        }

    }
}