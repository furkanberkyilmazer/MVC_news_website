using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blogHaber.Models
{
    public class About
    {

        public int Id { get; set; }


        [AllowHtml]
        public string Hakkimda { get; set; }
    }
}