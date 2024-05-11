using DDL.Database_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Speaker.leison.Kontext
{
    public class d : DbContext
    {
        public d(DbContextOptions<d> db):base(db)
        {
        }

        public virtual DbSet<Chanell> Chanells { get; set; }

        public virtual DbSet<Info> Infos { get; set; }

        public virtual DbSet<Transcoder> Transcoders { get; set; }

        public virtual DbSet<Desclambler> Desclamblers { get; set; }

        public virtual DbSet<Emr60Info> Emr60Info { get; set; }

        public virtual DbSet<Reciever> Reciever { get; set; }
    }
}