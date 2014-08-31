using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBPortal.Domain.Entities;
using FBPortal.Domain.Abstract;
using System.Threading.Tasks;

namespace FBPortal.WebUI.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        IInvoiceRepository repository;
        
        public InvoiceController( IInvoiceRepository repo) { repository = repo; }
        //
        // GET: /Invoice/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guid ClientId,[Bind(Include="Name,Vendor,DateAdded,AmountPaid")] Invoice invoice) {
            invoice.ClientId = ClientId;

            if (ModelState.IsValid) {
                 await repository.Create(invoice);
            }

            return View();
        }
	}
}