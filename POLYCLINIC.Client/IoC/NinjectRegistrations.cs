using Ninject.Modules;
using POLYCLINIC.BLL;
using POLYCLINIC.BLL.Interfaces;
using POLYCLINIC.Client.Infrastructure;
using POLYCLINIC.Client.Interfaces;

namespace POLYCLINIC.Client
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBaseManager>().To<BaseManager>().InSingletonScope();
            Bind<IAuthorizationService>().To<AuthorizationService>().InSingletonScope();
            Bind<IMainNavigation>().To<MainNavigation>().InSingletonScope();
        }
    }
}
