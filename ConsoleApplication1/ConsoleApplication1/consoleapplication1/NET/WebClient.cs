using EstudosCSharp.NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
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

        public static void DownloadFile()
        {
            string url = @"https://getbootstrap.com/docs/5.0/assets/img/favicons/favicon-32x32.png";
            using (var wc = new System.Net.WebClient())
            {
                //var localName = @"favicon-32x32.png";
                var localName = string.Format(@"favicon-32x32_{0:yyyyMMdd_hhmmss}.png", DateTime.Now);
                wc.DownloadFile(url, localName);
            }
        }

        public static void DownloadFileAsync()
        {
            string url = @"https://getbootstrap.com/docs/5.0/assets/img/favicons/favicon-32x32.png";
            using (var wc = new System.Net.WebClient())
            {
                var localName = string.Format(@"favicon-32x32_{0:yyyyMMdd_hhmmss}.png", DateTime.Now);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(Wc_DownloadFileCompleted);
                wc.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
                wc.DownloadFileAsync(new Uri(url), localName);
            }
        }

        private static void Wc_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            Console.Write(".");
        }

        private static void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("async file download completed");
        }

        public static void DownloadFileData()
        {
            string url = @"https://getbootstrap.com/docs/5.0/assets/img/favicons/favicon-32x32.png";
            using (var wc = new System.Net.WebClient())
            {
                var localName = string.Format(@"favicon-32x32_{0:yyyyMMdd_hhmmss}.png", DateTime.Now);
                var fileBytes = wc.DownloadData(new Uri(url));
                //File.WriteAllBytes(localName, fileBytes);
                FileStream fs = new FileStream(localName, FileMode.Create);
                fs.Write(fileBytes, 0, fileBytes.Length);
                fs.Flush();
                fs.Close();
            }
        }


        //public static void UploadValuesAsync()
        //{
        //    using (var wc = new System.Net.WebClient())
        //    {
        //        wc.Up
        //    }
        //}
    }
}
