using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace blogHaber.Models
{
    //bu dosysa bittikten sonra global.asax a koymayı unutma
    public class HaberInitializer : CreateDatabaseIfNotExists<HaberContext>  // DropCreateDatabaseIfModelChanges model de bir değişiklik olunca veritabanını yeniden oluşturun.
    {
        protected override void Seed(HaberContext context)
        {
            List<Category> kategoriler = new List<Category>()
            {
                new Category()  {KategoriAdi="Yaşam" },
                new Category()  {KategoriAdi="Kültür" },
                new Category()  {KategoriAdi="Sağlık" },

            };
            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();

            List<About> aboutlar = new List<About>()
            {
                new About()  {Hakkimda="Behiye Sude Doğan" }
               

            };
            foreach (var item in aboutlar)
            {
                context.Aboutlar.Add(item);
            }
            context.SaveChanges();

            List<Haber> haberler = new List<Haber>()
            {
                new Haber(){Baslik="Gaz depolamada önemli eşik aşıldı",Aciklama="Doğal gaz arz güvenliği için depolamada önemli bir eşik geçildi. Silivri Yeraltı Depolama Tesisi’nde kapasite artırımı için açılan kuyuların ikisinde gaz yakma testi tamamlandı.",EklenmeTarihi=DateTime.Now.AddDays(-10),Anasayfa=true,Onay=true,Giris="Son yıllarda yoğunlaşan doğal gaz arz güvenliği çalışmaları kapsamında doğal gazın boru hatları ile giriş kapasiteleri artırılırken, deniz üzerinden girişi sağlanan LNG (sıvılaştırılmış doğal gaz) tesislerinde geçen yıl devreye alınan ve Türkiye’nin ilk Yüzer LNG depolama ve gazlaştırma (FSRU) gemisi Ertuğrul Gazi başı çekiyor. Hatay Dörtyol’da hizmet eden Ertuğrul Gazi FSRU Gemisi’nin yanında bu yıl sonunda açılması planlanan Saros FSRU projesi de doğal gaz giriş noktasının çeşitlendirilmesinde kritik rol oynayacak.",Govde="Tuz Gölü Doğal Gaz Depolama Tesisinde genişleme çalışmalarıyla 1.2 milyar metreküp kapasitenin, 2023’te 5.4 milyar metreküpe, 40 milyon metreküp olan günlük geri üretim kapasitesinin ise 80 milyon metreküpe çıkması planlanıyor. Türkiye’nin ilk yer altı doğal gaz depolama tesisi olan Silivri Yeraltı Doğal Gaz Depolama Tesisi’nin de halihazırda 3.2 milyar metreküp depolama kapasitesi bulunuyor. Yaklaşık 4 yıl önce başlatılan genişleme çalışmalarında yeni yüzey tesislerine boru inşası, deniz platformların inşası ve kuyu sondajların yapılması yer alıyor. Tesiste şu anda iki deniz platformunun inşası tamamlandı. Bu platformların her birinde 9 olmak üzere toplamda 18 kuyunun sondajı gerçekleşti. Bu kuyulardan ikisinde ise gaz akış (yakma) testi başarıyla tamamlandı. Böylece genişleme çalışmalarında kritik bir viraj geçilmiş oldu. Çalışmalarla 28 milyon metreküp olan günlük geri üretim kapasitesi de 75 milyon metreküpe çıkacak. Mart 2022 sonu itibarıyla yüzde 60’ı tamamlanan projenin 2022 yılı içerisinde bitmesi hedefleniyor. Enerji ve Tabii Kaynaklar Bakanı Fatih Dönmez, Türkiye’nin doğal gazda yüzde 99 dışa bağımlı olduğuna dikkat çekerek, “Ya keşif yapacaksınız ya da böyle depoların sayısını artıracaksınız. Karadeniz’deki 540 milyar metreküplük gaz keşfimizin yanında karadaki depolama tesislerimizin de kapasitelerini artırıyoruz. Yıl sonunda hem Silivri’deki tesisimizin hem de Saros FSRU’yu açacağız” diye konuştu. Dönmez, doğal gaz depolama için gaz akış testi yapılan iki kuyuda gaz üretebilir hale gelindiğini ifade ederek, “Yakma işlemi de başarıyla sondajın tamamladığını, kuyuların çalışmaya hazır hale geldiğinin göstergesi. Yaz aylarında 18 kuyunun tamamında gaz akış testi tamamlanacak. Bu yatırımlarla Türkiye, hem LNG hem de depolamada dünyada nadir ülkelerden oluyor. Bunlar bize fiyat ve arz güvenliği esnekliği sağlayacak” dedi.",Resim="1.jpg",CategoryId=1},

             };

            foreach (var item in haberler)
            {
                context.Haberler.Add(item);
            }
            context.SaveChanges();




            base.Seed(context);
        }
    }
}
