using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System.Web.Http;
using Sorocaba.Commons.Ninject;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof($rootnamespace$.App_Start.NinjectConfig), "Stop")]

namespace $rootnamespace$.App_Start {
    public static class NinjectConfig {

        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()  {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop() {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel() {
            IKernel kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            ConfigureBindings(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        private static void ConfigureBindings(IKernel kernel) {
        }
    }
}
