using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Natia_Jandagishvili_Http.Services
{
    public class MerService
    {

        public bool checkMer(int card, int port)
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


            //    var content = await res.Content.ReadAsStringAsync();


            //    var splited = content.Split(new string[] { "<*2*>", "<*1*>", "/html", "html/", "html" }, StringSplitOptions.None);
            //    //6  //7
            //    Console.WriteLine(splited.Length);
            //    if (splited.Length > 7)
            //    {

            //        var pirveli = splited[6];
            //        pirveli = pirveli.Replace("Mbps", "");

            //        if (float.TryParse(pirveli, out float speed))
            //        {
            //            var sec = splited[7];
            //            sec = sec.Replace("Mbps", "");
            //            var re = float.Parse(pirveli);
            //            var sc = float.Parse(sec);
            //            if (re >= 1.1 && sc <= 0.2)
            //            {
            //                Console.WriteLine($"{re},{sc}");
            //                return true;
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Failed to parse speed.");
            //        }
            //    }
            //}
            return true;
        }
    }
}
