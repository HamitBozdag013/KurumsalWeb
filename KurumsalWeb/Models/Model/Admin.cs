using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
   public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50, ErrorMessage ="En fazla 50 karakter olabilir.")]
        public string Eposta { get; set; }
        [Required, StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        public string Sifre { get; set; }
        public int? YetkiId { get; set; }
        public virtual Yetki Yetki { get; set; }
    }
}