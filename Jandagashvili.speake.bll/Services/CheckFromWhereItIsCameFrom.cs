using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jandagashvili.speake.bll.Services
{
    public class CheckFromWhereItIsCameFrom
    {

        public int check(string emr,string chanellName)
        {

            HttpClient client = new HttpClient();
            string username = "dima";
            string password = "dima123";
            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            HttpResponseMessage response = client.GetAsync($"http://192.168.20.{emr}/mux/mux_config_en.asp").Result;
            var lis = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                lis.AddRange(responseBody.Split(new char[] { '\n' }).Select(io => io.ToLower()));
                var res = lis.Where(io => io.Contains(chanellName.ToLower()) && io.Contains("card7->phy1")).FirstOrDefault();

                string portInfo = Regex.Match(res, @"port(\d+)").Groups[1].Value;
               return int.Parse(portInfo);
            }
            else
            {
                Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                return -1;
            }
        }
    }
}
