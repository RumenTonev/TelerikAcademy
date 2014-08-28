using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCAjaxDemo.Data;
using MVCAjaxDemo.Models;

namespace MVCAjaxDemo.Controllers
{
    public class ActorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Actors
      

        // GET: /Actors/
        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        // GET: /Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadingActor LeadingActor = db.Actors.Find(id);
            if (LeadingActor == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", LeadingActor);
        }

        // GET: /Actors/Create
        public ActionResult Create()
        {
            return PartialView("_Create", new LeadingActor());
        }

        // POST: /Actors/Create
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeadingActor LeadingActor)
        {
            if (ModelState.IsValid)
            {
                db.Actors.Add(LeadingActor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(LeadingActor);
        }

        // GET: /Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadingActor LeadingActor = db.Actors.Find(id);
            if (LeadingActor == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit", LeadingActor);
        }

        // POST: /Actors/Edit/5
        // To protect from over posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 
        // Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeadingActor LeadingActor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(LeadingActor).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("Index");
                //return View(LeadingActor);
            }
            return View(LeadingActor);
        }

        // GET: /Actors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LeadingActor LeadingActor = db.Actors.Find(id);
            if (LeadingActor == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", LeadingActor);
        }

        // POST: /Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var movie in db.Movies.Where(m => m.LeadingMaleActorId == id))
            {
              //  movie.LeadingMaleActorId = null;
            }

            LeadingActor LeadingActor = db.Actors.Find(id);
            db.Actors.Remove(LeadingActor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
