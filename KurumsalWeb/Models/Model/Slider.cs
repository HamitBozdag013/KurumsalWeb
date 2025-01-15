using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Slider Başlık"), StringLength(50,ErrorMessage ="50 karakter olmalıdır")]
        public string Baslik { get; set; }
        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "150 karakter olmalıdır")]
        public string Aciklama { get; set; }
        [DisplayName("Slider Resim"), StringLength(250)]
        public string ResimURL { get; set; }

    }
}