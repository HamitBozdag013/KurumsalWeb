using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetlerController : Controller
    {
        private readonly KurumsalDBContext db = new KurumsalDBContext();
        // GET: Hizmetler
        public ActionResult Index()
        {
            return View(db.Hizmets.ToList());
        }
        public ActionResult Create()
        {
            return View();
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);

                    string resimName = Path.GetFileNameWithoutExtension(ResimURL.FileName) + fileInfo.Extension;
                    webImage.Resize(500, 500);
                    webImage.Save("~/Uploads/Hizmet/" + resimName);

                    hizmet.ResimURL = "/Uploads/Hizmet/" + resimName;
                }
                db.Hizmets.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek hizmet bulunamadı.";
            }
            var duzenlenecekHizmet = db.Hizmets.Find(id);
            if (duzenlenecekHizmet == null)
            {
                return HttpNotFound();
            }
            return View(duzenlenecekHizmet);
        }
        [HttpPost]
        public ActionResult Edit(Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    if (hizmet.ResimURL != ResimURL.ToString())
                    {
                        System.IO.File.Delete(Server.MapPath(hizmet.ResimURL.ToString()));
                    }
                    if (System.IO.File.Exists(Server.MapPath(ResimURL.ToString())))
                    {
                        System.IO.File.Delete(Server.MapPath(ResimURL.ToString()));
                    }

                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);

                    string logoName = Path.GetFileNameWithoutExtension(ResimURL.FileName) + fileInfo.Extension;
                    webImage.Resize(500, 500);
                    webImage.Save("~/Uploads/Hizmet/" + logoName);

                    hizmet.ResimURL = "/Uploads/Hizmet/" + logoName;
                }
                db.Entry(hizmet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var silinecekHizmet = db.Hizmets.Where(h => h.Id == id).FirstOrDefault();

            if (System.IO.File.Exists(Server.MapPath(silinecekHizmet.ResimURL.ToString())))
            {
                System.IO.File.Delete(Server.MapPath(silinecekHizmet.ResimURL.ToString()));
            }

            db.Hizmets.Remove(silinecekHizmet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}