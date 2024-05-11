using Natia_Jandagishvili_Http.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Natia_Jandagishvili_Http
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "http://192.168.20.200/goform/formEMR30?type=5&cmd=1&language=0&slotNo=2&portNo=3&ran=0.284873454";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string responseData = reader.ReadToEnd();
                    Console.WriteLine(responseData);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.StackTrace}");
            }
        }
    }
}
