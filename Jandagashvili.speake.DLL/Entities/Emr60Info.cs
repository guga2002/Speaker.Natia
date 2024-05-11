using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDL.Database_Layer.Entities
{
    public class Emr60Info : AbstractEntity
    {
        public string Port { get; set; }
        public int SourceEmr { get; set; }
        public string Text { get; set; }
    }
}
