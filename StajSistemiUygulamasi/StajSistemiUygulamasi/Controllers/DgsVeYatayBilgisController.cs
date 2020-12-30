using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StajSistemiUygulamasi.Models;

namespace StajSistemiUygulamasi.Controllers
{
    public class DgsVeYatayBilgisController : Controller
    {
        private stajsistemiEntities db = new stajsistemiEntities();

        // GET: DgsVeYatayBilgis
        public ActionResult Index()
        {
            var dgsVeYatayBilgi = db.DgsVeYatayBilgi.Include(d => d.OgrenciBilgi);
            return View(dgsVeYatayBilgi.ToList());
        }

        // GET: DgsVeYatayBilgis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DgsVeYatayBilgi dgsVeYatayBilgi = db.DgsVeYatayBilgi.Find(id);
            if (dgsVeYatayBilgi == null)
            {
                return HttpNotFound();
            }
            return View(dgsVeYatayBilgi);
        }

        // GET: DgsVeYatayBilgis/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo");
            return View();
        }

        // POST: DgsVeYatayBilgis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciNo,OncekiOkul,KurumAdi,ToplamGun")] DgsVeYatayBilgi dgsVeYatayBilgi)
        {
            if (ModelState.IsValid)
            {
                db.DgsVeYatayBilgi.Add(dgsVeYatayBilgi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", dgsVeYatayBilgi.OgrenciNo);
            return View(dgsVeYatayBilgi);
        }

        // GET: DgsVeYatayBilgis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DgsVeYatayBilgi dgsVeYatayBilgi = db.DgsVeYatayBilgi.Find(id);
            if (dgsVeYatayBilgi == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", dgsVeYatayBilgi.OgrenciNo);
            return View(dgsVeYatayBilgi);
        }

        // POST: DgsVeYatayBilgis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciNo,OncekiOkul,KurumAdi,ToplamGun")] DgsVeYatayBilgi dgsVeYatayBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dgsVeYatayBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", dgsVeYatayBilgi.OgrenciNo);
            return View(dgsVeYatayBilgi);
        }

        // GET: DgsVeYatayBilgis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DgsVeYatayBilgi dgsVeYatayBilgi = db.DgsVeYatayBilgi.Find(id);
            if (dgsVeYatayBilgi == null)
            {
                return HttpNotFound();
            }
            return View(dgsVeYatayBilgi);
        }

        // POST: DgsVeYatayBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DgsVeYatayBilgi dgsVeYatayBilgi = db.DgsVeYatayBilgi.Find(id);
            db.DgsVeYatayBilgi.Remove(dgsVeYatayBilgi);
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
    }
}
