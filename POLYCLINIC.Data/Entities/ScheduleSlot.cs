using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Data.Entities
{
    public class ScheduleSlot : BaseEntity
    {
        public Weekday Weekday { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
