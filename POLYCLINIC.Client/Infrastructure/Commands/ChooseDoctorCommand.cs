using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Infrastructure.Structures;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;
using POLYCLINIC.Data.Entities;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class ChooseDoctorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;

        public ChooseDoctorCommand(IAppointmentNavigation navigation, IСreatingVoucherService сreatingVoucherService)
        {
            this.navigation = navigation;
            this.сreatingVoucherService = сreatingVoucherService;
        }

        public bool CanExecute(object parameter)
        {
            var (doctor, date) = convert(parameter);
            if (doctor != null)
            {
                var schedule = new WeeklyScheduleModel() { Entity = doctor, Week = new BLL.Infrastructure.Week(date) };
                var day = schedule[date.DayOfWeek];
                return date >= DateTime.Now && day.NumberScheduleSlots > 0 && (day.NumberScheduleSlots - day.NumberOccupied) >= 0;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            var (doctor, date) = convert(parameter);
            сreatingVoucherService.Doctor = doctor;
            сreatingVoucherService.Date = date;
            navigation.Navigate(new ChoiceTime());
        }

        private (Doctor Doctor, DateTime Date) convert(object parameter)
        {
            AppointmentDayData param = parameter as AppointmentDayData;
            var cellInfo = param.DataGrid.SelectedCells.FirstOrDefault();

            if (cellInfo != null)
            {
                var cell = cellInfo.Column?.GetCellContent(cellInfo.Item);
                if (cell != null)
                {
                    var schedul = cell.DataContext as WeeklyScheduleModel;
                    string cellContent = (cell as TextBlock).Text;
                    int column = cellInfo.Column.DisplayIndex;
                    Doctor doctor = column == 0 ? null : schedul.Entity;
                    return (doctor, param.Week.GetDay((DayOfWeek)column));
                }
            }

            return (null, DateTime.Now);
        }
    }
}
