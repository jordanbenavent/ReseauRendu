using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace MyClient
{
    internal class Program
    {
        readonly HttpClient client = new HttpClient();

        MyStats stats = new MyStats();

        List<String> servers = new List<string>();

        List<String> siteWiki = new List<string>();
        static void Main(string[] args)
        {
            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }


            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            Program program = new Program();
            program.client.Timeout = TimeSpan.FromSeconds(5);
            program.listening(listener);

            Console.ReadLine();
        }

        private void listening(HttpListener listener)
        {
            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // get url 
                Console.WriteLine($"Received request for {request.Url}");


                if(request.Url.ToString() == "http://localhost:8080/age")
                {
                    Exo2 exo2 = new Exo2();
                    string res = exo2.GetResult();
                    //
                    // Obtain a response object.
                    string css1 = System.IO.File.ReadAllText("../../lauch/index.css");
                    HttpListenerResponse response = context.Response;
                    string css = System.IO.File.ReadAllText("../../popularity/tablePopularity.css");
                    // Construct a response.
                    string responseString = $"<HTML><style>{css1 + css}</style><BODY>" + res + "</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                else if (request.Url.ToString() == "http://localhost:8080/stats")
                {
                    Exo1 exo1 = new Exo1();

                    string res = exo1.GetResult();
                    //res += stats.ServerStatsAge();
                    //
                    Console.WriteLine(documentContents);

                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    string css1 = System.IO.File.ReadAllText("../../lauch/index.css");
                    string css = System.IO.File.ReadAllText("../../popularity/tablePopularity.css");
                    // Construct a response.
                    string responseString = $"<HTML><style>{css1 + css}</style><BODY>" + res + "</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                else if (request.Url.ToString() == "http://localhost:8080/scenario")
                {
                    Exo3 s = new Exo3();

                    string res = s.GetResult();
                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;
                    string css1 = System.IO.File.ReadAllText("../../lauch/index.css");
                    string css = System.IO.File.ReadAllText("../../popularity/tablePopularity.css");
                    // Construct a response.
                    string responseString = $"<HTML><style>{css1 + css}</style><BODY>" + res + "</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                } else
                {
                    HttpListenerResponse response = context.Response;
                    // Lecture du fichier HTML
                    string html = System.IO.File.ReadAllText("../../lauch/index.html");
                    // Lecture du fichier CSS
                    string css = System.IO.File.ReadAllText("../../lauch/index.css");
                    // Lecture du fichier JavaScript
                    string js = System.IO.File.ReadAllText("../../lauch/index.js");
                    // Combinaison du HTML, du CSS et du JavaScript
                    string responseHtml = $"<html><head><style>{css}</style></head><body>{html}<script>{js}</script></body></html>";
                    // Conversion de la réponse en tableau de bytes
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseHtml);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }
                
            }
        }
    }
}
