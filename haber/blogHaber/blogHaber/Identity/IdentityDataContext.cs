using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blogHaber.Identity
{
    public class IdentityDataContext: IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() : base("haberDb") //istersen web confige bir string daha ekle kullanıcılar farklı veritabanında tut farketmez
        {


        }


    }
}