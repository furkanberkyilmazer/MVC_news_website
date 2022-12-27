using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace blogHaber.Identity
{
    //bundan sonra controlleri açmadan Startup1 işlemi yapılır app_starta ve normal class değil owin startup item ekleden yapılır.
    //bu dosysa bittikten sonra global.asax a koymayı unutma
    public class IdentityInitializer: CreateDatabaseIfNotExists<IdentityDataContext> //yoksa oluştur
    {
        protected override void Seed(IdentityDataContext context)
        {
            //roller
            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "admin", Description = "admin rolü" }; ;
                manager.Create(role);
            }

            if (!context.Roles.Any(i => i.Name == "user"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);

                var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
                manager.Create(role);
            }
          
            //!!!!!!!!!!!!!!!!!!!!!!şifreler 6 haneden büyük olmazsa eklemiyor!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //user
            if (!context.Users.Any(i => i.Name == "furkanberk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "berk", Surname = "yılmazer", UserName = "furkanberk", Email = "furkan_berk_yilmazer@hotmail.com" };
                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }
            if (!context.Users.Any(i => i.Name == "besu"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser() { Name = "sude", Surname = "doğan", UserName = "besu", Email = "behiyesudedogan@gmail.com" };
                manager.Create(user, "585858s");
                manager.AddToRole(user.Id, "admin");
                manager.AddToRole(user.Id, "user");
            }



            base.Seed(context);
        }
    }
}