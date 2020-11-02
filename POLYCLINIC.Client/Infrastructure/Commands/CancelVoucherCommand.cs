using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class CancelVoucherCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly ICancleVoucherService cancelVoucherService;

        public CancelVoucherCommand(ICancleVoucherService cancelVoucherService)
        {
            this.cancelVoucherService = cancelVoucherService;
        }

        public bool CanExecute(object parameter)
        {
            VoucherForAppointmentModel model = parameter as VoucherForAppointmentModel;
            return model != null && model.State == "Открыта"; //TODO Hardcode
        }

        public void Execute(object parameter)
        {
            VoucherForAppointmentModel model = parameter as VoucherForAppointmentModel;
            cancelVoucherService.CancelVoucher(model.Entity.Id);
        }
    }
}
