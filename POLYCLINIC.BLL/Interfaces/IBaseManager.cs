using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.Data.Entities;

namespace POLYCLINIC.BLL.Interfaces
{
    public interface IBaseManager
    {
        LiveData<Gender> Gender { get; }
        LiveData<NonWorkingDay> NonWorkingDay { get; }
        LiveData<Visit> Visit { get; }
        LiveData<Admin> Admin { get; }
        LiveData<Doctor> Doctor { get; }
        LiveData<Patient> Patient { get; }
        LiveData<Region> Region { get; }
        LiveData<ScheduleSlot> ScheduleSlot { get; }
        LiveData<Specialization> Specialization { get; }
        LiveData<Street> Street { get; }
        LiveData<User> User { get; }
        LiveData<VoucherForAppointment> VoucherForAppointment { get; }

        void Save();
    }
}
