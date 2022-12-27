﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace blogHaber.Models
{
    public class Register
    {
        [Required] //veri girme zorunlu
        [DisplayName("Adınız")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyadınız")]
        public string SurName { get; set; }

        [Required]
        [DisplayName("Kulanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Eposta")]
        [EmailAddress(ErrorMessage = "Eposta adresinizi düzgün giriniz.")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor.")]
        public string RePassword { get; set; }
        public bool RememberMe { get; internal set; }
    }
}