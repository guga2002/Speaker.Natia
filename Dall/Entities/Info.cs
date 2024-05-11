using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDL.Database_Layer.Entities
{
    [Table("Infos")]
    public class Info
    {
        [Key]
        public int Id { get; set; }

        [Column("Alarm_For_Display")]
        public string AlarmMessage { get; set; }

        [ForeignKey("chanell")]
        [Column("CHanell_Id")]
        public int CHanellId { get; set; }
        public Chanell chanell { get; set; }
    }
}
