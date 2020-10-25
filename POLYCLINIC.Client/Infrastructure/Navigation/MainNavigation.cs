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
            CurrentPage = pages[typeof(Authorization).Name];
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
    }
}
