using BuisnessLayer.Models;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface ITranscoder : Icrud<TranscoderModel>
    {
        TranscoderModel GetTranscoderInfoByCHanellId(int id);
        int GetChanellIdBycardandport(int card, int port);
    }
}
