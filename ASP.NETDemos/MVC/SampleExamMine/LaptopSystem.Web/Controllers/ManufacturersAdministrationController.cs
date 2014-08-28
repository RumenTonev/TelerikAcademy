using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaptopSystem.Data;
using LaptopSystem.Models;

namespace LaptopSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManufacturersAdministrationController : BaseController
    {
        public ManufacturersAdministrationController(IUowData data)
            : base(data)
            {
                //this.Data = data;
            }
        // GET: /ManufacturersAdministration/
        public ActionResult Index()
        {
            return View(this.Data.Manufacturers.All().ToList());
        }

        // GET: /ManufacturersAdministration/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = this.Data.Manufacturers.GetById(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // GET: /ManufacturersAdministration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ManufacturersAdministration/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturer manufacturer)
        {
            if (this.Data.Manufacturers.All().Any(x => x.Name == manufacturer.Name))
            {
                ModelState.AddModelError("Name", "There is already a manufacturer with that name.");
            }

            if (ModelState.IsValid)
            {
                this.Data.Manufacturers.Add(manufacturer);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturer);
        }

        // GET: /ManufacturersAdministration/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = this.Data.Manufacturers.GetById(id);

            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: /ManufacturersAdministration/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manufacturer manufacturer)
        {
            if (this.Data.Manufacturers.All().Any(x => x.Name == manufacturer.Name))
            {
                ModelState.AddModelError("Name", "There is already a manufacturer with that name.");
            }

            if (ModelState.IsValid)
            {
                this.Data.Manufacturers.Update(manufacturer);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturer);
        }

        // GET: /ManufacturersAdministration/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer manufacturer = this.Data.Manufacturers.GetById(id);
            if (manufacturer == null)
            {
                return HttpNotFound();
            }
            return View(manufacturer);
        }

        // POST: /ManufacturersAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturer manufacturer = this.Data.Manufacturers.GetById(id);

            foreach (var laptop in manufacturer.Laptops.ToList())
            {
                foreach (var vote in laptop.Votes.ToList())
                {
                    this.Data.Votes.Delete(vote);
                }

                foreach (var comment in laptop.Comments.ToList())
                {
                    this.Data.Comments.Delete(comment);
                }

                this.Data.Laptops.Delete(laptop);
            }

            this.Data.Manufacturers.Delete(manufacturer);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.Data.Dispose();
            base.Dispose(disposing);
        }
    }
}
