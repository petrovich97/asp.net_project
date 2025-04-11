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
    public class UslugasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uslugas
        public ActionResult Index()
        {
            return View(db.Usluge.ToList());
        }

        
        public ActionResult IndexZaGosta(string id)
        {
            

            List<Usluga> usluge = new List<Usluga>();
            List<PruzaUslugu> pruzaUsluge = new List<PruzaUslugu>();

            pruzaUsluge = db.PruzaUslugu.Where(x => x.PibHotela == id).ToList();

            foreach(PruzaUslugu pu in pruzaUsluge)
            {
                usluge.Add(db.Usluge.Where(x => x.UslugaId == pu.UslugaId).FirstOrDefault());
            }

            return View(usluge);
        }
        
        public ActionResult Zahtevaj(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            Gost g = db.Gosti.Where(x => x.eMail == currentUser.Email).FirstOrDefault();
           // ViewBag.racun=
            Usluga u = db.Usluge.Where(x => x.UslugaId == id).FirstOrDefault();
            u.GostUsername= (string)currentUser.UserName.Split('@')[0];
            db.SaveChanges();
            return RedirectToAction("ZahtevaneUsluge","Gosts");
        
        }

       
        public ActionResult ObradaZahteva()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            string username = currentUser.UserName.Split('@')[0];
            List<Usluga> uslugeZaposlenog = new List<Usluga>();
            List<Usluga> zahtevaneUsluge = new List<Usluga>();
            List<bool> zahtevi = new List<bool>();
            uslugeZaposlenog = db.Usluge.Where(x => x.ZaposleniUsername == username).ToList();
            foreach(Usluga u in uslugeZaposlenog)
            {
                if (u.GostUsername != null)
                    zahtevaneUsluge.Add(u);
            }
            foreach (Usluga u in zahtevaneUsluge)
            {
                zahtevi.Add(db.PruzaUslugu.Where(x => x.UslugaId == u.UslugaId).FirstOrDefault().prihvacena);
            }

            List<UslugaView> uslugeView = new List<UslugaView>();

            for (int i = 0; i < zahtevaneUsluge.Count(); i++)
            {
                UslugaView uslugaView = new UslugaView()
                {
                    UslugaId = zahtevaneUsluge[i].UslugaId,
                    Tip = zahtevaneUsluge[i].Tip,
                    Cena = zahtevaneUsluge[i].Cena,
                    Opis = zahtevaneUsluge[i].Opis,
                    ZaposleniUsername = zahtevaneUsluge[i].ZaposleniUsername,
                    GostUsername = zahtevaneUsluge[i].GostUsername,
                    Trajanje = zahtevaneUsluge[i].Trajanje,
                    prihvacena = zahtevi[i]
                };

                uslugeView.Add(uslugaView);
            }

            return View(uslugeView);
            
        }

        public ActionResult PrihvatiZahtev(int id)
        {
            PruzaUslugu pU = db.PruzaUslugu.Where(x => x.UslugaId == id).FirstOrDefault();
            pU.prihvacena = true;
            Usluga u = db.Usluge.Where(x => x.UslugaId == id).FirstOrDefault();
            Gost g = db.Gosti.Where(x => x.Username == u.GostUsername).FirstOrDefault();
            g.Racun += u.Cena;
            db.SaveChanges();
            return RedirectToAction("ObradaZahteva");
        
        }

        // GET: Uslugas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // GET: Usluge/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uslugas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UslugaId,Tip,Cena,Opis,ZaposleniUsername,GostUsername,Trajanje")] Usluga usluga)
        {

            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            usluga.ZaposleniUsername = (string)currentUser.UserName.Split('@')[0]; 
            Osoblje o= db.Zaposleni.Where(x => x.Email == currentUser.Email ).FirstOrDefault();
            Hotel h= db.Hoteli.Where(x => x.PibHotela == o.PibHotela).FirstOrDefault();
            PruzaUslugu pruzaUslugu = new PruzaUslugu() { PibHotela = o.PibHotela};

            if (ModelState.IsValid)
            {
                db.Usluge.Add(usluga);
                db.SaveChanges();
                pruzaUslugu.UslugaId = usluga.UslugaId;
                db.PruzaUslugu.Add(pruzaUslugu);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usluga);
        }

        // GET: Uslugas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Uslugas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UslugaId,Tip,Cena,Opis,ZaposleniUsername,GostUsername,Trajanje")] Usluga usluga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usluga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usluga);
        }

        // GET: Uslugas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usluga usluga = db.Usluge.Find(id);
            if (usluga == null)
            {
                return HttpNotFound();
            }
            return View(usluga);
        }

        // POST: Uslugas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usluga usluga = db.Usluge.Find(id);
            db.Usluge.Remove(usluga);
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
