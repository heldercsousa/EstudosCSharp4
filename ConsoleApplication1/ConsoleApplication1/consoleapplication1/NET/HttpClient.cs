using EstudosCSharp.NET.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EstudosCSharp.NET
{
    /// <summary>
    /// HttpClient was introduced in .NET Framework 4.5. For developers using .NET 4.5 or later, it is the preferred way to 
    /// consume HTTP requests unless you have a specific reason not to use it. In essence, HttpClient combines the flexibility 
    /// of HttpWebRequest and the simplicity of WebClient, giving you the best of both the worlds.
    /// </summary>
    public static class HttpClient
    {
        public static void RequestCatsData()
        {
            var client = new System.Net.Http.HttpClient();
            HttpResponseMessage response = client.GetAsync(WebRequest.uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var str = response.Content.ReadAsStringAsync().Result;
                var seri = new JavaScriptSerializer();
                var data = seri.Deserialize<AllData>(str);

                var print = WebRequest.PrepareForPrint(data);
                System.Console.WriteLine("Cats with HttpClient");
                System.Console.WriteLine(string.Join(Environment.NewLine, print.Take(5)));
            }
        }
    }
}
