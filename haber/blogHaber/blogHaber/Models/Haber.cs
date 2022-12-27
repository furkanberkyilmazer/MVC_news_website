using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blogHaber.Models
{
    public class Haber
    {

        public int Id { get; set; }
        public string Baslik { get; set; }


        public string Aciklama { get; set; }

        public string Resim { get; set; }

        [AllowHtml]
        public string Giris { get; set; }

        [AllowHtml]
        public string Govde { get; set; }
        //public int? sayi { get; set; } int? null geçilebilmesini sağlıyor

        public DateTime EklenmeTarihi { get; set; }

        public bool Onay { get; set; }

        public bool Anasayfa { get; set; }

       /* [NotMapped]  //veri tabanında bir şeyle eşleşmeyeceği anlamına gelir
        public List<HttpPostedFileBase> Resimler { get; set; }
       */

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}