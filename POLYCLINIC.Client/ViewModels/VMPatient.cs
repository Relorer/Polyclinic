using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;

namespace POLYCLINIC.Client.ViewModels
{
    class VMPatient : VMBase
    {
        private LogOutCommand logOutCommand;
        public LogOutCommand LogOutCommand => logOutCommand ??
                  (logOutCommand = new LogOutCommand(IoC.Get<IMainNavigation>(), IoC.Get<IAuthorizationService>()));
    }
}
