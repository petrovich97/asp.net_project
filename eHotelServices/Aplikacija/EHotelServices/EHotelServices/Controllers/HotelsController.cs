using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EHotelServices.Models;
using Microsoft.AspNet.Identity;

namespace EHotelServices.Controllers
{
    public class HotelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hotels
        public ActionResult Index()
        {
            return View(db.Hoteli.ToList());
        }

        [HttpGet]
        public ActionResult ViewZaGosta()
        {
            return View(db.Hoteli.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PibHotela,Ime,Lokacija,Opis,Slika")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hoteli.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("AddHotelImage", "Image", new { pib = hotel.PibHotela });
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PibHotela,Ime,Lokacija,Opis,Slika")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditHotelImage", "Image", new { pib = hotel.PibHotela });
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hoteli.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Hotel hotel = db.Hoteli.Find(id);
            db.Hoteli.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UslugeZaGosta(string pib)
        {
            return RedirectToAction("IndexZaGosta", "Uslugas", new { id = pib });
        }

        public ActionResult Profil()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            int idG = db.Gosti.Where(x => x.eMail == currentUser.Email).FirstOrDefault().GostId;

            return RedirectToAction("Details", "Gosts", new { id = idG});
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
