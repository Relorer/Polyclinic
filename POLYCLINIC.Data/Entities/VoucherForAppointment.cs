using System;

namespace POLYCLINIC.Data.Entities
{
    public class VoucherForAppointment : BaseEntity
    {
        public DateTime Date { get; set; }
        public VoucherState State { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
