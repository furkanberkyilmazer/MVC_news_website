using blogHaber.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blogHaber.Controllers
{
    public class HomeController : Controller
    {

        private HaberContext context = new HaberContext();

        public ActionResult Index()
        {


            //   return View(context.Bloglar.ToList()); ilk yaptığımız 

            var haberler = context.Haberler.OrderByDescending(i => i.EklenmeTarihi).Where(i => i.Onay == true && i.Anasayfa == true)
                             .Select(i => new HaberModel()
                             {
                                 Id = i.Id,
                                 Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                                 Aciklama = i.Aciklama.Length > 150 ? i.Aciklama.Substring(0, 147) + "..." : i.Aciklama,
                                 EklenmeTarihi = i.EklenmeTarihi,
                                 Anasayfa = i.Anasayfa,
                                 Onay = i.Onay,
                                 Resim = i.Resim == null ? "default.jpg" : i.Resim,
                             });

            return View(haberler.ToList());
        }
    }
}