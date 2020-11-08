using System;

namespace POLYCLINIC.BLL.Models
{
    public class ScheduleDayModel
    {
        public DayOfWeek Day { get; }
        public int NumberScheduleSlots { get; }
        public int NumberOccupied { get; }

        public ScheduleDayModel(DayOfWeek day, int numberVouchers, int numberOccupied)
        {
            this.Day = day;
            this.NumberScheduleSlots = numberVouchers;
            this.NumberOccupied = numberOccupied;
        }

        public override string ToString()
        {
            if (NumberScheduleSlots == 0) return string.Empty;
            return $"{NumberOccupied}/{NumberScheduleSlots}";
        }
    }
}
