using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StajSistemiUygulamasi.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace StajSistemiUygulamasi.Controllers
{
    public class StajBilgisController : Controller
    {
        private stajsistemiEntities db = new stajsistemiEntities();

        // GET: StajBilgis
        public ActionResult Index()
        {
            var stajBilgi = db.StajBilgi.Include(s => s.OgrenciBilgi);
            return View(stajBilgi.ToList());
        }

        // GET: StajBilgis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            if (stajBilgi == null)
            {
                return HttpNotFound();
            }
            return View(stajBilgi);
        }

        // GET: StajBilgis/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo");
            return View();
        }

        // POST: StajBilgis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciNo,Sinif,KurumAdi,Sehir,StajKonusu,BaslangicTarihi,BitisTarihi,ToplamGun,KabulEdilenGun,StajDegerlendirildi")] StajBilgi stajBilgi)
        {
            if (ModelState.IsValid)
            {
                db.StajBilgi.Add(stajBilgi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", stajBilgi.OgrenciNo);
            return View(stajBilgi);
        }

        // GET: StajBilgis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            if (stajBilgi == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", stajBilgi.OgrenciNo);
            return View(stajBilgi);
        }

        // POST: StajBilgis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciNo,Sinif,KurumAdi,Sehir,StajKonusu,BaslangicTarihi,BitisTarihi,ToplamGun,KabulEdilenGun,StajDegerlendirildi")] StajBilgi stajBilgi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stajBilgi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciNo = new SelectList(db.OgrenciBilgi, "OgrenciNo", "OgrenciNo", stajBilgi.OgrenciNo);
            return View(stajBilgi);
        }

        // GET: StajBilgis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            if (stajBilgi == null)
            {
                return HttpNotFound();
            }
            return View(stajBilgi);
        }

        // POST: StajBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StajBilgi stajBilgi = db.StajBilgi.Find(id);
            db.StajBilgi.Remove(stajBilgi);
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

        public void ExportContentToCSV()
        {
            var strw = new StringWriter();
            strw.WriteLine("\"OgrenciNo\", \"Sinif\",\"KurumAdi\",\"Sehir\",\"StajKonusu\",\"BaslangicTarihi\", \"BitisTarihi\", \"ToplamGun\", \"KabulEdilenGun\", \"StajDegerlendirildi\"");
            Response.ClearContent();
            Response.AddHeader("Content-disposition",
                string.Format("attachment;filename=NewListing_{0}.csv", DateTime.Now));
            Response.ContentType = "text/csv";
            var listNews = db.StajBilgi.OrderBy(x => x.OgrenciNo).ToList();
            foreach (var StajBilgi in listNews)
            {
                strw.WriteLine(string.Format("\"{0}\", \"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\"",
                StajBilgi.OgrenciNo, StajBilgi.Sinif, StajBilgi.KurumAdi, StajBilgi.Sehir, StajBilgi.StajKonusu, StajBilgi.BaslangicTarihi, StajBilgi.BitisTarihi, StajBilgi.ToplamGun, StajBilgi.KabulEdilenGun, StajBilgi.StajDegerlendirildi));
            }
            Response.Write(strw.ToString());
            Response.End();
        }

        public void ExportContentToExcel()
        {
            var gv = new GridView();
            gv.DataSource = db.StajBilgi.OrderBy(x => x.OgrenciNo).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.AddHeader("Content-disposition",
                string.Format("attachment;filename=NewListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var strw = new StringWriter();
            var htmlTw = new HtmlTextWriter(strw);

            gv.RenderControl(htmlTw);
            Response.Write(strw.ToString());
            Response.End();
        }
        public void KurumAdlariniListele()
        {
            var strw = new StringWriter();
            strw.WriteLine("\"KurumAdi\"");
            Response.ClearContent();
            Response.AddHeader("Content-disposition",
                string.Format("attachment;filename=NewListing_{0}.xls", DateTime.Now));
            Response.ContentType = "applicatin/excel";
            var listNews = db.StajBilgi.OrderBy(x => x.OgrenciNo).ToList();
            foreach (var StajBilgi in listNews)
            {
                strw.WriteLine(string.Format("\"{0}\"",
                StajBilgi.KurumAdi));
            }
            Response.Write(strw.ToString());
            Response.End();
        }

        public void IlBaziBasari()
        {
            var strw = new StringWriter();
            strw.WriteLine("\"OgrenciNo\", \"Sehir\",\"StajKonusu\" ,\"ToplamGun\",\"KabulEdilenGun\"");
            Response.ClearContent();
            Response.AddHeader("Content-disposition",
                string.Format("attachment;filename=NewListing_{0}.xls", DateTime.Now));
            Response.ContentType = "application/excel";
            var listNews = db.StajBilgi.OrderBy(x => x.Sehir).ToList();
            foreach (var StajBilgi in listNews)
            {
                strw.WriteLine(string.Format("\"{0}\", \"{1}\",\"{2}\",\"{3}\", \"{4}\"",
                StajBilgi.OgrenciNo, StajBilgi.Sehir, StajBilgi.StajKonusu, StajBilgi.ToplamGun, StajBilgi.KabulEdilenGun, StajBilgi.StajDegerlendirildi));
            }
            Response.Write(strw.ToString());
            Response.End();

        }
    }
}