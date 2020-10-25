using System;
using System.Collections.Generic;

namespace POLYCLINIC.Data.Entities
{
    public class Patient : User
    {
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MedicalPolicyNumber { get; set; }
        public Street Street { get; set; }
        public ICollection<VoucherForAppointment> Vouchers { get; set; }
        public ICollection<Visit> Visits { get; set; }
    }
}
