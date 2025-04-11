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
    public class GostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Gosts
        public ActionResult Index()
        {
            return View(db.Gosti.ToList());
        }

        // GET: Gosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // GET: Gosts/Create
        public ActionResult Create(string email)
        {
            string username = (string)email.Split('@')[0];
            var gost = new Gost() { Username = username, eMail = email };
            return View(gost);
        }

        // POST: Gosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GostId,Username,Ime,Prezime,BrLK,Slika,PibHotela,Racun,eMail,Odobren")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                gost.Odobren = false;
                db.Gosti.Add(gost);
                db.SaveChanges();
                return RedirectToAction("Add", "Image",new { username = gost.Username});
            }

            return View(gost);
        }

        // GET: Gosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // POST: Gosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GostId,Username,Ime,Prezime,BrLK,Slika,PibHotela,Racun,eMail,Odobren")] Gost gost)
        {
            if (ModelState.IsValid)
            {
                gost.Odobren = true;
                db.Entry(gost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditGostView", "Image", new { username = gost.Username });
            }
            return View(gost);
        }

        // GET: Gosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gost gost = db.Gosti.Find(id);
            if (gost == null)
            {
                return HttpNotFound();
            }
            return View(gost);
        }

        // POST: Gosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gost gost = db.Gosti.Find(id);
            db.Gosti.Remove(gost);
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
        public ActionResult Odobri(string username)
        {
            using (ApplicationDbContext dbc = new ApplicationDbContext())
            {

                Gost g = dbc.Gosti.Where(x => x.Username == username).FirstOrDefault();
                if (g != null)
                {
                    g.Odobren = true;
                    dbc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult ZahtevaneUsluge()
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            List<Usluga> usluge = new List<Usluga>();
            List<bool> odobrene = new List<bool>();
            string username = currentUser.UserName.Split('@')[0];


            usluge = db.Usluge.Where(x => x.GostUsername == username).ToList();

            foreach (Usluga u in usluge)
            {
                odobrene.Add(db.PruzaUslugu.Where(x => x.UslugaId == u.UslugaId).FirstOrDefault().prihvacena);
            }

            List<UslugaView> uslugeView = new List<UslugaView>();

            for(int i=0;i<usluge.Count();i++)
            {
                UslugaView uslugaView = new UslugaView()
                {
                    UslugaId = usluge[i].UslugaId,
                    Tip = usluge[i].Tip,
                    Cena = usluge[i].Cena,
                    Opis = usluge[i].Opis,
                    ZaposleniUsername = usluge[i].ZaposleniUsername,
                    GostUsername = usluge[i].GostUsername,
                    Trajanje = usluge[i].Trajanje,
                    prihvacena = odobrene[i]
                };

                uslugeView.Add(uslugaView);
            }
            
            return View(uslugeView);
        }
        public ActionResult OtkaziUslugu(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            string username = currentUser.UserName.Split('@')[0];
            PruzaUslugu pU = db.PruzaUslugu.Where(x => x.UslugaId == id).FirstOrDefault();
            pU.prihvacena = false;
            Usluga u = db.Usluge.Where(x => x.GostUsername == username).FirstOrDefault();
            u.GostUsername = null;
            db.SaveChanges();
            return RedirectToAction("ZahtevaneUsluge", "Gosts");
        }

        public ActionResult ListaUsluga(int id)
        {
            Gost g = db.Gosti.Where(x => x.GostId == id).FirstOrDefault();
            return RedirectToAction("IndexZaGosta", "Uslugas", new { pib = g.PibHotela });
        }
    }
}
