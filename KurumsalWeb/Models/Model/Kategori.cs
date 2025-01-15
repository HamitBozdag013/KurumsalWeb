using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50, ErrorMessage = "En fazla 50 karakter olabilir.")]
        public string KategoriAd { get; set; }
        public string Aciklama { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}