using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDL.Database_Layer.Entities
{
    public class Reciever : AbstractEntity
    {
        [Column("Emr_Number")]
        public int EmrNumber { get; set; }

        [Column("Card_In_Reciever")]
        public int Card { get; set; }

        [Column("Port_In_Reciever")]
        public int Port { get; set; }

        [Column("Chanell_Id")]
        [ForeignKey("Chanell")]
        public int ChanellId { get; set; }

        public Chanell Chanell { get; set; }
    }
}
