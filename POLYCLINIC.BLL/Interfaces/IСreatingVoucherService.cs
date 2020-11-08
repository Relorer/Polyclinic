using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.BLL.Interfaces
{
    public interface IСreatingVoucherService
    {
        Specialization Specialization { get; set; }
        Doctor Doctor { get; set; }
        ScheduleSlot ScheduleSlot { get; set; }
        DateTime Date { get; set; }

        void Create();
        void Clear();
    }
}
