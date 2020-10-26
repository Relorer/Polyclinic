using POLYCLINIC.BLL;
using POLYCLINIC.Client.Pages;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class LogOutCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            AuthorizationService.LogOut();
            MainNavigation.Instance.Navigate(new Authorization());
        }

    }
}
