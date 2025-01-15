using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Yorum
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        [DisplayName("Yorumunuz")]
        public string YorumIcerik { get; set; }
        public bool Onay { get; set; }
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}