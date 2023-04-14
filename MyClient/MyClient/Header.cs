using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyClient
{
    internal class Header
    {
        public string server { get; set; }
        public int age { get; set; }

        public Header(HttpResponseMessage response)
        {
            server = null;
            age = -1 ;
            if (response.Content.Headers.LastModified.HasValue)
            {
                DateTimeOffset now = DateTimeOffset.Now;
                DateTimeOffset page = response.Content.Headers.LastModified.Value;
                age = (int)((now - page).TotalHours);
                Console.WriteLine(age);
            }
            //Console.WriteLine(response.Headers);
            if (response.Headers.TryGetValues("Server", out IEnumerable<string> serverValues))
            {
                server = serverValues.FirstOrDefault();
            }
            Console.WriteLine(server);
        }
    }
}
