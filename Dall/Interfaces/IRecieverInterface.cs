using DDL.Database_Layer.Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Database_Layer.Interfaces
{
    public interface IRecieverInterface:BaseInterface<Reciever>
    {
        Reciever GetRecieverInfoById(int id);
        Chanell GetChanellIdBycardandport(int card, int port);
    }
}
