using System;
using System.Collections.Generic;

namespace POLYCLINIC.Data.Entities
{
    public class ScheduleSlot : BaseEntity
    {
        public DayOfWeek Weekday { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual Doctor Doctor { get; set; }
        public ICollection<VoucherForAppointment> VoucherForAppointments { get; set; }
    }
}
