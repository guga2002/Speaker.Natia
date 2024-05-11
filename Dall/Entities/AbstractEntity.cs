using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDL.Database_Layer.Entities
{
    public abstract  class AbstractEntity
    {
            [Key]
            public int Id { get; set; }

            protected AbstractEntity()
            {

            }

            public AbstractEntity(int Id)
            {
                this.Id = Id;
            }
    }
}
