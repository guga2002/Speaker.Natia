using BuisnessLayer.Models;
using DDL.Database_Layer.Entities;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface Ichanell:Icrud<ChanellModel>
    {
        ChanellModel GetChanellByPort(int port);
        Chanell GetByID(int id);
    }
}
