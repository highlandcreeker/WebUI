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
    public class PriceListController : Controller
    {
        private IPriceListRepository repository;

        public PriceListController(IPriceListRepository repo) { repository = repo; }

        // GET: /PriceList/
        public async Task<ActionResult> Index()
        {
            return View(await repository.PriceLists.ToListAsync());
        }

        // GET: /PriceList/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList pricelist = await repository.PriceLists.SingleAsync(pl => pl.ID == id);
            if (pricelist == null)
            {
                return HttpNotFound();
            }
            return View(pricelist);
        }

        // GET: /PriceList/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PriceList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Description,DateAdded,File,FilePath")] PriceList pricelist)
        {
            if (ModelState.IsValid)
            {
                await repository.Create(pricelist);

                return RedirectToAction("Index");
            }

            return View(pricelist);
        }

        // GET: /PriceList/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList pricelist = await repository.PriceLists.SingleAsync(pl => pl.ID == id);

            if (pricelist == null)
            {
                return HttpNotFound();
            }
            return View(pricelist);
        }

        // POST: /PriceList/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Description,DateAdded,File,FilePath")] PriceList pricelist)
        {
            if (ModelState.IsValid)
            {
                await repository.Edit(pricelist);
                return RedirectToAction("Index");
            }
            return View(pricelist);
        }

        // GET: /PriceList/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList pricelist = await repository.PriceLists.SingleAsync();
            if (pricelist == null)
            {
                return HttpNotFound();
            }
            return View(pricelist);
        }

        // POST: /PriceList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PriceList pricelist = await repository.PriceLists.SingleAsync(pl => pl.ID == id);
            await repository.Delete(pricelist);
            return RedirectToAction("Index");
        }
       
    }
}
