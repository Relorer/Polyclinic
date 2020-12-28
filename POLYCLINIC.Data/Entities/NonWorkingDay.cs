using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Data.Entities
{
    public class NonWorkingDay : BaseEntity
    {
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
