using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.Client.ViewModels
{
    class VMPatient : VMBase
    {
        private readonly IBaseManager baseManager;
        private readonly IAuthorizationService authorization;

        public Patient Patient { get; }
        public string DateOfBirthWithoutTime => Patient.DateOfBirth.ToString("MM/dd/yyyy");

        private LogOutCommand logOutCommand;
        public LogOutCommand LogOutCommand => logOutCommand ??
                  (logOutCommand = new LogOutCommand(IoC.Get<IMainNavigation>(), authorization));

        public VMPatient()
        {
            this.baseManager = IoC.Get<IBaseManager>();
            this.authorization = IoC.Get<IAuthorizationService>();

            User user = authorization.GetCurrentUser();
            if (!(user is Patient)) throw new System.Exception("Invalid user type");
            Patient = user as Patient;
        }
    }
}
