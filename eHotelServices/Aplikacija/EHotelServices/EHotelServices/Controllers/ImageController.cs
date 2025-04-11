using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHotelServices.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace EHotelServices.Controllers
{
    public class ImageController : Controller
    {
        [HttpGet]
        public ActionResult Add(string username)
        {
            Image img = new Image() { Title = username };
            return View(img);
        }

        [HttpGet]
        public ActionResult AddHotelImage(string pib)
        {
            Image img = new Image() { Title = pib };
            return View(img);
        }
        
        [HttpPost]
        public ActionResult AddHotelImage(Image image)
        {
            String filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            String extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);

            using (ApplicationDbContext dbc = new ApplicationDbContext())
            {
                Hotel h = dbc.Hoteli.Where(x => x.PibHotela == image.Title).ToList()[0];
                h.Slika = image.ImagePath;
                dbc.SaveChanges();
            }

            using (ImageDbModels db = new ImageDbModels())
            {
                db.Image.Add(image);
                db.SaveChanges();

            }
            ModelState.Clear();
            return RedirectToAction("Index", "Hotels");
        }

        



        [HttpPost]
        public ActionResult Add( Image image)
        {
            String filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            String extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);

            using (ApplicationDbContext dbc = new ApplicationDbContext())
            {
                Gost user = dbc.Gosti.Where(x => x.Username == image.Title).ToList()[0];
                user.Slika = image.ImagePath;
                dbc.SaveChanges();
            }

            using (ImageDbModels db = new ImageDbModels())
            {
                db.Image.Add(image);
                db.SaveChanges();
                
            }
            ModelState.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult View(int id)
        {
            Image image = new Image();
            using(ImageDbModels db= new ImageDbModels())
            {
                image = db.Image.Where(x => x.ImageID == id).FirstOrDefault();
            }
            return View(image);
        }

        [HttpGet]
        public ActionResult EditGostView(string username)
        {
            Image img = new Image() { Title = username };
            return View(img);
        }

        [HttpPost]
        public ActionResult EditGostView(Image image)
        {
            String filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            String extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);

            ApplicationDbContext dbc = new ApplicationDbContext();
            
                Gost user = dbc.Gosti.Where(x => x.Username == image.Title).ToList()[0];
                user.Slika = image.ImagePath;
                dbc.SaveChanges();
            

            ImageDbModels db = new ImageDbModels();
            
                db.Image.Add(image);
                db.SaveChanges();

            return RedirectToAction("Details", "Gosts", new { id = user.GostId });
            
        }

        [HttpGet]
        public ActionResult EditHotelImage(string pib)
        {
            Image img = new Image() { Title = pib };
            return View(img);
        }

        [HttpPost]
        public ActionResult EditHotelImage(Image image)
        {
            String filename = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
            String extension = Path.GetExtension(image.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            image.ImagePath = "~/Images/" + filename;
            filename = Path.Combine(Server.MapPath("~/Images/"), filename);
            image.ImageFile.SaveAs(filename);

             ApplicationDbContext dbc = new ApplicationDbContext();
            
                Hotel h = dbc.Hoteli.Where(x => x.PibHotela == image.Title).ToList()[0];
                h.Slika = image.ImagePath;
                dbc.SaveChanges();
            

            using (ImageDbModels db = new ImageDbModels())
            {
                db.Image.Add(image);
                db.SaveChanges();

            }
            ModelState.Clear();

            
            return RedirectToAction("Details", "Hotels", new { id = h.PibHotela });
        }


    }
}