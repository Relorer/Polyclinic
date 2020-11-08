using POLYCLINIC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLYCLINIC.Client.Interfaces
{
    public interface IMainNavigation : INavigation
    {
        void Navigate(User user);
    }
}
