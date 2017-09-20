# aspnet-mvc5-external-spa

Demonstrates loading cshtml from a virtual provider.
In this case I have an abstracted store, which is implemented by pulling a json from rawgit.com.

[Here](https://rawgit.com/ghstahl/asset-repo/master/aspnet-mvc5/views.json) is the Views data.  
[Here](https://rawgit.com/ghstahl/asset-repo/master/aspnet-mvc5/external.spa.config.json) is the External SPA Data.

```
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
```
