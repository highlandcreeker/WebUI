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

namespace FBPortal.WebUI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 50;

        public ProductController(IProductRepository repo) { this.repository = repo; }

        public async Task<ActionResult> List(int page = 1)
        {
            ProductViewModel model = new ProductViewModel
            {
                Products = await repository.Products
                              .OrderBy(p => p.Number)
                .Skip((page - 1) * PageSize)
                .Take(PageSize).ToListAsync(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },

            };

            return View(model);
        }

        // GET: /Product/Details/5
        public async Task<ActionResult> Details(int? id, string returnUrl)
        {
            ProductViewModel model = new ProductViewModel();
            model.ReturnUrl = returnUrl;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.Product = await repository.Products.SingleAsync(p => p.ID == id);
            if (model.Product == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Number,Description,Brand,Quantity,Weight,PackageTypeCode,PackageType,Price,DateAdded")] Product product)
        {
            if (ModelState.IsValid)
            {
                await repository.Create(product);

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await repository.Products.SingleAsync(p => p.ID == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Number,Description,Brand,Quantity,Weight,PackageTypeCode,PackageType,Price,DateAdded")] Product product)
        {
            if (ModelState.IsValid)
            {
                await repository.Edit(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = await repository.Products.SingleAsync(p => p.ID == id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await repository.Products.SingleAsync(p => p.ID == id);

            await repository.Delete(product);

            return RedirectToAction("Index");
        }

    }
}
