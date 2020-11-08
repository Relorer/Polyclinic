using POLYCLINIC.BLL.Infrastructure;

namespace POLYCLINIC.BLL.Interfaces
{
    public interface IWeekViewerService
    {
        Week CurrentWeek { get; }
        Week PrevWeek { get; }
        Week NextWeek { get; }

        void GoToNextWeek();
        void GoToPrevWeek();
    }
}
