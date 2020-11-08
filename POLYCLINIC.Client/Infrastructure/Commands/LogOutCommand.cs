using POLYCLINIC.BLL;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Interfaces;
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

        private readonly IMainNavigation navigation;
        private readonly IAuthorizationService authorization;

        public LogOutCommand(IMainNavigation navigation, IAuthorizationService authorization)
        {
            this.navigation = navigation;
            this.authorization = authorization;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            authorization.LogOut();
            navigation.Navigate(new Authorization());
        }

    }
}
