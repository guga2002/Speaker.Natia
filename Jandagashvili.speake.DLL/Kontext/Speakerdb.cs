using DDL.Database_Layer.Entities;
using Jandagashvili.speake.DLL.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace Jandagashvili.speake.DLL.Kontext
{
    public class Speakerdb : DbContext
    {
        public Speakerdb()
            : base("data source=DESKTOP-UQHSPGM;initial catalog=JandagBase;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;")
        {
        }
        public virtual DbSet<Chanell> Chanells { get; set; }

        public virtual DbSet<Info> Infos { get; set; }

        public virtual DbSet<Transcoder> Transcoders { get; set; }

        public virtual DbSet<Desclambler> Desclamblers { get; set; }

        public virtual DbSet<Emr60Info> Emr60Info { get; set; }

        public virtual DbSet<Reciever> Reciever { get; set; }
        public virtual DbSet<Emr100Info> Emr100Infos  { get; set; }
        public virtual DbSet<Emr110info> Emr110Infos { get; set; }
        public virtual DbSet<Emr120Info> Emr120Infos { get; set; }
        public virtual DbSet<Emr130Info> Emr130Infos { get; set; }
        public virtual DbSet<Emr200Info> Emr200Infos { get; set; }

    }
}