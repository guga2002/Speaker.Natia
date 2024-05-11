using DDL.Database_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Business_layer.Interface
{
    public interface IAllInOne
    {
        Emr60Info GEtInfoByCHanellName(string Name);
        Desclambler GetDesclamblerInfoByChanellId(int id);
        Reciever GetRecieverInfoByChanellId(int id);
        string GetPort(string Name);
    }
}
