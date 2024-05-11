using Speaker.leison.Business_layer.Interface;
using Speaker.leison.Database_Layer.Interfaces;
using Speaker.leison.Database_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using DDL.Database_Layer.Entities;

namespace Speaker.leison.Business_layer.Services
{
    public class AllInOneService : IAllInOne
    {
        private readonly IDesclambler desclambler;
        private readonly IEmr60Info emr60Info;
        private readonly IRecieverInterface recieverInterface;

        public AllInOneService()
        {
            desclambler = new DesclamblerRepository();
            emr60Info = new Emr60InfoService();
            recieverInterface = new RecieverRepository();
        }
        public Desclambler GetDesclamblerInfoByChanellId(int id)
        {
            var res=desclambler.GetDesclamblerInfoById(id);
            return res;
        }

        public Emr60Info GEtInfoByCHanellName(string Name)
        {
            var res = emr60Info.GetEmrInfoByCHanellName(Name);
            return res;
        }

        public Reciever GetRecieverInfoByChanellId(int id)
        {
            var res = recieverInterface.GetRecieverInfoById(id);
            return res;
        }

        public string  GetPort(string Name)
        {
             HttpClient client = new HttpClient();
            string username = "dima";
            string password = "dima123";
            string credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            HttpResponseMessage response =  client.GetAsync("http://192.168.20.60/en/muxset.asp").Result;
            var lis = new List<string>();
            if (response.IsSuccessStatusCode)
            {
                string responseBody =  response.Content.ReadAsStringAsync().Result;
                lis.AddRange(responseBody.Split(new char[] { '\n' }).Select(io => io.ToLower()));
                var res = lis.Where(io => io.Contains(Name.ToLower()) && io.Contains("card7->phy1")).FirstOrDefault();

                string portInfo = Regex.Match(res, @"port(\d+)").Groups[1].Value;
                return portInfo;
            }
            else
            {
                Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                return null;
            }
        }
    }
}
