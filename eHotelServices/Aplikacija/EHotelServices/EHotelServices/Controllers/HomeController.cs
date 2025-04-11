using EHotelServices.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHotelServices.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ApplicationDbContext dbc = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = dbc.Users.FirstOrDefault(x => x.Id == currentUserId);
            if (currentUser != null)
            {
                string username = currentUser.Email.Split('@')[0];
                if (dbc.Gosti.Where(x => x.Username == username).FirstOrDefault() != null)
                    return RedirectToAction("ViewZaGosta", "Hotels");
                if (dbc.Zaposleni.Where(x => x.Username == username).FirstOrDefault() != null && dbc.Admin.Where(x => x.Username == username).FirstOrDefault() == null)
                    return RedirectToAction("Index", "Uslugas");
                if (dbc.Admin.Where(x => x.Username == username).FirstOrDefault() != null)
                    return RedirectToAction("Index", "Administrators");
            }
            return View();
        }
    }
}