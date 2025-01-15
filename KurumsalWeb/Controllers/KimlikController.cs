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
    public class KimlikController : Controller
    {
        private readonly KurumsalDBContext db = new KurumsalDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimliks.ToList());
        }

        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimliks.Where(k => k.Id == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kimlik kimlik, HttpPostedFileBase LogoURL)
        {
            var _kimlik = db.Kimliks.Where(k => k.Id == id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (LogoURL!=null)
                {
                    System.IO.File.Delete(Server.MapPath(_kimlik.LogoURL));
                    
                    WebImage webImage = new WebImage(LogoURL.InputStream);
                    FileInfo fileInfo = new FileInfo(LogoURL.FileName);

                    string logoName = Path.GetFileNameWithoutExtension(LogoURL.FileName) + fileInfo.Extension;
                    webImage.Resize(150, 120);
                    webImage.Save("~/Uploads/Kimlik/" + logoName);

                    _kimlik.LogoURL = "/Uploads/Kimlik/" + logoName;
                }
                _kimlik.Title = kimlik.Title;
                _kimlik.Keywords = kimlik.Keywords;
                _kimlik.Description = kimlik.Description;
                _kimlik.Unvan = kimlik.Unvan;
                db.SaveChanges();
                return RedirectToAction("Index");
            }          
                return View(kimlik);        
        }

    }
}
