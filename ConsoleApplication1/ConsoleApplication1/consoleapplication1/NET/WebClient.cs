using EstudosCSharp.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace EstudosCSharp.NET
{
    /// <summary>
    /// The System.Net.WebClient class in .NET provides a high-level abstraction on top of HttpWebRequest. WebClient is just a wrapper around HttpWebRequest, 
    /// so uses HttpWebRequest internally. Thus WebClient is a bit slow compared to HttpWebRequest, but requires you to write much less code. You can use WebClient for 
    /// simple ways to connect to and work with HTTP services. It is generally a better choice than HttpWebRequest unless you need to leverage the additional features 
    /// that HttpWebRequest provides. 
    /// </summary>
    public static class WebClient
    {
        public static void RequestCatsData()
        {
            string data = null;
            using (var webClient = new System.Net.WebClient())
            {
                data = webClient.DownloadString(WebRequest.uri);
                var serializer = new JavaScriptSerializer();
                var lst = serializer.Deserialize<AllData>(data);

                IEnumerable<string> print = WebRequest.PrepareForPrint(lst);
                System.Console.WriteLine("Cats with WebClient");
                System.Console.WriteLine(string.Join(Environment.NewLine, print.Take(5)));
            }
        }
    }
}
