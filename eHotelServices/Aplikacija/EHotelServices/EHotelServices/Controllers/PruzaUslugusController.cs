using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EHotelServices.Models;

namespace EHotelServices.Controllers
{
    public class PruzaUslugusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PruzaUslugus
        public ActionResult Index()
        {
            return View(db.PruzaUslugu.ToList());
        }

        // GET: PruzaUslugus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruzaUslugu pruzaUslugu = db.PruzaUslugu.Find(id);
            if (pruzaUslugu == null)
            {
                return HttpNotFound();
            }
            return View(pruzaUslugu);
        }

        // GET: PruzaUslugus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PruzaUslugus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UslugaId,termin,PibHotela,prihvacena")] PruzaUslugu pruzaUslugu)
        {
            if (ModelState.IsValid)
            {
                db.PruzaUslugu.Add(pruzaUslugu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pruzaUslugu);
        }

        // GET: PruzaUslugus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruzaUslugu pruzaUslugu = db.PruzaUslugu.Find(id);
            if (pruzaUslugu == null)
            {
                return HttpNotFound();
            }
            return View(pruzaUslugu);
        }

        // POST: PruzaUslugus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UslugaId,termin,PibHotela,prihvacena")] PruzaUslugu pruzaUslugu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pruzaUslugu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pruzaUslugu);
        }

        // GET: PruzaUslugus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruzaUslugu pruzaUslugu = db.PruzaUslugu.Find(id);
            if (pruzaUslugu == null)
            {
                return HttpNotFound();
            }
            return View(pruzaUslugu);
        }

        // POST: PruzaUslugus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PruzaUslugu pruzaUslugu = db.PruzaUslugu.Find(id);
            db.PruzaUslugu.Remove(pruzaUslugu);
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
