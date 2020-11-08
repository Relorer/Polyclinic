using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.BLL.Services
{
    public class СreatingVoucherService : IСreatingVoucherService
    {
        private readonly IBaseManager baseManager;
        private readonly IAuthorizationService authorizationService;

        public Specialization Specialization { get; set; }
        public Doctor Doctor { get; set; }
        public ScheduleSlot ScheduleSlot { get; set; }
        public DateTime Date { get; set; }

        public СreatingVoucherService(IBaseManager baseManager, IAuthorizationService authorizationService)
        {
            this.baseManager = baseManager;
            this.authorizationService = authorizationService;
            Clear();
        }

        public void Clear()
        {
            Specialization = null;
            Doctor = null;
            Date = DateTime.MinValue;
        }

        public void Create()
        {
            if (!(authorizationService.GetCurrentUser() is Patient))
            {
                throw new Exception("Invalid user type for creating appointment");
            }
            var voucher = new VoucherForAppointment()
            {
                Date = this.Date.Date.AddMilliseconds(ScheduleSlot.StartTime.TotalMilliseconds),
                ScheduleSlot = this.ScheduleSlot,
                Doctor = this.Doctor,
                Patient = (Patient)authorizationService.GetCurrentUser(),
                State = VoucherState.Opened,
            };
            baseManager.VoucherForAppointment.Add(voucher);
            baseManager.Save();
        }
    }
}
