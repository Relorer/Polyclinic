using System.Collections.Generic;

namespace POLYCLINIC.Data.Entities
{
    public class Doctor : User
    {
        public virtual Specialization Specialization { get; set; }
        public virtual Region Region { get; set; }
        public ICollection<ScheduleSlot> ScheduleSlots { get; set; }
        public ICollection<VoucherForAppointment> Vouchers { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
