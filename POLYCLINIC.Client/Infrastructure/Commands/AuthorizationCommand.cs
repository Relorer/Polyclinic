using POLYCLINIC.Client.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class AuthorizationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var data = parameter as AuthorizationData;
            return data != null && !string.IsNullOrEmpty(data.Login);
        }

        public void Execute(object parameter)
        {
            //TODO Authorization logic
            MainNavigation.Instance.Navigate(new Patient());
        }
    }
}
