
using DDL.Database_Layer.Entities;
using Interfaces;
using Repositories;
using Speaker.leison.Database_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Database_Layer.Repositories
{
    public class DesclamblerRepository : BaseRepository, IDesclambler
    {
        private readonly DbSet<Desclambler> desclamblers;
        public DesclamblerRepository():base()
        {
            desclamblers = database.Set<Desclambler>(); 
        }

        public void Add(Desclambler item)
        {
            if (!desclamblers.Any(io => io.ChanellId == item.ChanellId))
            {
                desclamblers.Add(item);
                database.SaveChanges();
            }
        }

        public Desclambler GetDesclamblerInfoById(int id)
        {
           return desclamblers.FirstOrDefault(io=>io.ChanellId==id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void View(int id)
        {
            throw new NotImplementedException();
        }
    }
}
