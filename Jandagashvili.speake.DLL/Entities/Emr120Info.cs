using DDL.Database_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jandagashvili.speake.DLL.Entities
{
    public class Emr120Info:AbstractEntity
    {
        public string Port { get; set; }
        public int SourceEmr { get; set; }
        public string Text { get; set; }
    }
}
