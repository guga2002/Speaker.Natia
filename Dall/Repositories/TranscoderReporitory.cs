
using DatabaseOperations.Interfaces;
using DDL.Database_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Speaker.leison.Kontext;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class TranscoderReporitory : BaseRepository, ITranscoderRepository
    {
        private readonly DbSet<Transcoder> Transcoder;
        public TranscoderReporitory(d db) : base(db)
        {
            this.Transcoder = database.Set<Transcoder>();
        }

        public void Add(Transcoder item)
        {
            if (!Transcoder.Any(io => io.ChanellId == item.ChanellId))
            {
                Transcoder.Add(item);
                 database.SaveChanges();
            }
        }

        public Transcoder GetTranscoderInfoByCHanellId(int id)
        {
            if (Transcoder.Any(io => io.ChanellId == id))
            {
                var res =  Transcoder.Where(io => io.ChanellId == id).FirstOrDefault();
                return res;
            }
            System.Console.WriteLine(  "transkoderi araa gansazggbruli");
            return null;
        }

        public int GetChanellIdBycardandport(int card,int port)
        {
           var res= Transcoder.Where(io=>io.Card==card&&io.Port==port).FirstOrDefault();
            if(res!=null)
            {
                return res.ChanellId;
            }
            return -1;
        }
        public void Remove(int id)
        {
            var res = Transcoder.Where(io => io.ChanellId == id).FirstOrDefault();
            if (res != null)
            {
                Transcoder.Remove(res);
                database.SaveChanges();
            }
        }

        public void View(int id)
        {
            var res =  Transcoder.Where(io => io.ChanellId == id).FirstOrDefault();

            if (res != null)
            {
               // MessageBox.Show(res.ToString());
            }
        }
    }
}
