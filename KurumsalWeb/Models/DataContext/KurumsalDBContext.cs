using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.DataContext
{
    public class KurumsalDBContext:DbContext
    {
        public KurumsalDBContext():base("KurumsalDB")
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Hakkimizda> Hakkimizdas { get; set; }
        public DbSet<Hizmet> Hizmets { get; set; }
        public DbSet<Iletisim> Iletisims { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Kimlik> Kimliks { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Yorum> Yorums { get; set; }
        public DbSet<Yetki> Yetkis { get; set; }
    }
}