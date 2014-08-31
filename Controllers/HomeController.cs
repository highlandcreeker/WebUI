using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPortal.Domain.Abstract;
using FBPortal.Domain.Entities;
using FBPortal.WebUI.Extensions;
using FBPortal.WebUI.Models;

namespace FBPortal.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        private IInvoiceRepository repository;

        public HomeController(IInvoiceRepository repo) { repository = repo; }

        public ActionResult Index()
        {
            IEnumerable<Invoice> invoices = new List<Invoice>();
            Guid clientId = this.CurrentUser().ClientId;
            invoices = repository.Invoices.Where(i => i.ClientId.Equals(clientId));

            return View(invoices);
        }



        public PartialViewResult ClientSummary()
        {
            
            ClientViewModel vm = new ClientViewModel { Name = this.CurrentUser().Client.Name , Balance = this.CurrentUser().Client.Balance.ToString("c")};
            return PartialView(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "F&B Portal is our answer to the anarchy that becomes the life of a restaurant manager/owner.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Let us know how we're doing.";

            return View();
        }
    }
}