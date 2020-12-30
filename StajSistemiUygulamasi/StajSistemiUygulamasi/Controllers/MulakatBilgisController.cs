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
    public class MulakatBilgisController : Controller
    {
        private stajsistemiEntities db = new stajsistemiEntities();

        // GET: MulakatBilgis
        public ActionResult Index()
        {
            var mulakatBilgi = db.MulakatBilgi.Include(m => m.OgrenciBilgi);
            return View(mulakatBilgi.ToList());
        }

        // GET: MulakatBilgis/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MulakatBilgi mulakatBilgi = db.MulakatBilgi.Find(id);
            if (mulakatBilgi == null)
            {
                return HttpNotFound();
            }
            return View(mulakatBilgi);
        }

        // GET: MulakatBilgis/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo");
            return View();
        }

        // POST: MulakatBilgis/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciNo,Devam,CalismaVeCaba,IsVaktindeDavranis,AmireKarsiDavranis,IsArkadaslarinaDavranis,Prove,Duzen,Sunum,Icerik,Mulakat,MulakatTarihiVeSaati")] MulakatBilgi mulakatBilgi)
        {
            if (ModelState.IsValid)
            {
                db.MulakatBilgi.Add(mulakatBilgi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", mulakatBilgi.OgrenciNo);
            return View(mulakatBilgi);
        }

        // GET: MulakatBilgis/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MulakatBilgi mulakatBilgi = db.MulakatBilgi.Find(id);
            if (mulakatBilgi == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", mulakatBilgi.OgrenciNo);
            return View(mulakatBilgi);
        }

        // POST: MulakatBilgis/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciNo,Devam,CalismaVeCaba,IsVaktindeDavranis,AmireKarsiDavranis,IsArkadaslarinaDavranis,Prove,Duzen,Sunum,Icerik,Mulakat,MulakatTarihiVeSaati")] MulakatBilgi mulakatBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mulakatBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", mulakatBilgi.OgrenciNo);
            return View(mulakatBilgi);
        }

        // GET: MulakatBilgis/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MulakatBilgi mulakatBilgi = db.MulakatBilgi.Find(id);
            if (mulakatBilgi == null)
            {
                return HttpNotFound();
            }
            return View(mulakatBilgi);
        }

        // POST: MulakatBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            MulakatBilgi mulakatBilgi = db.MulakatBilgi.Find(id);
            db.MulakatBilgi.Remove(mulakatBilgi);
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
