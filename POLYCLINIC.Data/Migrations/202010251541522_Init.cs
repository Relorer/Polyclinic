namespace POLYCLINIC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Streets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Region_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.Region_Id)
                .Index(t => t.Region_Id);
            
            CreateTable(
                "dbo.ScheduleSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Doctor_Id = c.Int(),
                        Weekday_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .ForeignKey("dbo.Weekdays", t => t.Weekday_Id)
                .Index(t => t.Doctor_Id)
                .Index(t => t.Weekday_Id);
            
            CreateTable(
                "dbo.Weekdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(),
                        Date = c.DateTime(nullable: false),
                        Doctor_Id = c.Int(),
                        Patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .Index(t => t.Doctor_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoucherForAppointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Doctor_Id = c.Int(),
                        Patient_Id = c.Int(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .ForeignKey("dbo.Patients", t => t.Patient_Id)
                .ForeignKey("dbo.VoucherStates", t => t.State_Id)
                .Index(t => t.Doctor_Id)
                .Index(t => t.Patient_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.VoucherStates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Region_Id = c.Int(),
                        Specialization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Regions", t => t.Region_Id)
                .ForeignKey("dbo.Specializations", t => t.Specialization_Id)
                .Index(t => t.Id)
                .Index(t => t.Region_Id)
                .Index(t => t.Specialization_Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Gender_Id = c.Int(),
                        Street_Id = c.Int(),
                        DateOfBirth = c.DateTime(nullable: false),
                        MedicalPolicyNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Genders", t => t.Gender_Id)
                .ForeignKey("dbo.Streets", t => t.Street_Id)
                .Index(t => t.Id)
                .Index(t => t.Gender_Id)
                .Index(t => t.Street_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Street_Id", "dbo.Streets");
            DropForeignKey("dbo.Patients", "Gender_Id", "dbo.Genders");
            DropForeignKey("dbo.Patients", "Id", "dbo.Users");
            DropForeignKey("dbo.Doctors", "Specialization_Id", "dbo.Specializations");
            DropForeignKey("dbo.Doctors", "Region_Id", "dbo.Regions");
            DropForeignKey("dbo.Doctors", "Id", "dbo.Users");
            DropForeignKey("dbo.Admins", "Id", "dbo.Users");
            DropForeignKey("dbo.VoucherForAppointments", "State_Id", "dbo.VoucherStates");
            DropForeignKey("dbo.VoucherForAppointments", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.VoucherForAppointments", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.Visits", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Visits", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.ScheduleSlots", "Weekday_Id", "dbo.Weekdays");
            DropForeignKey("dbo.ScheduleSlots", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.Streets", "Region_Id", "dbo.Regions");
            DropIndex("dbo.Patients", new[] { "Street_Id" });
            DropIndex("dbo.Patients", new[] { "Gender_Id" });
            DropIndex("dbo.Patients", new[] { "Id" });
            DropIndex("dbo.Doctors", new[] { "Specialization_Id" });
            DropIndex("dbo.Doctors", new[] { "Region_Id" });
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropIndex("dbo.Admins", new[] { "Id" });
            DropIndex("dbo.VoucherForAppointments", new[] { "State_Id" });
            DropIndex("dbo.VoucherForAppointments", new[] { "Patient_Id" });
            DropIndex("dbo.VoucherForAppointments", new[] { "Doctor_Id" });
            DropIndex("dbo.Visits", new[] { "Patient_Id" });
            DropIndex("dbo.Visits", new[] { "Doctor_Id" });
            DropIndex("dbo.ScheduleSlots", new[] { "Weekday_Id" });
            DropIndex("dbo.ScheduleSlots", new[] { "Doctor_Id" });
            DropIndex("dbo.Streets", new[] { "Region_Id" });
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Admins");
            DropTable("dbo.VoucherStates");
            DropTable("dbo.VoucherForAppointments");
            DropTable("dbo.Genders");
            DropTable("dbo.Visits");
            DropTable("dbo.Specializations");
            DropTable("dbo.Weekdays");
            DropTable("dbo.ScheduleSlots");
            DropTable("dbo.Streets");
            DropTable("dbo.Regions");
            DropTable("dbo.Users");
        }
    }
}
