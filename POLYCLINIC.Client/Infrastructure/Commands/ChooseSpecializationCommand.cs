using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;
using POLYCLINIC.Data.Entities;
using System;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class ChooseSpecializationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;

        public ChooseSpecializationCommand(IAppointmentNavigation navigation, IСreatingVoucherService сreatingVoucherService)
        {
            this.navigation = navigation;
            this.сreatingVoucherService = сreatingVoucherService;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            сreatingVoucherService.Clear();
            сreatingVoucherService.Specialization = parameter as Specialization;
            navigation.Navigate(new ChoiceDoctor());
        }
    }
}
