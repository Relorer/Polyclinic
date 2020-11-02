using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;

namespace POLYCLINIC.Client.ViewModels
{
    class VMPatient : VMBase
    {
        private readonly IBaseManager baseManager;
        private readonly IAuthorizationService authorization;

        public Patient Patient { get; }
        public string DateOfBirthWithoutTime => Patient.DateOfBirth.ToString("MM/dd/yyyy");
        public ICollection<VoucherForAppointmentModel> Vouchers => Patient.Vouchers.ModelList<VoucherForAppointment, VoucherForAppointmentModel>();

        private LogOutCommand logOutCommand;
        public LogOutCommand LogOutCommand => logOutCommand ??
                  (logOutCommand = new LogOutCommand(IoC.Get<IMainNavigation>(), authorization));

        private CancelVoucherCommand cancelVoucherCommand;
        public CancelVoucherCommand CancelVoucherCommand => cancelVoucherCommand ??
                  (cancelVoucherCommand = new CancelVoucherCommand(IoC.Get<ICancleVoucherService>()));

        public VMPatient()
        {
            baseManager = IoC.Get<IBaseManager>();
            authorization = IoC.Get<IAuthorizationService>();

            User user = authorization.GetCurrentUser();
            if (!(user is Patient)) throw new Exception("Invalid user type");
            Patient = user as Patient;
            
            baseManager.VoucherForAppointment.TableChanged += (sender, e) => OnPropertyChanged("Vouchers");
        }
    }
}
