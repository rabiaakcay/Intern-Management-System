using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StajSistemiUygulamasi.Models;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StajSistemiUygulamasi.Controllers
{
    public class KomisyonBilgisController : Controller
    {
        private stajsistemiEntities db = new stajsistemiEntities();

        // GET: KomisyonBilgis
        public ActionResult Index()
        {
            return View(db.KomisyonBilgi.ToList());
        }

        // GET: KomisyonBilgis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KomisyonBilgi komisyonBilgi = db.KomisyonBilgi.Find(id);
            if (komisyonBilgi == null)
            {
                return HttpNotFound();
            }
            return View(komisyonBilgi);
        }

        // GET: KomisyonBilgis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KomisyonBilgis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Unvan,AkademisyenAdi,AkademisyenSoyadi,Gorev")] KomisyonBilgi komisyonBilgi)
        {
            if (ModelState.IsValid)
            {
                db.KomisyonBilgi.Add(komisyonBilgi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(komisyonBilgi);
        }

        // GET: KomisyonBilgis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KomisyonBilgi komisyonBilgi = db.KomisyonBilgi.Find(id);
            if (komisyonBilgi == null)
            {
                return HttpNotFound();
            }
            return View(komisyonBilgi);
        }

        // POST: KomisyonBilgis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Unvan,AkademisyenAdi,AkademisyenSoyadi,Gorev")] KomisyonBilgi komisyonBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(komisyonBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(komisyonBilgi);
        }

        // GET: KomisyonBilgis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KomisyonBilgi komisyonBilgi = db.KomisyonBilgi.Find(id);
            if (komisyonBilgi == null)
            {
                return HttpNotFound();
            }
            return View(komisyonBilgi);
        }

        // POST: KomisyonBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KomisyonBilgi komisyonBilgi = db.KomisyonBilgi.Find(id);
            db.KomisyonBilgi.Remove(komisyonBilgi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public void OgrenciKomisyon()
        {
            var strw = new StringWriter();
            strw.WriteLine("\"OgrenciNo\", \"Adi\",\"Soyadi\",\"Ogretim\",\"MulakatTarihiVeSaati\",\"Gorev\", \"Unvan\", \"AkademisyenAdi\", \"AkademisyenSoyadi\"");
            Response.ClearContent();
            Response.AddHeader("Content-disposition",
                string.Format("attachment;filename=NewListing_{0}.xls", DateTime.Now));
            Response.ContentType = "text/xls";
            var listNews = db.OgrenciBilgi.OrderBy(x => x.OgrenciNo).ToList();
            var listNews1 = db.MulakatBilgi.OrderBy(x => x.OgrenciNo).ToList();
            var listNews2 = db.KomisyonBilgi.OrderBy(x => x.Gorev).ToList();

            foreach (var OgrenciBilgi in listNews)
            {
                foreach( var MulakatBilgi in listNews1)
                {
                    foreach(var KomisyonBilgi in listNews2)
                    {
                        strw.WriteLine(string.Format("\"{0}\", \"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\", \"{6}\", \"{7}\", \"{8}\"",
                        OgrenciBilgi.OgrenciNo, OgrenciBilgi.Adi, OgrenciBilgi.Soyadi, OgrenciBilgi.Ogretim, MulakatBilgi.MulakatTarihiVeSaati, KomisyonBilgi.Gorev, KomisyonBilgi.Unvan, KomisyonBilgi.AkademisyenAdi, KomisyonBilgi.AkademisyenSoyadi));


                    }
                }
    }
            Response.Write(strw.ToString());
            Response.End();

        }
    }
}
