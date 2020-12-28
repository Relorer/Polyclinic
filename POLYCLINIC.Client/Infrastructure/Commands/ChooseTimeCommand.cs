using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Views.Pages.MakeAppointment;
using POLYCLINIC.Data.Entities;
using System;
using System.Linq;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class ChooseTimeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;

        public ChooseTimeCommand(IAppointmentNavigation navigation, IСreatingVoucherService сreatingVoucherService)
        {
            this.navigation = navigation;
            this.сreatingVoucherService = сreatingVoucherService;
        }

        public bool CanExecute(object parameter)
        {
            ScheduleSlotModel model = parameter as ScheduleSlotModel;

            return model != null && (model.Entity.Doctor.Vouchers == null || !model.Entity.Doctor.Vouchers.Any(voucher =>
            voucher.ScheduleSlot == model.Entity &&
            voucher.State == VoucherState.Opened &&
            voucher.Date.Date == сreatingVoucherService.Date.Date)) &&
            model.Entity.Doctor
            .NonWorkingDays?
            .FirstOrDefault(n => n.Date.Year == сreatingVoucherService.Date.Year &&
                n.Date.Month == сreatingVoucherService.Date.Month &&
                n.Date.Day == сreatingVoucherService.Date.Day
            ) == null;
        }

        public void Execute(object parameter)
        {
            ScheduleSlotModel model = parameter as ScheduleSlotModel;
            сreatingVoucherService.ScheduleSlot = model.Entity;
            navigation.Navigate(new ConfirmChoice());
        }
    }
}
