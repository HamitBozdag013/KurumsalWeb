using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Models.Model
{
    public class Hizmet
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(150, ErrorMessage = "En fazla 150 karakter olabilir.")]
        [DisplayName("Hizmet Başlık")]
        public string Baslik { get; set; }
        [DisplayName("Hizmet Açıklama")]
        [AllowHtml]
        public string Aciklama { get; set; }
        [DisplayName("Hizmet Resim")]
        public string ResimURL { get; set; }
    }
}