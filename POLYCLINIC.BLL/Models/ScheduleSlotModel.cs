using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.BLL.Models
{
    public class ScheduleSlotModel : BaseModel<ScheduleSlot>
    {
        public string StartTimeFormated => Entity.StartTime.ToString(@"hh\:mm");
    }
}
