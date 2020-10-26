using POLYCLINIC.Client.Infrastructure.Commands;

namespace POLYCLINIC.Client.ViewModels
{
    class VMAuthorization : VMBase
    {
        private LogInCommand logInCommand;
        public LogInCommand LogInCommand => logInCommand ??
                  (logInCommand = new LogInCommand());
    }
}
