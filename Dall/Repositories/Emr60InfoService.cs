using DDL.Database_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Speaker.leison.Database_Layer.Interfaces;
using Speaker.leison.Kontext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Database_Layer.Repositories
{
    public class Emr60InfoService : BaseRepository, IEmr60Info
    {
        private readonly DbSet<Emr60Info> _dbSet;
        public Emr60InfoService(d db):base(db)
        {
                _dbSet=database.Set<Emr60Info>();
        }

        public int GetEmrCodeByName(string port)
        {
            var res = _dbSet.FirstOrDefault(io => io.Port == port);
            return res.SourceEmr;
        }

        public Emr60Info GetEmrInfoByCHanellName(string Port)
        {
            var res = _dbSet.FirstOrDefault(io => io.Port == Port);
            return res;
        }
    }
}
