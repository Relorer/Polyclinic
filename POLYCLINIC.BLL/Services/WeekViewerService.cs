using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.BLL.Interfaces;
using System;

namespace POLYCLINIC.BLL.Services
{
    public class WeekViewerService : IWeekViewerService
    {
        public Week CurrentWeek { get; private set; }
        public Week PrevWeek { get; private set; }
        public Week NextWeek { get; private set; }

        public WeekViewerService(DateTime day)
        {
            this.CurrentWeek = new Week(day);
            this.PrevWeek = new Week(day.AddDays(-7));
            this.NextWeek = new Week(day.AddDays(7));
        }

        public void GoToNextWeek()
        {
            this.CurrentWeek.Monday = CurrentWeek.Monday.AddDays(7);
            this.PrevWeek.Monday = PrevWeek.Monday.AddDays(7);
            this.NextWeek.Monday = NextWeek.Monday.AddDays(7);
        }

        public void GoToPrevWeek()
        {
            this.CurrentWeek.Monday = CurrentWeek.Monday.AddDays(-7);
            this.PrevWeek.Monday = PrevWeek.Monday.AddDays(-7);
            this.NextWeek.Monday = NextWeek.Monday.AddDays(-7);
        }

    }
}
