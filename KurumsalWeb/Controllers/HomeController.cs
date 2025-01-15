using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly KurumsalDBContext db = new KurumsalDBContext();
        // GET: Home
        [Route("")]
        [Route("Home/Index")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            ViewBag.Hizmetler = db.Hizmets.ToList().OrderByDescending(h => h.Id);
            return View();
        }
        public ActionResult SliderPartial()
        {
            return View(db.Sliders.ToList().OrderByDescending(s => s.Id));
        }
        public ActionResult HizmetPartial()
        {
            return View(db.Hizmets.ToList().OrderByDescending(h => h.Id));
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            return View(db.Hakkimizdas.SingleOrDefault());
        }
        [Route("Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            return View(db.Hizmets.ToList().OrderByDescending(h => h.Id));
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            return View(db.Iletisims.SingleOrDefault());
        }
        [HttpPost]
        public ActionResult Iletisims(string AdSoyad = null, string Email = null, string Konu = null, string Mesaj = null)
        {
            if (AdSoyad != null && Email != null && Konu != null && Mesaj != null)
            {
                //Google SMTP port ayarlarında yapılan değişiklikten dolayı kodlar doğru olmasına rağmen mail gönderilememektedir. 
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "gereksiz.013@gmail.com";
                WebMail.Password = "qoix qnyx yitl wljh";
                WebMail.SmtpPort = 587;
                WebMail.Send("gereksiz.013@gmail.com", Konu, Email + " - " + Mesaj);
                TempData["Ok"] = "Mesajınız başarılı bir şekilde gönderilmiştir.";
                return RedirectToAction("Iletisim", "Home");
            }
            else
            {
                TempData["No"] = "Hata oluştu. Tekrar deneyiniz.";
            }
            return View("Iletisim", "Home");
        }
        [Route("BlogPost")]
        public ActionResult Blog(int sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            return View(db.Blogs.Include("Kategori").OrderByDescending(b => b.Id).ToPagedList(sayfa,5));
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int sayfa = 1)
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            return View(db.Blogs.Include("Kategori").Where(k=>k.Kategori.Id==id).OrderByDescending(b => b.Id).ToPagedList(sayfa, 5));
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = db.Kimliks.SingleOrDefault();
            var blog = db.Blogs.Include("Yorums").Where(b => b.Id == id).SingleOrDefault();
            blog.TiklanmaSayisi += 1;
            db.SaveChanges();
            return View(blog);
        }
        public JsonResult YorumYap(string AdSoyad, string Eposta, string YorumIcerik, int BlogId)
        {
            if (YorumIcerik==null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorums.Add(new Yorum { AdSoyad = AdSoyad, Eposta = Eposta, YorumIcerik = YorumIcerik, BlogId=BlogId, Onay=false });
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);
        }
        public ActionResult BlogKategoriPartial()
        {
            return PartialView(db.Kategoris.Include("Blogs").ToList().OrderBy(k=>k.KategoriAd));
        }
        public ActionResult SonBlogKayitlariPartial()
        {
            return PartialView(db.Blogs.ToList().OrderByDescending(b => b.TiklanmaSayisi));
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Hizmetler = db.Hizmets.ToList().OrderByDescending(h => h.Id);
            ViewBag.Iletisim = db.Iletisims.SingleOrDefault();
            ViewBag.Blog = db.Blogs.ToList().OrderByDescending(b => b.Id);
            return PartialView();
        }
    }
}