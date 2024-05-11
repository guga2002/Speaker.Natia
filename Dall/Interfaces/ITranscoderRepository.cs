using DDL.Database_Layer.Entities;
using Interfaces;
using System.Threading.Tasks;

namespace DatabaseOperations.Interfaces
{
    public interface ITranscoderRepository : BaseInterface<Transcoder>
    {
        Transcoder GetTranscoderInfoByCHanellId(int id);
        int GetChanellIdBycardandport(int card, int port);
    }
}
