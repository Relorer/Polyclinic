using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.BLL.Models
{
    public class VoucherForAppointmentModel : BaseModel<VoucherForAppointment>
    {
        public int Number => Entity.Id;
        public string Date => Entity.Date.ToString("dd MMMM yyyy HH:mm");
        public string State => GetRussianNameVoucherState(Entity.State);
        public string Doctor => Entity.Doctor.FirstName + " " + Entity.Doctor.LastName;
        public string DoctorSpecialization => Entity.Doctor.Specialization.Name;

        private string GetRussianNameVoucherState(VoucherState state)
        {
            return state switch
            {
                VoucherState.Opened => "Открыта",
                VoucherState.Completed => "Завершена",
                VoucherState.Expired => "Просрочена",
                VoucherState.Canceled => "Отменена",
                _ => throw new NotImplementedException("Остался больше ничего нет"),
            };
        }
    }
}
