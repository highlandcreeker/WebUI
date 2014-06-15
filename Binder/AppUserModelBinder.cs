using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FBPortal.Domain.Concrete;
using FBPortal.Domain.Entities;

namespace FBPortal.WebUI.Binder
{
    public class AppUserModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = new ApplicationUser();
            if (controllerContext.HttpContext.User.Identity.GetUserId() != null) { user=um.FindById(controllerContext.HttpContext.User.Identity.GetUserId()); }
            
            return user;
        }
    }
}