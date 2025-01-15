using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Models.Model
{
    public class Hakkimizda
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Hakkımızda Açıklama")]
        [AllowHtml]
        public string Aciklama { get; set; }
        public string ResimURL { get; set; }
    }
}