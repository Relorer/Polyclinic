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
    class VMChoiceTime : VMBase
    {
        private readonly IAppointmentNavigation navigation;
        private readonly IСreatingVoucherService сreatingVoucherService;

        private DateTime day;

        public string CurrentDayDetails => $"{day:ddd}{getTimeRange()}";
        public string CurrentDay => day.ToString("M");
        public string PrevDay => "← " + day.AddDays(-1).ToString("M");
        public string NextDay => day.AddDays(1).ToString("M") + " →";

        public ICollection<ScheduleSlotModel> ScheduleSlots => сreatingVoucherService.Doctor.ScheduleSlots
            .Where(s => s.Weekday == day.DayOfWeek)
            .OrderBy(s1 => s1.StartTime)
            .ToList()
            .ModelList<ScheduleSlot, ScheduleSlotModel>();

        private RelayCommand goToNextDayCommand;
        public RelayCommand GoToNextDayCommand
        {
            get
            {
                return goToNextDayCommand ?? (goToNextDayCommand = new RelayCommand(obj =>
                {
                    day = day.AddDays(1);
                    сreatingVoucherService.Date = day;
                    onDayChanged();
                }));
            }
        }

        private RelayCommand goToPrevDayCommand;
        public RelayCommand GoToPrevDayCommand
        {
            get
            {
                return goToPrevDayCommand ?? (goToPrevDayCommand = new RelayCommand(obj =>
                {
                    day = day.AddDays(-1);
                    сreatingVoucherService.Date = day;
                    onDayChanged();
                }, obj =>
                {
                    var now = DateTime.Now;
                    return now <= day.AddDays(-1);
                }
                ));
            }
        }

        private RelayCommand backCommand;
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(obj => navigation.Navigate(new ChoiceDoctor())));
            }
        }

        private ChooseTimeCommand chooseTimeCommand;
        public ChooseTimeCommand ChooseTimeCommand => chooseTimeCommand ??
                  (chooseTimeCommand = new ChooseTimeCommand(IoC.Get<IAppointmentNavigation>(), сreatingVoucherService));

        public VMChoiceTime()
        {
            this.navigation = IoC.Get<IAppointmentNavigation>();
            this.сreatingVoucherService = IoC.Get<IСreatingVoucherService>();

            day = this.сreatingVoucherService.Date;
        }

        private void onDayChanged()
        {
            OnPropertyChanged("CurrentDayDetails");
            OnPropertyChanged("CurrentDay");
            OnPropertyChanged("PrevDay");
            OnPropertyChanged("NextDay");
            OnPropertyChanged("ScheduleSlots");
        }

        private string getTimeRange()
        {
            var slots = сreatingVoucherService.Doctor.ScheduleSlots.ToList().Where(s => s.Weekday == day.DayOfWeek);
            if (slots.Count() > 0)
            {
                DateTime start = DateTime.MaxValue;
                DateTime end = DateTime.MinValue;
                foreach (var slot in slots)
                {
                    start = slot.StartTime < start ? slot.StartTime : start;
                    end = slot.EndTime > end ? slot.EndTime : end;
                }
                return $" {start:hh\\:mm} - {end:hh\\:mm}";
            }
            return string.Empty;
        }
    }
}
