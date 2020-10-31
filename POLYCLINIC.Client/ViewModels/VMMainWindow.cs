using POLYCLINIC.BLL;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Interfaces;
using System;
using System.Windows.Controls;

namespace POLYCLINIC.Client.ViewModels
{
    class VMMainWindow : VMBase
    {
        private readonly IMainNavigation navigation;
        private readonly IAuthorizationService authorization;

        public Page CurrentPage => navigation.CurrentPage;

        public VMMainWindow()
        {
            navigation = IoC.Get<IMainNavigation>();
            authorization = IoC.Get<IAuthorizationService>();

            navigation.CurrentPageChanged += (sender, e) => OnPropertyChanged(e.PropertyName);
            navigation.Navigate(authorization.GetCurrentUser());
        }

    }
}
