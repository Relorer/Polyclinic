using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Models;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POLYCLINIC.Client.ViewModels
{
    class VMChoiceDoctor : VMBase
    {
        private readonly IBaseManager baseManager;
        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;
        private readonly IWeekViewerService weekViewerService;
        private readonly IAuthorizationService authorization;
        public Patient Patient { get; }

        #region Weeks

        public Week CurrentWeek => weekViewerService.CurrentWeek;
        public string CurrentWeekString => weekViewerService.CurrentWeek.ToString();
        public string PrevWeekString => "← " + weekViewerService.PrevWeek.ToString();
        public string NextWeekString => weekViewerService.NextWeek.ToString() + " →";

        #endregion

        #region Days of week

        public string Monday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Monday);
        public string Tuesday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Tuesday);
        public string Wednesday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Wednesday);
        public string Thursday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Thursday);
        public string Friday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Friday);
        public string Saturday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Saturday);
        public string Sunday => weekViewerService.CurrentWeek.ToString(DayOfWeek.Sunday);

        #endregion

        public IEnumerable<WeeklyScheduleModel> Doctors
        {
            get
            {
                var doctors = baseManager.Doctor.List
                    .Where(d => d.Specialization == сreatingVoucherService.Specialization && Patient.Street.Region.Id == d.Region.Id)
                    .ToList()
                    .ModelList<Doctor, WeeklyScheduleModel>();
                foreach (var doctor in doctors) doctor.Week = CurrentWeek;
                return doctors;
            }
        }

        private RelayCommand goToNextWeekCommand;
        public RelayCommand GoToNextWeekCommand
        {
            get
            {
                return goToNextWeekCommand ?? (goToNextWeekCommand = new RelayCommand(obj =>
                {
                    weekViewerService.GoToNextWeek();
                    onWeekChanged();
                }));
            }
        }

        private RelayCommand goToPrevWeekCommand;
        public RelayCommand GoToPrevWeekCommand
        {
            get
            {
                return goToPrevWeekCommand ?? (goToPrevWeekCommand = new RelayCommand(obj =>
                {
                    weekViewerService.GoToPrevWeek();
                    onWeekChanged();
                }, obj =>
                {
                    var now = DateTime.Now.Date;
                    return now < weekViewerService.CurrentWeek.Monday
                        || now > weekViewerService.CurrentWeek.Sunday;
                }
                ));
            }
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(obj => navigation.Navigate(new ChoiceSpecialization())));
            }
        }

        private ChooseDoctorCommand chooseDoctorCommand;
        public ChooseDoctorCommand ChooseDoctorCommand => chooseDoctorCommand ??
                  (chooseDoctorCommand = new ChooseDoctorCommand(IoC.Get<IAppointmentNavigation>(), сreatingVoucherService));

        public VMChoiceDoctor()
        {
            this.baseManager = IoC.Get<IBaseManager>();
            this.navigation = IoC.Get<IAppointmentNavigation>();
            this.сreatingVoucherService = IoC.Get<IСreatingVoucherService>();
            this.authorization = IoC.Get<IAuthorizationService>();
            this.weekViewerService = IoC.Get<IWeekViewerService>();

            Patient = getCurrentPatient();

            baseManager.Doctor.TableChanged += (sender, e) => OnPropertyChanged("Doctors");
        }

        private void onWeekChanged()
        {
            OnPropertyChanged("Doctors");

            OnPropertyChanged("CurrentWeekString");
            OnPropertyChanged("PrevWeekString");
            OnPropertyChanged("NextWeekString");

            OnPropertyChanged("Monday");
            OnPropertyChanged("Tuesday");
            OnPropertyChanged("Wednesday");
            OnPropertyChanged("Thursday");
            OnPropertyChanged("Friday");
            OnPropertyChanged("Saturday");
            OnPropertyChanged("Sunday");
        }
        private Patient getCurrentPatient()
        {
            User user = authorization.GetCurrentUser();
            if (!(user is Patient)) throw new Exception("Invalid user type");
            return user as Patient;
        }
    }
}
