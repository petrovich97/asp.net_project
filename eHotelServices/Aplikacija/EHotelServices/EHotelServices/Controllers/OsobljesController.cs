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
    public class OsobljesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Osobljes
        public ActionResult Index()
        {
            return View(db.Zaposleni.ToList());
        }

        // GET: Osobljes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoblje osoblje = db.Zaposleni.Find(id);
            if (osoblje == null)
            {
                return HttpNotFound();
            }
            return View(osoblje);
        }

        // GET: Osobljes/Create
        public ActionResult Create(string email)
        {
            string username = (string)email.Split('@')[0];
            var osoblje = new Osoblje() { Username = username, Email = email };
            return View(osoblje);
        }

        // POST: Osobljes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OsobljeId,Username,Ime,Prezime,DatumRodjenja,BrRacuna,OpisDuznosti,PibHotela,Email,Odobren")] Osoblje osoblje)
        {
            if (ModelState.IsValid)
            {
                osoblje.Odobren = false;
                db.Zaposleni.Add(osoblje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoblje);
        }

        // GET: Osobljes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoblje osoblje = db.Zaposleni.Find(id);
            if (osoblje == null)
            {
                return HttpNotFound();
            }
            return View(osoblje);
        }

        // POST: Osobljes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OsobljeId,Username,Ime,Prezime,DatumRodjenja,BrRacuna,OpisDuznosti,PibHotela,Email,Odobren")] Osoblje osoblje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osoblje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoblje);
        }

        // GET: Osobljes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoblje osoblje = db.Zaposleni.Find(id);
            if (osoblje == null)
            {
                return HttpNotFound();
            }
            return View(osoblje);
        }

        // POST: Osobljes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osoblje osoblje = db.Zaposleni.Find(id);
            db.Zaposleni.Remove(osoblje);
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

        public ActionResult AzurirajPIB(int id, string pib)
        {
            using (ApplicationDbContext dbc = new ApplicationDbContext())
            {

                Osoblje o = dbc.Zaposleni.Where(x => x.OsobljeId == id).FirstOrDefault();
                if (o != null)
                {
                    o.PibHotela = pib;
                    dbc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Odobri(string username)
        {
            using (ApplicationDbContext dbc = new ApplicationDbContext())
            {

                Osoblje o = dbc.Zaposleni.Where(x => x.Username == username).FirstOrDefault();
                if (o != null)
                {
                    o.Odobren = true;
                    dbc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }


    }
}
