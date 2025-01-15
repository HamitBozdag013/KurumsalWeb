using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Models.Model
{
    public class Kimlik
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Site Başlık")]
        [Required, StringLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string Title { get; set; }
        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(200, ErrorMessage = "En fazla 200 karakter olabilir.")]
        public string Keywords { get; set; }
        [DisplayName("Site Açıklama")]
        [Required, StringLength(300, ErrorMessage = "En fazla 300 karakter olabilir.")]
        [AllowHtml]
        public string Description { get; set; }
        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }
        [DisplayName("Site Ünvan")]
        public string Unvan { get; set; }
    }
}