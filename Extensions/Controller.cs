using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FBPortal.Domain.Entities;
using FBPortal.Domain.Concrete;

namespace FBPortal.WebUI.Extensions
{
    public static class ControllerExention
    {
        public static ApplicationUser CurrentUser(this Controller controller)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = um.FindById(HttpContext.Current.User.Identity.GetUserId());

            if (user == null) user = new ApplicationUser();

            return user;
        }
    }
}