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

namespace FBPortal.WebUI.Controllers
{
    public class VendorController : Controller
    {
        private IVendorRepository repository;

        public VendorController(IVendorRepository repo) { this.repository = repo; }

        // GET: /Vendor/
        public async Task<ActionResult> Index()
        {
            return View(await repository.Vendors.ToListAsync());
        }

        // GET: /Vendor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = await repository.Vendors.SingleAsync(v => v.ID == id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: /Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Description")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                Vendor v = await repository.Create(vendor);
                return RedirectToAction("Index");
            }

            return View(vendor);
        }

        // GET: /Vendor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = await repository.Vendors.SingleAsync(v => v.ID == id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: /Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Description,DateAdded")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                await repository.Edit(vendor);
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        // GET: /Vendor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendor vendor = await repository.Vendors.SingleAsync(v => v.ID == id);

            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // POST: /Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await repository.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
