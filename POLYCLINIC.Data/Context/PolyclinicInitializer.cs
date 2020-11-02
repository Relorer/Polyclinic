using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;

namespace POLYCLINIC.Data
{
    public class PolyclinicInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BaseContext>
    {
        protected override void Seed(BaseContext context)
        {
            var countryList = new List<Country>()
            {
                new Country(){Name = "Россия"},
            };
            var genderList = new List<Gender>()
            {
                new Gender(){Name = "Мужской"},
                new Gender(){Name = "Женский"}
            };
            var dayList = new List<Weekday>()
            {
                new Weekday(){Name = "Понедельник"},
                new Weekday(){Name = "Вторник"},
                new Weekday(){Name = "Среда"},
                new Weekday(){Name = "Четверг"},
                new Weekday(){Name = "Пятница"},
                new Weekday(){Name = "Суббота"},
                new Weekday(){Name = "Воскресенье"}
            };
            var streetList = new List<Street>()
            {
                new Street(){Name = "S1"},
                new Street(){Name = "S2"},
                new Street(){Name = "S3"}
            };
            var regionList = new List<Region>()
            {
                new Region(){Name = "R1", Streets = streetList}
            };
            var patientList = new List<Patient>()
            {
                new Patient()
                {
                    FirstName = "Шип",
                    LastName = "Валерьевич",
                    Login = "Test patient 1",
                    Password = "Test patient 1",
                    DateOfBirth = DateTime.Now,
                    Gender = genderList[0],
                    MedicalPolicyNumber = "Test patient 1",
                    Street = streetList[0],
                    Vouchers = new List<VoucherForAppointment>(),
                    Citizenship = countryList[0],
                    IdentityDocument = "Паспорт гражданина РФ 5789 815462, выдан отделом УФМС России по Ивановской области в Шуйском районе, код подразделения 370-009, дата выдачи 06.06.2011",
                    PlaceOfBirth = "Нашли под дверью"
                },
                new Patient()
                {
                    FirstName = "Test patient 2",
                    LastName = "Test patient 2",
                    Login = "Test patient 2",
                    Password = "Test patient 2",
                    DateOfBirth = DateTime.Now,
                    Gender = genderList[1],
                    MedicalPolicyNumber = "Test patient 2",
                    Street = streetList[1],
                    Vouchers = new List<VoucherForAppointment>(),
                    Citizenship = countryList[0],
                    IdentityDocument = "Паспорт гражданина РФ 5789 815462, выдан отделом УФМС России по Ивановской области в Шуйском районе, код подразделения 370-009, дата выдачи 06.06.2011",
                    PlaceOfBirth = "Нашли под дверью"
                }
            };
            var specializationList = new List<Specialization>()
            {
                new Specialization(){Name = "Педиатр"},
                new Specialization(){Name = "Spec 2"}
            };
            var doctorList = new List<Doctor>()
            {
                new Doctor()
                {
                    FirstName = "Алексий",
                    LastName = "Алексеевич",
                    Login = "Test doctor 1",
                    Password = "Test doctor 1",
                    Region = regionList[0],
                    Specialization = specializationList[0],
                    ScheduleSlots = new List<ScheduleSlot>(),
                    Vouchers = new List<VoucherForAppointment>()
                }
            };
            var adminList = new List<Admin>()
            {
                new Admin()
                {
                    FirstName = "Test admin 1",
                    LastName = "Test admin 1",
                    Login = "Test admin 1",
                    Password = "Test admin 1",
                }
            };
            var voucherList = new List<VoucherForAppointment>()
            {
                new VoucherForAppointment()
                {
                    State = VoucherState.Opened,
                    Date = DateTime.Now,
                    Doctor = doctorList[0],
                    Patient = patientList[0]
                },
                new VoucherForAppointment()
                {
                    State = VoucherState.Completed,
                    Date = DateTime.Now,
                    Doctor = doctorList[0],
                    Patient = patientList[0]
                }
            };

            genderList.ForEach(e => context.Gender.Add(e));
            dayList.ForEach(e => context.Weekday.Add(e));
            streetList.ForEach(e => context.Street.Add(e));
            regionList.ForEach(e => context.Region.Add(e));
            patientList.ForEach(e => context.Patient.Add(e));
            specializationList.ForEach(e => context.Specialization.Add(e));
            doctorList.ForEach(e => context.Doctor.Add(e));
            adminList.ForEach(e => context.Admin.Add(e));
            voucherList.ForEach(e => context.VoucherForAppointment.Add(e));

            context.SaveChanges();
        }
    }
}
