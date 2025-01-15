using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        private readonly KurumsalDBContext db = new KurumsalDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            return View(db.Hakkimizdas.ToList());
        }
        public ActionResult Edit(int id)
        {
            var hakkimizda = db.Hakkimizdas.Where(h=> h.Id == id).FirstOrDefault();
            return View(hakkimizda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hakkimizda hakkimizda, HttpPostedFileBase ResimURL)
        {
            var _hakkimizda = db.Hakkimizdas.Where(h => h.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    System.IO.File.Delete(Server.MapPath(_hakkimizda.ResimURL));

                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);

                    string resimName = Path.GetFileNameWithoutExtension(ResimURL.FileName) + fileInfo.Extension;
                    webImage.Resize(800, 450);
                    webImage.Save("~/Uploads/Hakkimizda/" + resimName);

                    _hakkimizda.ResimURL = "/Uploads/Hakkimizda/" + resimName;
                }
                _hakkimizda.Aciklama = hakkimizda.Aciklama;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}