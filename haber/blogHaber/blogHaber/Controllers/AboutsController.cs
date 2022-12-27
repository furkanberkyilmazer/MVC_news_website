using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blogHaber.Models;

namespace blogHaber.Controllers
{
    public class AboutsController : Controller
    {
        private HaberContext db = new HaberContext();


        [Authorize(Roles = "admin")]
        // GET: Abouts
        public ActionResult Index()
        {
            return View(db.Aboutlar.ToList());
        }



        // GET: Abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Aboutlar.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        [Authorize(Roles = "admin")]
        // GET: Abouts/Create
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        // POST: Abouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Hakkimda")] About about)
        {
            if (ModelState.IsValid)
            {
                db.Aboutlar.Add(about);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(about);
        }


        [Authorize(Roles = "admin")]
        // GET: Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Aboutlar.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }


        [Authorize(Roles = "admin")]
        // POST: Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Hakkimda")] About about)
        {
            if (ModelState.IsValid)
            {
                db.Entry(about).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);
        }


        [Authorize(Roles = "admin")]
        // GET: Abouts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Aboutlar.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }


        [Authorize(Roles = "admin")]
        // POST: Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            About about = db.Aboutlar.Find(id);
            db.Aboutlar.Remove(about);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
