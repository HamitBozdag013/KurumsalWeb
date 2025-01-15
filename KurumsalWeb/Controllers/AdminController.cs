using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogSayisi = db.Blogs.Count();
            ViewBag.KategoriSayisi = db.Kategoris.Count();
            ViewBag.HizmetlerimizSayisi = db.Hizmets.Count();
            ViewBag.YorumSayisi = db.Yorums.Count();
            ViewBag.YorumOnay = db.Yorums.Where(y => y.Onay == false).Count();
            return View(db.Kategoris.ToList());
        }
        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admins.Where(a => a.Eposta == admin.Eposta).FirstOrDefault();
            if (login.Eposta == admin.Eposta && login.Sifre == Crypto.Hash(admin.Sifre, "MD5"))
            {
                Session["id"] = login.Id;
                Session["eposta"] = login.Eposta;
                Session["yetkiId"] = login.YetkiId;
                return RedirectToAction("Index", "Admin");
            }
            TempData["No"] = "Kullanıcı adı veya şifre yanlış!";
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["eposta"] = null;
            Session["yetkiId"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult ForgotMyPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotMyPassword(string eposta)
        {
            var admin = db.Admins.Where(a => a.Eposta == eposta).SingleOrDefault();
            if (admin != null)
            {
                Random random = new Random();
                int yeniSifre = random.Next();
                admin.Sifre = Crypto.Hash(Convert.ToString(yeniSifre), "MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "gereksiz.013@gmail.com";
                WebMail.Password = "qoix qnyx yitl wljh";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Admin Panel giriş şifreniz.", "Şifreniz: " + yeniSifre);
                TempData["Ok"] = " Yeni şifreniz " + eposta + " adresine gönderilmiştir.";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                TempData["No"] = "Hata oluştu. Tekrar deneyiniz.";
            }
            return View();
        }
        public ActionResult Kullanicilar()
        {
            return View(db.Admins.ToList());
        }
        public ActionResult Create(Admin admin, string sifre, string eposta)
        {
            ViewBag.YetkiId = new SelectList(db.Yetkis, "Id", "YetkiAdi");
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Kullanicilar");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.YetkiId = new SelectList(db.Yetkis, "Id", "YetkiAdi");
            var admin = db.Admins.Where(a => a.Id == id).SingleOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var login = db.Admins.Find(admin.Id);
                login.Eposta = admin.Eposta;
                if (login.Sifre != admin.Sifre)
                {
                    login.Sifre = Crypto.Hash(admin.Sifre, "MD5");
                }
                login.YetkiId = admin.YetkiId;

                //db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Kullanicilar");
            }
            return View(admin);
        }
        public ActionResult Delete(int id)
        {
            var silinecekAdmin = db.Admins.Where(a => a.Id == id).SingleOrDefault();
            if (silinecekAdmin != null)
            {
                db.Admins.Remove(silinecekAdmin);
                db.SaveChanges();
                return RedirectToAction("Kullanicilar");
            }
            return View();
        }
    }
}
