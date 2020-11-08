using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;

namespace POLYCLINIC.Client.ViewModels.MakeAppointment
{
    class VMConfirmChoice : VMBase
    {
        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;

        public string Date => сreatingVoucherService.Date.Date.AddMilliseconds(сreatingVoucherService.ScheduleSlot.StartTime.TotalMilliseconds)
                                                              .ToString("dd MMMM yyyy HH:mm");
        public string Doctor => $"{сreatingVoucherService.Doctor.FirstName} {сreatingVoucherService.Doctor.LastName}";
        public string Specialization => сreatingVoucherService.Specialization.Name;

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(obj => navigation.Navigate(new ChoiceDoctor())));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(obj => navigation.Navigate(new ChoiceSpecialization())));
            }
        }

        private RelayCommand confirmCommand;
        public RelayCommand ConfirmCommand
        {
            get
            {
                return confirmCommand ?? (confirmCommand = new RelayCommand(obj =>
                {
                    сreatingVoucherService.Create();
                    navigation.Navigate(new ChoiceSpecialization());
                }));
            }
        }

        public VMConfirmChoice()
        {
            this.navigation = IoC.Get<IAppointmentNavigation>();
            this.сreatingVoucherService = IoC.Get<IСreatingVoucherService>();
        }

    }
}
