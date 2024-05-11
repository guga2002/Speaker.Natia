
using DDL.Database_Layer.Entities;
using Interfaces;
using System.Threading.Tasks;

namespace DatabaseOperations.Interfaces
{
    public interface IInfoRepository:BaseInterface<Info>
    {
        Info GeTInfoByCHanellID(int id);
    }
}
