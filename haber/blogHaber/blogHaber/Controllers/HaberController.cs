using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blogHaber.Models;

namespace blogHaber.Controllers
{
    public class HaberController : Controller
    {
        private HaberContext db = new HaberContext();

        public ActionResult List(int? id, string AnahtarKelime)
        {
             

            var haberler = db.Haberler.OrderByDescending(i => i.EklenmeTarihi)
                .Where(i => i.Onay == true && i.Anasayfa == true)
                             .Select(i => new HaberModel()
                             {
                                 Id = i.Id,
                                 Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                                 Aciklama = i.Aciklama.Length > 150 ? i.Aciklama.Substring(0,147)+"...":i.Aciklama,
                                 EklenmeTarihi = i.EklenmeTarihi,
                                 Anasayfa = i.Anasayfa,
                                 Onay = i.Onay,
                                 Resim = i.Resim==null?"default.jpg":i.Resim,
                                 CategoryId = i.CategoryId
                             }).AsQueryable(); //where ekleyebilmek için dah asonra
            if (string.IsNullOrEmpty(AnahtarKelime) == false)//boş olup olmama kontrolü
            {
                //contains arama
                haberler = haberler.Where(i => i.Baslik.Contains(AnahtarKelime) || i.Aciklama.Contains(AnahtarKelime));
            }
            if (id != null)
            {
                haberler = haberler.Where(i => i.CategoryId == id);
            }
            
            return View(haberler.ToList());

        }

        [Authorize(Roles = "admin")]
        // GET: Haber
        public ActionResult Index()
        {
            var haberler = db.Haberler.Include(b => b.Category).OrderByDescending(i => i.EklenmeTarihi);
            //orderbydescending i sonradan ekledik eklenme tarihine göreazalan şekilde sıralıyor ki şeni eklenen en üstte gözüküyor.

            return View(haberler.ToList());
        }


      
        // GET: Haber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haber haber = db.Haberler.Find(id);
            if (haber == null)
            {
                return HttpNotFound();
            }
            return View(haber);
        }


        [Authorize(Roles = "admin")]
        // GET: Haber/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi");
            return View();
        }


        [Authorize(Roles = "admin")]
        // POST: Haber/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Baslik,Aciklama,Resim,Giris,Govde,CategoryId")] Haber haber, [Bind(Include = "Resim,HaberId")] Image image)
        {
            if (ModelState.IsValid)
            {
                haber.EklenmeTarihi = DateTime.Now;
                haber.Onay = false;  //ikisi eklemesekte varsayılan false olarak ekler
                haber.Anasayfa = false;
                if (Request.Files.Count > 0)
                {
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                   // string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/Image/" + filename;//+ uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    haber.Resim =  filename;//+ uzanti;
                    image.Resim =  filename;//+ uzanti;
                    image.HaberId = haber.Id;
                }
                db.Haberler.Add(haber);
                db.Resimler.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", haber.CategoryId);
            return View(haber);
        }


        [Authorize(Roles = "admin")]
        // GET: Haber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haber haber = db.Haberler.Find(id);
            if (haber == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", haber.CategoryId);
            return View(haber);
        }


        [Authorize(Roles = "admin")]
        // POST: Haber/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Resim,Giris,Govde,Onay,Anasayfa,CategoryId")] Haber haber, [Bind(Include = "Resim,HaberId")] Image image ,List<HttpPostedFileBase> files )
        {
         
           
            if (ModelState.IsValid)
            { 
                var entity = db.Haberler.Find(haber.Id); 
                // db.Entry(haber).State = EntityState.Modified; ekleme tarihi güncellenmeyeceği için sadece istediğimiz alanlar için bir sorgu yapıcaz
                if (entity != null)
                {
                    entity.Baslik = haber.Baslik;
                    entity.Aciklama = haber.Aciklama;
                    entity.Resim = haber.Resim;
                    entity.Giris = haber.Giris;
                    entity.Govde = haber.Govde;
                    entity.Onay = haber.Onay;
                    entity.Anasayfa = haber.Anasayfa;
                    entity.CategoryId = haber.CategoryId;

                    /*  if (Request.Files.Count > 0)
                      {
                          for (int i = 0; i <= Request.Files.Count; i++)
                          {
                              string filename = Path.GetFileName(Request.Files[i].FileName);
                             string uzanti = Path.GetExtension(Request.Files[i].FileName);
                             string yol = "~/Image/" + filename + uzanti;
                             Request.Files[i].SaveAs(Server.MapPath(yol));

                            image.Resim =  filename + uzanti;
                            image.HaberId = haber.Id;

                          }

                      }*/
                    
                    foreach (var file in files)//multi foto yüklemek için
                    {
                        if (file!=null && file.ContentLength>0)
                        {
                            string filename = Path.GetFileName(file.FileName);
                            // string uzanti = Path.GetExtension(Request.Files[0].FileName);
                            string yol = "~/Image/" + filename;//+ uzanti;
                           file.SaveAs(Server.MapPath(yol));

                          //file.SaveAs(Path.Combine(Server.MapPath("~/Image/"),Guid.NewGuid()+Path.GetExtension(file.FileName)));
                            image.Resim = file.FileName;
                            image.HaberId = haber.Id; 
                            db.Resimler.Add(image);
                            db.SaveChanges();
                        }
                    }


                    db.SaveChanges();
                    TempData["Haber"] = entity; //wiev e bilgi taşıma
                    return RedirectToAction("Index");
                }
              
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "CategoryId", "KategoriAdi", haber.CategoryId);
            return View(haber);
        }


        [Authorize(Roles = "admin")]
        // GET: Haber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haber haber = db.Haberler.Find(id);
            if (haber == null)
            {
                return HttpNotFound();
            }
            return View(haber);
        }

        // POST: Haber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Haber haber = db.Haberler.Find(id);
            db.Haberler.Remove(haber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Blog/Details/5
       
        public PartialViewResult GaleriPartial(int? id)
        {
            if (id==null)
            {
                return PartialView(db.Resimler.ToList());
            }
            else
            {
                  var resimler = db.Resimler
                    .Where(i => i.HaberId == id); //where ekleyebilmek için daha sonra

               return PartialView(resimler.ToList());  
            }
               

               
           
          
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
