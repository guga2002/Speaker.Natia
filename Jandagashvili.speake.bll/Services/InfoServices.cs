using BuisnessLayer.Interface;
using BuisnessLayer.Models;
using DatabaseOperations.Interfaces;
using DDL.Database_Layer.Entities;
using Repositories;
using System.Threading.Tasks;


namespace BuisnessLayer.Services
{
    public class InfoServices : IInfo
    {
        private readonly IInfoRepository repos;

        public InfoServices()
        {
            this.repos = new InfoRepository();  
        }
        public void Add(InfoModel item)
        {
             repos.Add(new Info()
            {
                AlarmMessage = item.AlarmMessage,
                CHanellId = item.CHanellId,
            });
        }

        public InfoModel GeTInfoByCHanellID(int id)
        {
            var res =  repos.GeTInfoByCHanellID(id);
            if (res != null)
            {
                return new InfoModel()
                {
                    AlarmMessage=res.AlarmMessage,
                    CHanellId=res.CHanellId,
                };
            }
            return new InfoModel();
        }

        public void Remove(int id)
        {
          repos.Remove(id);
        }

        public void View(int id)
        {
            repos.View(id);
        }
    }
}
