using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FBPortal.Domain.Entities;
using FBPortal.Domain.Concrete;
using FBPortal.Domain.Abstract;
using FBPortal.WebUI.Models;
using FBPortal.WebUI.Extensions;


namespace FBPortal.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientController : Controller
    {
        private IClientRepository repository;

        public ClientController(IClientRepository repo) { repository = repo; }

        // GET: /Client/
        public async Task<ActionResult> Index()
        {
            return View(await repository.Clients.ToListAsync());
        }

        // GET: /Client/Details/5
        public async Task<ActionResult> Invoices(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await repository.Clients.SingleAsync(c => c.ClientId == (Guid)id);

            if (client == null)
            {
                return HttpNotFound();
            }

            ClientViewModel vm = new ClientViewModel { ClientId = client.ClientId, Name = client.Name, Balance = client.Balance.ToString("c"), Invoices =client.Invoices };
            return View(vm);
        }

        // GET: /Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,Name")] Client client)
        {
            if (ModelState.IsValid)
            {
                Client c = await repository.Create(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: /Client/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = await repository.Clients.SingleAsync(c => c.ClientId.Equals((Guid)id));
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,Name,Balance")] Client client)
        {
            if (ModelState.IsValid)
            {
                await repository.Edit(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: /Client/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await repository.Clients.SingleAsync(c => c.ClientId.Equals((Guid)id));

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: /Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await repository.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult CreateInvoice(Guid clientId)
        {
            var clientInvoice = new Invoice { ClientId = clientId };
            var client = repository.Clients.Where(c => c.ClientId == clientId).First();

            ViewBag.ClientBalance = client.Balance.ToString("c"); ;

            return View(clientInvoice);
        }

        [HttpPost, ActionName("CreateInvoice")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInvoice(Guid clientId, [Bind(Include = "Name,Vendor,DateAdded,AmountPaid")] Invoice invoice)
        {
            invoice.ClientId = clientId;

            if (ModelState.IsValid)
            {
                await repository.CreateInvoice(invoice);
            }
            @TempData.Add("SuccessMessage",invoice.Name + " created successfully.");
            return RedirectToAction("CreateInvoice", new { clientId = clientId });
        }

        public async Task<ActionResult> Expenses(Guid clientId) {
            var client = await repository.Clients.SingleAsync(c=>c.ClientId == clientId);
            var expenses = client.ClientPeriods;

            return new HttpNotFoundResult();
            //return View(expenses);
        }

    }
}
