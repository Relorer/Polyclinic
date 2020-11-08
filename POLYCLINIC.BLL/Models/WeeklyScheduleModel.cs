using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.Data.Entities;
using System;
using System.Linq;

namespace POLYCLINIC.BLL.Models
{
    public class WeeklyScheduleModel : BaseModel<Doctor>
    {
        public string DoctorFullName => Entity.FirstName + " " + Entity.LastName;

        public ScheduleDayModel this[DayOfWeek day] => GetScheduleModel(day);

        public Week Week { get; set; }

        public ScheduleDayModel Monday => this[DayOfWeek.Monday];
        public ScheduleDayModel Tuesday => this[DayOfWeek.Tuesday];
        public ScheduleDayModel Wednesday => this[DayOfWeek.Wednesday];
        public ScheduleDayModel Thursday => this[DayOfWeek.Thursday];
        public ScheduleDayModel Friday => this[DayOfWeek.Friday];
        public ScheduleDayModel Saturday => this[DayOfWeek.Saturday];
        public ScheduleDayModel Sunday => this[DayOfWeek.Sunday];

        public WeeklyScheduleModel()
        {
            Week = new Week(DateTime.Now);
        }

        private ScheduleDayModel GetScheduleModel(DayOfWeek day)
        {
            return new ScheduleDayModel(day, getNumberScheduleSlots(day), getNumberOccupied(day));
        }

        private int getNumberScheduleSlots(DayOfWeek day)
        {
            return Entity?.ScheduleSlots?.Where(slot => slot.Weekday == day).ToList().Count ?? 0;
        }

        private int getNumberOccupied(DayOfWeek day)
        {
            return Entity?.Vouchers?.Where(slot => slot.State == VoucherState.Opened && slot.Date.DayOfWeek == day && Week.Exist(slot.Date)).ToList().Count ?? 0;
        }
    }
}
