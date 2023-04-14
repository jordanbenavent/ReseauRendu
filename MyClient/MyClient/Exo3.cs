using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyClient
{
    internal class Exo3
    {
        readonly HttpClient client = new HttpClient();
        public List<int> contentLength { get; set; }
        public Dictionary<string, int> encoding { get; set; }

        public List<double> responseTime { get; set; }

        public Exo3()
        {
            contentLength = new List<int>();
            encoding = new Dictionary<string, int>();
            responseTime = new List<double>(); 
            client.Timeout = TimeSpan.FromSeconds(5);
        }

        public string GetResult()
        {
            callURL();
            string res = "<div class=\"content\"> <div class=\"titre\"> Analyse des Headers </div>";
            
            string tab = "<table class=\"tablePopulatiry\"><tr>" +
                "<th>Nombre de page</th>" +
                "<th>Moyenne de la taille de page (nombre de caracteres)</th>" +
                "<th>Content Encoding le plus utilise</th>" +
                 "<th>Temps de reponse moyen (en ms)</th>" +
                "</tr>";

            var size = contentLength.Count;

            tab += "<tr>" +
                "<td>" +size + "</td>" +
                "<td>" + contentLength.Average().ToString("F2") + "</td>" +
                "<td>" + findTheEncoding() + "</td>" +
                "<td>" + averageReponseTime() + "</td>" +
                "</tr>";

            
            tab += "</table>";
            return res + tab + "</div >";
        }

        private string findTheEncoding()
        {
            var dicoTrie = encoding.OrderByDescending(x => x.Value);
            encoding = dicoTrie.ToDictionary(x => x.Key, x => x.Value);
            return encoding.Count > 0 ? encoding.ElementAt(0).Key : "Aucun Encoding trouver lors des appels";
        }

        private string averageReponseTime()
        {
            return responseTime.Count > 0 ? responseTime.Average().ToString() : "Aucun temps de réponse";
        }

        private string callURL()
        {
            String url;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("../../urls/urls3.txt");
                //Read the first line of text
                url = sr.ReadLine();
                //Continue to read until you reach end of file
                while (url != null)
                {
                    //write the line to console window
                    Console.WriteLine(url);
                    //Read the next line
                    url = sr.ReadLine();
                    
                    try
                    {
                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        HttpResponseMessage responseServer = client.GetAsync(url).Result;
                        stopwatch.Stop();
                        responseTime.Add(stopwatch.ElapsedMilliseconds);
                        var l  = responseServer.Content.ReadAsStringAsync().Result.Length;
                        contentLength.Add(l);

                        if (responseServer.Headers.TryGetValues("Content-Encoding", out var encodingValues))
                        {
                            foreach (var encodingValue in encodingValues)
                            {
                                if (encoding.ContainsKey(encodingValue))
                                {
                                    encoding[encodingValue] += 1;
                                } else
                                {
                                    encoding.Add(encodingValue, 1);
                                }
                            }
                        }

                    } catch (Exception e)
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
