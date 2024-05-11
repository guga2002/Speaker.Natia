using DDL.Database_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.Database_Layer.Interfaces
{
    public interface IEmr60Info
    {
         int GetEmrCodeByName(string Port);
         Emr60Info GetEmrInfoByCHanellName(string Port);
    }
}
