using Ninject;
using Ninject.Parameters;

namespace POLYCLINIC.Client
{
    public static class IoC
    {
        private static IKernel Kernel = new StandardKernel(new NinjectRegistrations());

        public static T Get<T>(params IParameter[] parameters)
        {
            return Kernel.Get<T>(parameters);
        }
    }

}
