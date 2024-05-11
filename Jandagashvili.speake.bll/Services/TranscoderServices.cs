using BuisnessLayer.Interface;
using BuisnessLayer.Models;
using DatabaseOperations.Interfaces;
using DDL.Database_Layer.Entities;
using Repositories;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class TranscoderServices : ITranscoder
    {
        private readonly ITranscoderRepository repos;
        public TranscoderServices()
        {
            this.repos = new TranscoderReporitory();
        }
        public void Add(TranscoderModel item)
        {
             repos.Add(new Transcoder()
            {
                Card=item.Card,
                ChanellId=item.ChanellId,
                Port=item.Port,
            });
        }

        public int GetChanellIdBycardandport(int card, int port)
        {
           return  repos.GetChanellIdBycardandport(card, port);
        }

        public TranscoderModel GetTranscoderInfoByCHanellId(int id)
        {
            var res =  repos.GetTranscoderInfoByCHanellId(id);
            if (res != null)
            {
                return new TranscoderModel()
                {
                    Card=res.Card,
                    ChanellId=res.ChanellId,
                    Port=res.Port,
                    Emr_Number=res.EmrNumber,
                };
            }
            return new TranscoderModel();
        }

        public void Remove(int id)
        {
             repos.Remove(id);
        }

        public void View(int id)
        {
             repos.Remove(id);
        }
    }
}
