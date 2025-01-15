using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Models.Model
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Baslik { get; set; }
        [AllowHtml]
        public string Icerik { get; set; }
        public string ResimURL { get; set; }
        public int TiklanmaSayisi { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori Kategori{ get; set; }
        public ICollection<Yorum> Yorums { get; set; }
    }
}