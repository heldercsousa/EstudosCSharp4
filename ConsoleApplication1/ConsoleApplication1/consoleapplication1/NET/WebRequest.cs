using EstudosCSharp.NET.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Web.Script.Serialization;

namespace EstudosCSharp.NET
{
    /// <summary>
    /// It´s the classic way .NET provides to make web requests
    /// </summary>
    public static class WebRequest
    {
        public static readonly string uri = "https://cat-fact.herokuapp.com/facts/";
        /// <summary>
        /// The System.Net.WebRequest class is an abstract class. Thus you will need to create a HttpWebRequest or FileWebRequest to consume HTTP requests using this class.
        /// Was the first class provided in the .NET Framework to consume HTTP requests. It gives you a lot of flexibility in handling each and every aspect of the request and response objects, 
        /// without blocking the user interface thread. You can use this class to access and work with headers, cookies, protocols, and timeouts when working with HTTP.
        /// </summary>
        public static void RequestCatsDataWebRequest()
        {
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(uri);
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.Method = "GET";
            System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            var stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string data = streamReader.ReadToEnd();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var lst = serializer.Deserialize<AllData>(data);
            IEnumerable<string> print = PrepareForPrint(lst);
            System.Console.WriteLine("Cats with WebRequest");
            System.Console.WriteLine(string.Join(Environment.NewLine, print.Take(5)));
        }

        public static IEnumerable<string> PrepareForPrint(AllData lst)
        {
            return lst.All
                   .Select((x, y) => $"{y.ToString().PadLeft(3, ' ')}-{x.Type}-{x.Upvotes.ToString().PadLeft(3, '0')}-{x.Text}")
                   .Select(x => x.Substring(0, Clamp<int>(x.Length,0,80)));
        }

        /// <summary>
        /// using HttpWebRequest 
        /// </summary>
        public static void RequestCatsDataHttpWebRequest()
        {
            System.Net.HttpWebRequest http = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            System.Net.WebResponse response = http.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string data = streamReader.ReadToEnd();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var lst = serializer.Deserialize<AllData>(data);
            var print = PrepareForPrint(lst);
            System.Console.WriteLine("Cats with HttpWebRequest");
            System.Console.WriteLine(string.Join(Environment.NewLine,print.Take(5)));
        }

        /// <summary>
        /// using HttpWebRequest with streamWriter
        /// </summary>
        public static void RequestCatsDataHttpWebRequestAndWritesFile()
        {
            System.Net.HttpWebRequest http = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            System.Net.WebResponse response = http.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string data = streamReader.ReadToEnd();
            
            //var x = File.Create("CatsData.txt", data.Length, FileOptions.WriteThrough);
            StreamWriter writer = new StreamWriter("CatsData.txt");
            writer.Write(data);
            writer.Close();
            System.Console.WriteLine("Cats with HttpWebRequest and StreamWriter. File was wroten:");
            System.Console.WriteLine(string.Join(Environment.NewLine, File.ReadAllLines("CatsData.txt", Encoding.UTF8).Take(20)));
        }

        public static T Clamp<T>(T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            if (value.CompareTo(max) > 0)
                return max;

            return value;
        }
    }
}
