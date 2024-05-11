using DDL.Database_Layer.Entities;
using Repositories;
using Speaker.leison.Database_Layer.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace Speaker.leison.Database_Layer.Repositories
{
    public class RecieverRepository : BaseRepository, IRecieverInterface
    {
        private readonly DbSet<Reciever> _recievers;
        public RecieverRepository():base()
        {
                _recievers=database.Set<Reciever>();
        }
        public void Add(DDL.Database_Layer.Entities.Reciever item)
        {
            if (!_recievers.Any(io => io.ChanellId == item.ChanellId))
            {
                _recievers.Add(item);
                database.SaveChanges();
            }
        }

        public Chanell GetChanellIdBycardandport(int card, int port)
        {
           return _recievers.FirstOrDefault(io=>io.Card==card&&io.Port==port).Chanell;
        }

        public DDL.Database_Layer.Entities.Reciever GetRecieverInfoById(int id)
        {
            return _recievers.FirstOrDefault(io=>io.ChanellId==id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void View(int id)
        {
            Console.WriteLine(_recievers.FirstOrDefault(io => io.Id == id));
        }
    }
}
