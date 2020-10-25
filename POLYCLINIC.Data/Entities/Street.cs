using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Data.Entities
{
    public class Street : BaseEntity
    {
        public string Name { get; set; }
        public virtual Region Region { get; set; }
    }
}
