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
    public class BlogController : Controller
    {
        private readonly KurumsalDBContext db = new KurumsalDBContext();
        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.ToList().OrderBy(b=>b.Id));
        }
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "Id", "KategoriAd");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);

                    string resimName = Path.GetFileNameWithoutExtension(ResimURL.FileName) + fileInfo.Extension;
                    webImage.Resize(600, 400);
                    webImage.Save("~/Uploads/Blog/" + resimName);

                    blog.ResimURL = "/Uploads/Blog/" + resimName;
                    blog.TiklanmaSayisi = 0;
                }
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }
            var blog = db.Blogs.Where(b => b.Id == id).FirstOrDefault();
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoris,"Id","KategoriAd",blog.KategoriId);
            return View(blog);
        }
        [HttpPost]
        public ActionResult Edit(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL!=null)
                {
                    if (blog.ResimURL!=ResimURL.ToString())
                    {
                        System.IO.File.Delete(Server.MapPath(blog.ResimURL.ToString()));
                    }
                    if (System.IO.File.Exists(Server.MapPath(ResimURL.ToString())))
                    {
                        System.IO.File.Delete(Server.MapPath(ResimURL.ToString()));
                    }                  

                    WebImage webImage = new WebImage(ResimURL.InputStream);
                    FileInfo fileInfo = new FileInfo(ResimURL.FileName);

                    string logoName = Path.GetFileNameWithoutExtension(ResimURL.FileName) + fileInfo.Extension;
                    webImage.Resize(600, 400);
                    webImage.Save("~/Uploads/Blog/" + logoName);

                    blog.ResimURL = "/Uploads/Blog/" + logoName;
                }
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var silinecekBlog = db.Blogs.Where(b => b.Id == id).FirstOrDefault();

            if (System.IO.File.Exists(Server.MapPath(silinecekBlog.ResimURL.ToString())))
            {
                System.IO.File.Delete(Server.MapPath(silinecekBlog.ResimURL.ToString()));
            }

            db.Blogs.Remove(silinecekBlog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}