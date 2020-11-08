using Ninject.Modules;
using POLYCLINIC.BLL;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.BLL.Services;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Infrastructure.Navigation;
using POLYCLINIC.Client.Interfaces;
using System;

namespace POLYCLINIC.Client
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBaseManager>().To<BaseManager>().InSingletonScope();
            Bind<IAuthorizationService>().To<AuthorizationService>().InSingletonScope();
            Bind<IMainNavigation>().To<MainNavigation>().InSingletonScope();
            Bind<IAppointmentNavigation>().To<AppointmentNavigation>().InSingletonScope();
            Bind<ICancleVoucherService>().To<CancelVoucherService>();
            Bind<IСreatingVoucherService>().To<СreatingVoucherService>().InSingletonScope();
            Bind<IWeekViewerService>().To<WeekViewerService>().WithConstructorArgument(DateTime.Now);
        }
    }
}
