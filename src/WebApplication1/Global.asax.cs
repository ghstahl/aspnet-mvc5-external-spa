using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using VirtCo.Providers;
using VirtCo.Providers.Stores;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var remoteRazorLocationStore = new RemoteRazorLocationStore();
            remoteRazorLocationStore.LoadRemoteDataAsync("https://rawgit.com/ghstahl/asset-repo/master/aspnet-mvc5/views.json").GetAwaiter().GetResult();

            HostingEnvironment.RegisterVirtualPathProvider(new ViewPathProvider(remoteRazorLocationStore));

            var remoteStaticExternalSpaStore = new RemoteStaticExternalSpaStore(
                "https://rawgit.com/ghstahl/asset-repo/master/aspnet-mvc5/external.spa.config.json");
            var records = remoteStaticExternalSpaStore.GetRemoteDataAsync().GetAwaiter().GetResult();
            foreach (var spa in records.Spas)
            {
                remoteStaticExternalSpaStore.AddRecord(spa);
            }

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(remoteStaticExternalSpaStore).As<IExternalSpaStore>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
