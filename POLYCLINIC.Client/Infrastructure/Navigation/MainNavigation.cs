using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Interfaces;
using POLYCLINIC.Client.Pages;
using POLYCLINIC.Client.Views.Pages;
using POLYCLINIC.Data.Entities;
using System;

namespace POLYCLINIC.Client.Infrastructure
{
    class MainNavigation : BaseNavigation, IMainNavigation
    {
        public void Navigate(User user)
        {
            if (user == null)
            {
                Navigate(new Authorization());
            }
            else if (user is Doctor)
            {
                throw new Exception("Doctor is not supported");
            }
            else if (user is Patient)
            {
                Navigate(new PatientPage());
            }
            else if (user is Admin)
            {
                Navigate(new AdminPage());
            }
        }

    }
}
