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

namespace DBCourseDesign.Controllers
{
    public class NotificationController : ApiController
    {
        public static ConcurrentBag<StreamWriter> clients;

        static NotificationController()
        {
            clients = new ConcurrentBag<StreamWriter>();
        }
        
        public static async Task NotificationCallbackMsg(string s)
        {
            foreach (var client in clients)
            {
                try
                {
                    var data = s;
                    await client.WriteAsync(data);
                    await client.FlushAsync();
                    client.Dispose();
                }
                catch (Exception)
                {
                    StreamWriter ignore;
                    clients.TryTake(out ignore);
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
            clients.Add(client);
        }

    }
}