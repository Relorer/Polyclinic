using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;

namespace POLYCLINIC.Client.ViewModels
{
    class VMAuthorization : VMBase
    {
        private LogInCommand logInCommand;
        public LogInCommand LogInCommand => logInCommand ??
                  (logInCommand = new LogInCommand(IoC.Get<IMainNavigation>(), IoC.Get<IAuthorizationService>()));
    }
}
