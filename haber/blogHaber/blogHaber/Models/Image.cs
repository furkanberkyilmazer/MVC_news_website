using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace blogHaber.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Resim { get; set; }




        public int HaberId { get; set; }
        public Haber Haber { get; set; }

    }
}