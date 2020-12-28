using Microsoft.Win32;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Services;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Infrastructure.Commands;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace POLYCLINIC.Client.ViewModels
{
    internal class VMAdmin : VMBase
    {
        private readonly IBaseManager baseManager;
        private readonly IAuthorizationService authorization;

        public Admin Admin { get; }
        public string AdminName => $"{Admin.FirstName} {Admin.LastName}";
        public ObservableCollection<Doctor> Doctors { get; set; }
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Specialization> Specializations { get; set; }
        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<Street> Streets { get; set; }
        public ObservableCollection<Gender> Genders { get; set; }
        public ObservableCollection<ScheduleSlot> ScheduleSlots { get; set; }
        public ObservableCollection<ScheduleSlot> ScheduleSlotsCurrentDoctor { get; set; }
        private ObservableCollection<ScheduleSlot> deletedScheduleSlotsCurrentDoctor = new ObservableCollection<ScheduleSlot>();

        public ObservableCollection<NonWorkingDay> NonWorkingDaysCurrentDoctor { get; set; }
        private ObservableCollection<NonWorkingDay> deletedNonWorkingDaysCurrentDoctor = new ObservableCollection<NonWorkingDay>();

        public List<FirstReportLine> FirstReport => baseManager.VoucherForAppointment.List
            .Where(v => v.Date >= SelectedBeginDateFirstReport && v.Date <= SelectedEndDateFirstReport)
            .GroupBy(v => v.Doctor.Specialization)
            .Select(g => new FirstReportLine()
            {
                Specialization = g.Key.Name,
                Opened = g.Where(v => v.State == VoucherState.Opened).Count(),
                Canceled = g.Where(v => v.State == VoucherState.Canceled).Count(),
                Completed = g.Where(v => v.State == VoucherState.Completed).Count(),
                Expired = g.Where(v => v.State == VoucherState.Expired).Count(),
                All = g.Count()
            }).ToList();

        public List<SecondReportLine> SecondReport => baseManager.VoucherForAppointment.List
            .Where(v => v.Doctor.Specialization == SelectedSpecializationSecondReport && v.Date >= SelectedBeginDateSecondReport && v.Date <= SelectedEndDateSecondReport)
            .GroupBy(v => v.Doctor)
            .Select(g => new SecondReportLine()
            {
                Doctor = g.Key.FirstName + " " + g.Key.LastName,
                Opened = g.Where(v => v.State == VoucherState.Opened).Count(),
                Canceled = g.Where(v => v.State == VoucherState.Canceled).Count(),
                Completed = g.Where(v => v.State == VoucherState.Completed).Count(),
                Expired = g.Where(v => v.State == VoucherState.Expired).Count(),
                All = g.Count()
            }).ToList();

        public struct Day { public DayOfWeek DayOfWeek { get; set; } public string Name { get; set; } };
        public struct FirstReportLine
        {
            public string Specialization { get; set; }
            public int Opened { get; set; }
            public int Completed { get; set; }
            public int Expired { get; set; }
            public int Canceled { get; set; }
            public int All { get; set; }
        };

        public struct SecondReportLine
        {
            public string Doctor { get; set; }
            public int Opened { get; set; }
            public int Completed { get; set; }
            public int Expired { get; set; }
            public int Canceled { get; set; }
            public int All { get; set; }
        };

        public ObservableCollection<Day> Days { get; set; }

        private Doctor selectedDoctor;
        public Doctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                ScheduleSlotsCurrentDoctor = new ObservableCollection<ScheduleSlot>(ScheduleSlots.Where(slot => selectedDoctor != null && slot.Doctor.Id == selectedDoctor.Id && slot.Weekday == selectedDay.DayOfWeek).ToList());
                OnPropertyChanged("ScheduleSlotsCurrentDoctor");
            }
        }

        private Doctor selectedDoctorForNonWork;
        public Doctor SelectedDoctorForNonWork
        {
            get
            {
                return selectedDoctorForNonWork;
            }
            set
            {
                selectedDoctorForNonWork = value;
                NonWorkingDaysCurrentDoctor = new ObservableCollection<NonWorkingDay>(baseManager.NonWorkingDay.List.Where(d => d.Doctor.Id == selectedDoctorForNonWork.Id).ToList());
                OnPropertyChanged("NonWorkingDaysCurrentDoctor");
            }
        }

        private Day selectedDay;
        public Day SelectedDay
        {
            get
            {
                return selectedDay;
            }
            set
            {
                selectedDay = value;
                ScheduleSlotsCurrentDoctor = new ObservableCollection<ScheduleSlot>(ScheduleSlots.Where(slot => selectedDoctor != null && slot.Doctor.Id == selectedDoctor.Id && slot.Weekday == selectedDay.DayOfWeek).ToList());
                OnPropertyChanged("ScheduleSlotsCurrentDoctor");
            }
        }

        private DateTime selectedBeginDateFirstReport;
        public DateTime SelectedBeginDateFirstReport
        {
            get
            {
                return selectedBeginDateFirstReport;
            }
            set
            {
                selectedBeginDateFirstReport = value;
                OnPropertyChanged("FirstReport");
            }
        }

        private DateTime selectedEndDateFirstReport;
        public DateTime SelectedEndDateFirstReport
        {
            get
            {
                return selectedEndDateFirstReport;
            }
            set
            {
                selectedEndDateFirstReport = value;
                OnPropertyChanged("FirstReport");
            }
        }

        private DateTime selectedBeginDateSecondReport;
        public DateTime SelectedBeginDateSecondReport
        {
            get
            {
                return selectedBeginDateSecondReport;
            }
            set
            {
                selectedBeginDateSecondReport = value;
                OnPropertyChanged("SecondReport");
            }
        }

        private DateTime selectedEndDateSecondReport;
        public DateTime SelectedEndDateSecondReport
        {
            get
            {
                return selectedEndDateSecondReport;
            }
            set
            {
                selectedEndDateSecondReport = value;
                OnPropertyChanged("SecondReport");
            }
        }

        private Specialization selectedSpecializationSecondReport;
        public Specialization SelectedSpecializationSecondReport
        {
            get
            {
                return selectedSpecializationSecondReport;
            }
            set
            {
                selectedSpecializationSecondReport = value;
                OnPropertyChanged("SecondReport");
            }
        }

        public ScheduleSlot SelectedSheduleSlot { get; set; }
        public NonWorkingDay SelectedNonWorkingDay { get; set; }

        private LogOutCommand logOutCommand;
        public LogOutCommand LogOutCommand => logOutCommand ??
                  (logOutCommand = new LogOutCommand(IoC.Get<IMainNavigation>(), authorization));

        private RelayCommand saveTables;
        public RelayCommand SaveTables
        {
            get
            {
                return saveTables ?? (saveTables = new RelayCommand(obj =>
                {
                    try
                    {
                        baseManager.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand saveTimes;
        public RelayCommand SaveTimes
        {
            get
            {
                return saveTimes ?? (saveTimes = new RelayCommand(obj =>
                {
                    try
                    {
                        foreach (var item in ScheduleSlotsCurrentDoctor)
                        {
                            var found = ScheduleSlots.FirstOrDefault(s => s.Id == item.Id);
                            if (found != null)
                            {
                                found.StartTime = item.StartTime;
                                found.EndTime = item.EndTime;
                            }
                            else
                            {
                                baseManager.ScheduleSlot.Add(item);
                            }
                        }
                        foreach (var item in deletedScheduleSlotsCurrentDoctor)
                        {
                            if (baseManager.ScheduleSlot.List.Contains(item))
                            {
                                baseManager.ScheduleSlot.Remove(item);
                            }
                        }
                        baseManager.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand saveNonWorkingDays;
        public RelayCommand SaveNonWorkingDays
        {
            get
            {
                return saveNonWorkingDays ?? (saveNonWorkingDays = new RelayCommand(obj =>
                {
                    try
                    {
                        foreach (var item in NonWorkingDaysCurrentDoctor)
                        {
                            var found = baseManager.NonWorkingDay.FirstOrDefault(s => s.Id == item.Id);
                            if (found != null)
                            {
                                found.Reason = item.Reason;
                                found.Date = item.Date;
                            }
                            else
                            {
                                baseManager.NonWorkingDay.Add(item);
                            }
                        }
                        foreach (var item in deletedNonWorkingDaysCurrentDoctor)
                        {
                            if (baseManager.NonWorkingDay.List.Contains(item))
                            {
                                baseManager.NonWorkingDay.Remove(item);
                            }
                        }
                        baseManager.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand addTimes;
        public RelayCommand AddTimes
        {
            get
            {
                return addTimes ?? (addTimes = new RelayCommand(obj =>
                {
                    try
                    {
                        ScheduleSlotsCurrentDoctor.Add(new ScheduleSlot() { Weekday = selectedDay.DayOfWeek, Doctor = selectedDoctor, EndTime = new DateTime(2000, 1, 1), StartTime = new DateTime(2000, 1, 1) });
                        OnPropertyChanged("ScheduleSlotsCurrentDoctor");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand deleteTimes;
        public RelayCommand DeleteTimes
        {
            get
            {
                return deleteTimes ?? (deleteTimes = new RelayCommand(obj =>
                {
                    try
                    {
                        if (SelectedSheduleSlot != null)
                        {
                            deletedScheduleSlotsCurrentDoctor.Add(SelectedSheduleSlot);
                            ScheduleSlotsCurrentDoctor.Remove(SelectedSheduleSlot);
                            OnPropertyChanged("ScheduleSlotsCurrentDoctor");
                        }
                        else
                        {
                            MessageBox.Show("Ни одна запись не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand addNonWorkingDay;
        public RelayCommand AddNonWorkingDay
        {
            get
            {
                return addNonWorkingDay ?? (addNonWorkingDay = new RelayCommand(obj =>
                {
                    try
                    {
                        NonWorkingDaysCurrentDoctor.Add(new NonWorkingDay() { Date = DateTime.Now, Doctor = SelectedDoctorForNonWork});
                        OnPropertyChanged("NonWorkingDaysCurrentDoctor");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand deleteNonWorkingDay;
        public RelayCommand DeleteNonWorkingDay
        {
            get
            {
                return deleteNonWorkingDay ?? (deleteNonWorkingDay = new RelayCommand(obj =>
                {
                    try
                    {
                        if (SelectedNonWorkingDay != null)
                        {
                            deletedNonWorkingDaysCurrentDoctor.Add(SelectedNonWorkingDay);
                            NonWorkingDaysCurrentDoctor.Remove(SelectedNonWorkingDay);
                            OnPropertyChanged("NonWorkingDaysCurrentDoctor");
                        }
                        else
                        {
                            MessageBox.Show("Ни одна запись не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand exportFirstReport;
        public RelayCommand ExportFirstReport
        {
            get
            {
                return exportFirstReport ?? (exportFirstReport = new RelayCommand(obj =>
                {
                    try
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*";
                        saveFileDialog.ShowDialog();
                        var path = saveFileDialog.FileName;

                        var export = new ExportToWordService(path)
                            .WriteLine("Общее", 14, NPOI.XWPF.UserModel.ParagraphAlignment.CENTER)
                            .WriteLine(SelectedBeginDateFirstReport.ToString("d") + " – " + SelectedEndDateFirstReport.ToString("d"), 12, NPOI.XWPF.UserModel.ParagraphAlignment.CENTER);

                        export.UseCustome(doc =>
                        {
                            XWPFTable tbl = doc.CreateTable(1 + FirstReport.Count, 6);

                            tbl.Rows[0].GetCell(0).SetText("Специальность");
                            tbl.Rows[0].GetCell(1).SetText("Открытые записи");
                            tbl.Rows[0].GetCell(2).SetText("Просроченные записи");
                            tbl.Rows[0].GetCell(3).SetText("Отмененные записи");
                            tbl.Rows[0].GetCell(4).SetText("Закрытые записи");
                            tbl.Rows[0].GetCell(5).SetText("Всего");

                            for (int i = 1; i < 1 + FirstReport.Count; i++)
                            {
                                tbl.Rows[i].GetCell(0).SetText(FirstReport[i - 1].Specialization);
                                tbl.Rows[i].GetCell(1).SetText(FirstReport[i - 1].Opened.ToString());
                                tbl.Rows[i].GetCell(2).SetText(FirstReport[i - 1].Expired.ToString());
                                tbl.Rows[i].GetCell(3).SetText(FirstReport[i - 1].Canceled.ToString());
                                tbl.Rows[i].GetCell(4).SetText(FirstReport[i - 1].Completed.ToString());
                                tbl.Rows[i].GetCell(5).SetText(FirstReport[i - 1].All.ToString());
                            }
                        });

                        export.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        private RelayCommand exportSecondReport;
        public RelayCommand ExportSecondReport
        {
            get
            {
                return exportSecondReport ?? (exportSecondReport = new RelayCommand(obj =>
                {
                    try
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*";
                        saveFileDialog.ShowDialog();
                        var path = saveFileDialog.FileName;

                        var export = new ExportToWordService(path)
                            .WriteLine("Обращения к специалистам", 14, NPOI.XWPF.UserModel.ParagraphAlignment.CENTER)
                            .WriteLine(SelectedBeginDateFirstReport.ToString("d") + " – " + SelectedEndDateFirstReport.ToString("d"), 12, NPOI.XWPF.UserModel.ParagraphAlignment.CENTER)
                            .WriteLine("Специальность: " + SelectedSpecializationSecondReport.Name, 12, NPOI.XWPF.UserModel.ParagraphAlignment.LEFT);

                        export.UseCustome(doc =>
                        {
                            XWPFTable tbl = doc.CreateTable(1 + SecondReport.Count, 6);

                            tbl.Rows[0].GetCell(0).SetText("Врач");
                            tbl.Rows[0].GetCell(1).SetText("Открытые записи");
                            tbl.Rows[0].GetCell(2).SetText("Просроченные записи");
                            tbl.Rows[0].GetCell(3).SetText("Отмененные записи");
                            tbl.Rows[0].GetCell(4).SetText("Закрытые записи");
                            tbl.Rows[0].GetCell(5).SetText("Всего");

                            for (int i = 1; i < 1 + SecondReport.Count; i++)
                            {
                                tbl.Rows[i].GetCell(0).SetText(SecondReport[i - 1].Doctor);
                                tbl.Rows[i].GetCell(1).SetText(SecondReport[i - 1].Opened.ToString());
                                tbl.Rows[i].GetCell(2).SetText(SecondReport[i - 1].Expired.ToString());
                                tbl.Rows[i].GetCell(3).SetText(SecondReport[i - 1].Canceled.ToString());
                                tbl.Rows[i].GetCell(4).SetText(SecondReport[i - 1].Completed.ToString());
                                tbl.Rows[i].GetCell(5).SetText(SecondReport[i - 1].All.ToString());
                            }
                        });

                        export.Save();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }));
            }
        }

        public VMAdmin()
        {
            baseManager = IoC.Get<IBaseManager>();
            authorization = IoC.Get<IAuthorizationService>();


            Doctors = baseManager.Doctor.List;
            Patients = baseManager.Patient.List;
            Specializations = baseManager.Specialization.List;
            Regions = baseManager.Region.List;
            Streets = baseManager.Street.List;
            Genders = baseManager.Gender.List;
            ScheduleSlots = baseManager.ScheduleSlot.List;
            SelectedEndDateFirstReport = DateTime.Now;
            SelectedBeginDateFirstReport = DateTime.Now.AddYears(-1);
            SelectedEndDateSecondReport = DateTime.Now;
            SelectedBeginDateSecondReport = DateTime.Now.AddYears(-1);
            SelectedSpecializationSecondReport = Specializations[0];
            Days = new ObservableCollection<Day>()
            {
                new Day() { DayOfWeek = DayOfWeek.Monday, Name = "Понедельник"},
                new Day() { DayOfWeek = DayOfWeek.Tuesday, Name = "Вторник"},
                new Day() { DayOfWeek = DayOfWeek.Wednesday, Name = "Среда"},
                new Day() { DayOfWeek = DayOfWeek.Thursday, Name = "Четверг"},
                new Day() { DayOfWeek = DayOfWeek.Friday, Name = "Пятница"},
                new Day() { DayOfWeek = DayOfWeek.Saturday, Name = "Суббота"},
                new Day() { DayOfWeek = DayOfWeek.Sunday, Name = "Воскресенье"},
            };
            Admin = getCurrentAdmin();
        }

        private Admin getCurrentAdmin()
        {
            User user = authorization.GetCurrentUser();
            if (!(user is Admin)) throw new Exception("Invalid user type");
            return user as Admin;
        }
    }
}
