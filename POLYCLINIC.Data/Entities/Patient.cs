using System;
using System.Collections.Generic;

namespace POLYCLINIC.Data.Entities
{
    public class Patient : User
    {
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MedicalPolicyNumber { get; set; }
        public string IdentityDocument { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Street Street { get; set; }
        public virtual Country Citizenship { get; set; }
        public virtual ICollection<VoucherForAppointment> Vouchers { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
