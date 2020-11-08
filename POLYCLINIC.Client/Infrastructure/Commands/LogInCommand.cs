using POLYCLINIC.BLL;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Interfaces;
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

        private readonly IMainNavigation navigation;
        private readonly IAuthorizationService authorization;

        public LogInCommand(IMainNavigation navigation, IAuthorizationService authorization)
        {
            this.navigation = navigation;
            this.authorization = authorization;
        }

        public bool CanExecute(object parameter)
        {
            var data = parameter as AuthorizationData;
            return data != null && !string.IsNullOrEmpty(data.Login);
        }

        public void Execute(object parameter)
        {
            var data = parameter as AuthorizationData;
            if (authorization.LogIn(data.Login, data.PasswordBox.Password))
            {
                navigation.Navigate(authorization.GetCurrentUser());
            }
        }
    }
}
