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
                new Weekday(){Name = "Monday"},
                new Weekday(){Name = "Tuesday"},
                new Weekday(){Name = "Wednesday"},
                new Weekday(){Name = "Thursday"},
                new Weekday(){Name = "Friday"},
                new Weekday(){Name = "Saturday"},
                new Weekday(){Name = "Sunday"}
            };
            var voucherStateList = new List<VoucherState>()
            {
                new VoucherState(){Name = "open"},
                new VoucherState(){Name = "completed"},
                new VoucherState(){Name = "overdue"},
                new VoucherState(){Name = "cancelled"}
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
                    FirstName = "Test patient 1",
                    LastName = "Test patient 1",
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
                new Specialization(){Name = "Spec 1"},
                new Specialization(){Name = "Spec 2"}
            };
            var doctorList = new List<Doctor>()
            {
                new Doctor()
                {
                    FirstName = "Test doctor 1",
                    LastName = "Test doctor 1",
                    Login = "Test doctor 1",
                    Password = "Test doctor 1",
                    Region = regionList[0],
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

            genderList.ForEach(e => context.Gender.Add(e));
            dayList.ForEach(e => context.Weekday.Add(e));
            voucherStateList.ForEach(e => context.VoucherState.Add(e));
            streetList.ForEach(e => context.Street.Add(e));
            regionList.ForEach(e => context.Region.Add(e));
            patientList.ForEach(e => context.Patient.Add(e));
            specializationList.ForEach(e => context.Specialization.Add(e));
            doctorList.ForEach(e => context.Doctor.Add(e));
            adminList.ForEach(e => context.Admin.Add(e));

            context.SaveChanges();
        }
    }
}
