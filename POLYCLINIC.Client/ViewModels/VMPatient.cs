using POLYCLINIC.Client.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Client.ViewModels
{
    class VMPatient : VMBase
    {
        private LogOutCommand logOutCommand;
        public LogOutCommand LogOutCommand => logOutCommand ??
                  (logOutCommand = new LogOutCommand());
    }
}
