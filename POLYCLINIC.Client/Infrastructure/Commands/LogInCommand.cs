using POLYCLINIC.BLL;
using System;
using System.Windows.Input;

namespace POLYCLINIC.Client.Infrastructure.Commands
{
    class LogInCommand : ICommand
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
            var data = parameter as AuthorizationData;
            if (AuthorizationService.LogIn(data.Login, data.PasswordBox.Password))
            {
                MainNavigation.Instance.Navigate(AuthorizationService.GetCurrentUser());
            }
        }
    }
}
