using POLYCLINIC.Client.Infrastructure.Commands;

namespace POLYCLINIC.Client.ViewModels
{
    class VMAuthorization : VMBase
    {
        private AuthorizationCommand logInCommand;
        public AuthorizationCommand LogInCommand => logInCommand ??
                  (logInCommand = new AuthorizationCommand());
    }
}
