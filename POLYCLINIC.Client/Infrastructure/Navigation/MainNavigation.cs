using POLYCLINIC.BLL;
using POLYCLINIC.Client.Pages;
using System;

namespace POLYCLINIC.Client.Infrastructure
{
    class MainNavigation : BaseNavigation
    {
        private static volatile MainNavigation instance;
        private static object syncRoot = new Object();

        private MainNavigation()
        {
            pages.Add(typeof(Authorization).Name, new Authorization());
            pages.Add(typeof(Patient).Name, new Patient());
            Navigate(AuthorizationService.GetCurrentUser());
        }

        public static MainNavigation Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MainNavigation();
                    }
                }
                return instance;
            }
        }

        public void Navigate(Data.Entities.User user)
        {
            if (user == null)
            {
                CurrentPage = pages[typeof(Authorization).Name];
            }
            else if (user is Data.Entities.Doctor)
            {
                AuthorizationService.LogOut();
                throw new Exception("Doctor is not supported");
            }
            else if (user is Data.Entities.Patient)
            {
                CurrentPage = pages[typeof(Patient).Name];
            }
            else if (user is Data.Entities.Admin)
            {
                AuthorizationService.LogOut();
                throw new Exception("Admin is not supported");
            }
        }

    }
}
