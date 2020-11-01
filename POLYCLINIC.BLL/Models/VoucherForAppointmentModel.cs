using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.BLL.Models
{
    public class VoucherForAppointmentModel : BaseModel<VoucherForAppointment>
    {
        public int Number => Entity.Id;
        public string Date => Entity.Date.ToString("dd MMMM yyyy HH:mm");
        public string State => Entity.State.Name;
        public string Doctor => Entity.Doctor.FirstName + " " + Entity.Doctor.LastName;
        public string DoctorSpecialization => Entity.Doctor.Specialization.Name;
    }
}
