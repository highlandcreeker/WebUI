using Ninject;
using System.Web;
using System.Web.Mvc;
using FBPortal.Domain.Abstract;
using FBPortal.Domain.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FBPortal.WebUI.Models;
using Microsoft.Owin.Host.SystemWeb;
using FBPortal.Domain.Entities;

namespace FBPortal.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();

        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            switch (controllerType.Name)
            {
                case "AccountController":

                    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

                    var authmgr = requestContext.HttpContext.GetOwinContext().Authentication;
                    return new FBPortal.WebUI.Controllers.AccountController(um, authmgr);
                default:
                    return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
            }

        }

        private void AddBindings()
        {

            ninjectKernel.Bind<IInvoiceRepository>().To<InvoiceRepository>();
            ninjectKernel.Bind<IClientRepository>().To<ClientRepository>();
            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            ninjectKernel.Bind<IVendorRepository>().To<VendorRepository>();
            ninjectKernel.Bind<IPriceListRepository>().To<PriceListRepository>();
            //ninjectKernel.Bind<UserManager<ApplicationUser>>().ToConstant(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())));

        }
    }
}