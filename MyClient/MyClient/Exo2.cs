using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace MyClient
{
    internal class Exo2
    {
        readonly HttpClient client = new HttpClient();
        public List<int> ages { get; set; }

        public Exo2()
        {
            ages = new List<int>();
            client.Timeout = TimeSpan.FromSeconds(5);
        }

        public string GetResult()
        {
            callURL();
            string res = "<div class=\"content\"><div class=\"titre\"> Age moyen des pages Wikipedia </div> ";
            double average = ages.Count > 0 ? ages.Average() : 0 ;
            double ecartType = 0;
            double sum = 0;
            for (int i = 0; i < ages.Count; i++)
            {
                sum += Math.Pow((ages[i] - average), 2);
            }
            ecartType = Math.Sqrt(sum / ages.Count);

            string tab = "<table class=\"tablePopulatiry\"><tr>" +
               "<th>Nombre de sites</th>" +
               "<th>Moyenne (en heure)</th>" +
               "<th>Ecart Type  (en heure)</th>" +
               "</tr>";

            tab += "<tr>" +
                "<td>" + ages.Count + "</td>" +
                "<td>" + average.ToString("F2") + "</td>" +
                "<td>" + ecartType.ToString("F2") + "</td>" +
                "</tr>";


            tab += "</table>";
            return res + tab + "</div >";
        }

        private string callURL()
        {
            String url;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("../../urls/urls2.txt");
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
                        int age = -1;
                        if (responseServer.Content.Headers.LastModified.HasValue)
                        {
                            DateTimeOffset now = DateTimeOffset.Now;
                            DateTimeOffset page = responseServer.Content.Headers.LastModified.Value;
                            age = (int)((now - page).TotalHours);
                            ages.Add(age);
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
