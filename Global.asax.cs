using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FBPortal.WebUI.Infrastructure;
using FBPortal.Domain.Entities;
using FBPortal.WebUI.Binder;

namespace FBPortal.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(ApplicationUser), new AppUserModelBinder());
           
            
        }
    }
}
