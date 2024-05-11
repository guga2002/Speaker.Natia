
using BuisnessLayer.Interface;
using BuisnessLayer.Models;
using DatabaseOperations.Interfaces;
using DDL.Database_Layer.Entities;
using Interfaces;
using Repositories;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class ChanellServices : Ichanell
    {
        private readonly IChanellRepository chan;

        public ChanellServices()
        {
            this.chan = new ChanellRepository();
        }
        public void Add(ChanellModel item)
        {
             chan.Add(new Chanell()
            {
                FromOptic = item.FromOptic,
                ChanellFormat = item.ChanellFormat,
                Name = item.Name,
                PortIn250 = item.PortIn250
                ,NameForSPeak=item.NameForSpeake
            });
        }

        public Chanell GetByID(int id)
        {
            return chan.GetByID(id);
        }

        public ChanellModel GetChanellByPort(int port)
        {
            var res= chan.GetChanellByPort(port);
            if(res!=null)
            {
                return new ChanellModel()
                {
                    Id = res.Id,
                    ChanellFormat = res.ChanellFormat,
                    FromOptic = res.FromOptic,
                    Name = res.Name,
                    PortIn250 = res.PortIn250,
                    NameForSpeake=res.NameForSPeak,
                };
            }
            return new ChanellModel();
        }

        public void Remove(int id)
        {
           chan.Remove(id);
        }

        public void View(int id)
        {
           chan.View(id);
        }
    }
}
