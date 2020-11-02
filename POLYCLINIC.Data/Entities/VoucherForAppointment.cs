using System;

namespace POLYCLINIC.Data.Entities
{
    public enum VoucherState { Opened, Completed, Expired, Canceled };

    public class VoucherForAppointment : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual VoucherState State { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
