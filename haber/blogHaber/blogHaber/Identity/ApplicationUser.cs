using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blogHaber.Identity
{
    //identity aktif çalıştırmak için önce Microsoft.AspNet.Identity.EntityFramework  , Microsoft.AspNet.Identity.Owin , Microsoft.Owin.Host.SystemWeb kuruyosun paketlerden
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}