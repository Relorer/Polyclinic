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

        public CancelVoucherCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            VoucherForAppointmentModel model = parameter as VoucherForAppointmentModel;
            return model != null && model.State == "Открыта"; //TODO Hardcode
        }

        public void Execute(object parameter)
        {
            Console.WriteLine(parameter);
        }
    }
}
