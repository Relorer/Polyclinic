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
        private LiveData<VoucherState> voucherState;
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
        public LiveData<VoucherState> VoucherState => voucherState ?? (voucherState = new LiveData<VoucherState>(ctx.VoucherState));
        public LiveData<VoucherForAppointment> VoucherForAppointment => voucherForAppointment ?? (voucherForAppointment = new LiveData<VoucherForAppointment>(ctx.VoucherForAppointment));

        public BaseManager()
        {
            this.ctx = new BaseContext();
        }
    }
}
