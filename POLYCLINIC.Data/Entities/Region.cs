using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Data.Entities
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Street> Streets { get; set; }
    }
}
