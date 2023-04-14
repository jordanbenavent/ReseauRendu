using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyClient
{
    internal class MyStats
    {
        public Dictionary<string, int> servers { get; set; }
        public Dictionary<string, List<int>> date { get; set; }
        public List<int> age { get; set; }

        public MyStats()
        {
            servers = new Dictionary<string, int>();
            date = new Dictionary<string, List<int>>();
            age = new List<int>();
        }

        public void nextCall(Header header)
        {
            statsServer(header);
            statsAgeServer(header);
            showBestServer();
        }

        public void statsServer(Header header)
        {
            if (header.server == null) return;
            if (servers.ContainsKey(header.server))
            {
                servers[header.server] += 1;
            }
            else
            {
                servers.Add(header.server, 1);
            }
        }

        public void statsAgeServer(Header header)
        {
            if(header.server == null) return;   
            if (header.age == -1) return;
            if (date.ContainsKey(header.server))
            {
                date[header.server].Add(header.age);
            }
            else
            {
                date.Add(header.server, new List<int>());
                date[header.server].Add(header.age);
            }
            age.Add(header.age);
        }

        public void showBestServer()
        {
            int max = 0;
            string caller = "";
            foreach (KeyValuePair<string, int> pair in servers)
            {
                if (pair.Value > max)
                {
                    max = pair.Value;
                    caller = pair.Key;
                }
            }
            Console.WriteLine(caller + " is the best server ! ");
        }

        public string ServerStatsPopularity()
        {
            string res = "<div class=\"content\">";
            var dicoTrie = servers.OrderByDescending(x => x.Value);
            servers = dicoTrie.ToDictionary(x => x.Key, x => x.Value);
            double nbTotal = 0;
            foreach (string server in servers.Keys)
            {
                nbTotal += servers[server];
                //res += "<div>Le server " + server + " est utlise " + servers[server] + " fois.\n </div>";
            }

            string tab = "<table class=\"tablePopulatiry\"><tr>" +
                "<th>Serveurs</th>" +
                "<th>Nombres d'utilsations</th>" +
                "<th>Poucentage d'utilisation</th>" +
                "</tr>";
            for (int i = 0; i < servers.Count && i<4; i++)
            {
                var element = servers.ElementAt(i);
                tab += "<tr>" +
                    "<td>"+element.Key+"</td>" +
                    "<td>"+element.Value.ToString("F2") + "</td>" +
                    "<td>"+(element.Value/nbTotal *100).ToString("F2") + "%</td>" +
                    "</tr>";

            }
            tab += "</table>";
            return res + tab + "</div >";
        }

        public string ServerStatsAge()
        {
            string res = "<div class=\"content\"> <div class=\"titre\">Les stats de wikipedia </div> ";
            DateTimeOffset now = DateTimeOffset.Now;
            /*

            foreach (string server in date.Keys)
            {
                int sum = 0;
                int nb = 0;
                foreach (int age in date[server])
                {

                    sum += age;
                    nb++;
                }
                res += "<div>" + server + " age moyen : " + sum / nb + "</div>";
            }
            */
            double average = age.Average();
            double ecartType = 0;
            double sum = 0;
            for(int i = 0; i<age.Count; i++)
            {
                sum += Math.Pow((age[i] - average),2);
            }
            ecartType = Math.Sqrt(sum / age.Count);

            string tab = "<table class=\"tablePopulatiry\"><tr>" +
               "<th>Nombre de sites</th>" +
               "<th>Moyenne</th>" +
               "<th>Ecart Type</th>" +
               "</tr>";

            tab += "<tr>" +
                "<td>" + age.Count + "</td>" +
                "<td>" + average.ToString("F2") + "</td>" +
                "<td>" + ecartType.ToString("F2") + "</td>" +
                "</tr>";


            tab += "</table>";
            return res + tab + "</div >";
        }
    }
}
