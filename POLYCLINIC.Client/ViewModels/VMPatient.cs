using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;
using POLYCLINIC.Data.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;

namespace POLYCLINIC.Client.ViewModels
{
    class VMPatient : VMBase
    {
        private readonly IAppointmentNavigation navigation;
        private readonly IBaseManager baseManager;
        private readonly IAuthorizationService authorization;

        #region Patient info properties

        public Page CurrentPage => navigation.CurrentPage;
        public Patient Patient { get; }
        public string DateOfBirthWithoutTime => Patient.DateOfBirth.ToString("MM/dd/yyyy");
        public string PatientName => $"{Patient.FirstName} {Patient.LastName}";
        public ICollection<VoucherForAppointmentModel> Vouchers => Patient.Vouchers.OrderByDescending(p => p.Date).ToList().ModelList<VoucherForAppointment, VoucherForAppointmentModel>();

        #endregion

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
            navigation = IoC.Get<IAppointmentNavigation>();

            subscribeToUpdates();
            Patient = getCurrentPatient();

            navigation.Navigate(new ChoiceSpecialization());
        }

        private Patient getCurrentPatient()
        {
            User user = authorization.GetCurrentUser();
            if (!(user is Patient)) throw new Exception("Invalid user type");
            return user as Patient;
        }

        private void subscribeToUpdates()
        {
            navigation.CurrentPageChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
            baseManager.VoucherForAppointment.TableChanged += (sender, e) => OnPropertyChanged("Vouchers");
        }
    }
}
