using POLYCLINIC.Client.Pages;
using POLYCLINIC.Client.Utils;

namespace POLYCLINIC.Client.ViewModels
{
    class VMAuthorization : VMBase
    {
        private RelayCommand logInCommand;
        public RelayCommand LogInCommand
        {
            get
            {
                return logInCommand ??
                  (logInCommand = new RelayCommand(obj =>
                  {
                      //TODO Authorization logic
                      MainNavigation.Instance.Navigate(new Patient());
                  }));
            }
        }
    }
}
