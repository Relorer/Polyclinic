using POLYCLINIC.BLL.Infrastructure;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Data;
using POLYCLINIC.Data.Entities;

namespace POLYCLINIC.BLL
{
    public class BaseManager : IBaseManager
    {
        private BaseContext ctx;

        private LiveData<Gender> gender;
        private LiveData<Weekday> weekday;
        private LiveData<Visit> visit;
        private LiveData<Admin> admin;
        private LiveData<Doctor> doctor;
        private LiveData<Patient> patient;
        private LiveData<Region> region;
        private LiveData<ScheduleSlot> scheduleSlot;
        private LiveData<Specialization> specialization;
        private LiveData<Street> street;
        private LiveData<User> user;
        private LiveData<Country> country;
        private LiveData<VoucherForAppointment> voucherForAppointment;

        public LiveData<Gender> Gender => gender ?? (gender = new LiveData<Gender>(ctx.Gender));
        public LiveData<Weekday> Weekday => weekday ?? (weekday = new LiveData<Weekday>(ctx.Weekday));
        public LiveData<Visit> Visit => visit ?? (visit = new LiveData<Visit>(ctx.Visit));
        public LiveData<Admin> Admin => admin ?? (admin = new LiveData<Admin>(ctx.Admin));
        public LiveData<Doctor> Doctor => doctor ?? (doctor = new LiveData<Doctor>(ctx.Doctor));
        public LiveData<Patient> Patient => patient ?? (patient = new LiveData<Patient>(ctx.Patient));
        public LiveData<Region> Region => region ?? (region = new LiveData<Region>(ctx.Region));
        public LiveData<ScheduleSlot> ScheduleSlot => scheduleSlot ?? (scheduleSlot = new LiveData<ScheduleSlot>(ctx.ScheduleSlot));
        public LiveData<Specialization> Specialization => specialization ?? (specialization = new LiveData<Specialization>(ctx.Specialization));
        public LiveData<Street> Street => street ?? (street = new LiveData<Street>(ctx.Street));
        public LiveData<User> User => user ?? (user = new LiveData<User>(ctx.User));
        public LiveData<Country> Country => country ?? (country = new LiveData<Country>(ctx.Country));
        public LiveData<VoucherForAppointment> VoucherForAppointment => voucherForAppointment ?? (voucherForAppointment = new LiveData<VoucherForAppointment>(ctx.VoucherForAppointment));

        public BaseManager()
        {
            this.ctx = new BaseContext();
        }

        public void Save()
        {
            ctx.SaveChanges();

            gender?.Refresh();
            weekday?.Refresh();
            visit?.Refresh();
            admin?.Refresh();
            doctor?.Refresh();
            patient?.Refresh();
            region?.Refresh();
            scheduleSlot?.Refresh();
            specialization?.Refresh();
            street?.Refresh();
            user?.Refresh();
            country?.Refresh();
            voucherForAppointment?.Refresh();
        }
    }
}
