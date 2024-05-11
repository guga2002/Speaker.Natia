using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speaker.leison.EventArguments
{
    public class Eventargsforsendportinfo:EventArgs
    {
        public int port { get; set; }

        public Eventargsforsendportinfo(int port)
        {
                this.port=port;
        }
    }
}
