using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyClient
{
    internal class Exo1
    {
        readonly HttpClient client = new HttpClient();
        public Dictionary<string, int> servers { get; set; }

        public Exo1()
        {
            servers = new Dictionary<string, int>();
            client.Timeout = TimeSpan.FromSeconds(5);
        }

        public string GetResult()
        {
            callURL();
            string res = "<div class=\"content\"><div class=\"titre\"> Analyse des serveurs </div>";
            string tab = "<table class=\"tablePopulatiry\"><tr>" +
                "<th>Serveurs</th>" +
                "<th>Nombre d'utilisations</th>" +
                "<th>Poucentage d'utilisations</th>" +
                "</tr>";
            var dicoTrie = servers.OrderByDescending(x => x.Value);
            servers = dicoTrie.ToDictionary(x => x.Key, x => x.Value);
            double nbTotal = 0;
            foreach (string server in servers.Keys)
            {
                nbTotal += servers[server];
            }

            for (int i = 0; i < servers.Count && i < 4; i++)
            {
                var element = servers.ElementAt(i);
                tab += "<tr>" +
                    "<td>" + element.Key + "</td>" +
                    "<td>" + element.Value.ToString("F2") + "</td>" +
                    "<td>" + (element.Value / nbTotal * 100).ToString("F2") + "%</td>" +
                    "</tr>";

            }
            tab += "</table>";
            return res + tab + "</div >";
        }

        private string callURL()
        {
            String url;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("../../urls/urls1.txt");
                //Read the first line of text
                url = sr.ReadLine();
                //Continue to read until you reach end of file
                while (url != null)
                {
                    //write the line to console window
                    Console.WriteLine(url);
                    //Read the next line
                    url = sr.ReadLine();

                    HttpResponseMessage responseServer = client.GetAsync(url).Result;
                    try
                    {
                        if(responseServer.Headers.TryGetValues("Server", out IEnumerable<string> serverValues))
            {
                            string server = serverValues.FirstOrDefault();
                            if (servers.ContainsKey(server))
                            {
                                servers[server] += 1;
                            }
                            else
                            {
                                servers.Add(server, 1);
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return "";
        }
    }
}
