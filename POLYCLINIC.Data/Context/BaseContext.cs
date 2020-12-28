using POLYCLINIC.Data.Entities;
using System.Data.Entity;

namespace POLYCLINIC.Data
{
    public class BaseContext : DbContext
    {
        private const string BaseName = "POLYCLINIC";

        public BaseContext() : base(BaseName)
        {
            Database.SetInitializer(new PolyclinicInitializer());
        }

        public DbSet<Gender> Gender { get; set; }
        public DbSet<Visit> Visit { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<ScheduleSlot> ScheduleSlot { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Street> Street { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<VoucherForAppointment> VoucherForAppointment { get; set; }
        public DbSet<NonWorkingDay> NonWorkingDay { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Admin>().ToTable("Admins");
        }
    }
}
