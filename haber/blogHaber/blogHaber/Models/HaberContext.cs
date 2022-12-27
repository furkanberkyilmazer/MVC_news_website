using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace blogHaber.Models
{
    public class HaberContext:DbContext
    {
        //:base("blogVt") bunu eklersen bu isimde oluşturur veri tabanını veya
        //connection string eklersen webconfige gidip blogVt ye uyan connection stringi bulur ve belirttiğin isimde veritabanı oluşturur
        //en sağlıklısı connection stringle yapmak çünkü bu sayede veritabanı ekleyebilir ve çıkartabiliriz
        //yada yayınlayacağımız zaman uzaktaki veritabanına bağlanabiliriz.
        public HaberContext() : base("haberDb")
        {


        }
        public DbSet<Haber> Haberler { get; set; }

        public DbSet<About> Aboutlar { get; set; }
        public DbSet<Category> Kategoriler { get; set; }
        public DbSet<Image> Resimler { get; set; }
        public object Image { get; internal set; }
    }
}