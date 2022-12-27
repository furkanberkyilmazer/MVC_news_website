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
    public class ImageController : Controller
    {
        private HaberContext db = new HaberContext();

        [Authorize(Roles = "admin")]
        // GET: Image
        public ActionResult Index()
        {
            var resimler = db.Resimler.Include(i => i.Haber);
            return View(resimler.ToList());
        }

        [Authorize(Roles = "admin")]
        // GET: Image/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Resimler.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        [Authorize(Roles = "admin")]
        // GET: Image/Create
        public ActionResult Create()
        {
            ViewBag.HaberId = new SelectList(db.Haberler, "Id", "Baslik");
            return View();
        }

        [Authorize(Roles = "admin")]
        // POST: Image/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Resim,HaberId")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Resimler.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HaberId = new SelectList(db.Haberler, "Id", "Baslik", image.HaberId);
            return View(image);
        }

        [Authorize(Roles = "admin")]
        // GET: Image/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Resimler.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            ViewBag.HaberId = new SelectList(db.Haberler, "Id", "Baslik", image.HaberId);
            return View(image);
        }

        [Authorize(Roles = "admin")]
        // POST: Image/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Resim,HaberId")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HaberId = new SelectList(db.Haberler, "Id", "Baslik", image.HaberId);
            return View(image);
        }

        [Authorize(Roles = "admin")]
        // GET: Image/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Resimler.Find(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        [Authorize(Roles = "admin")]
        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image image = db.Resimler.Find(id);
            db.Resimler.Remove(image);
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
