using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class Hotel
    {
        [Key]
        public String PibHotela { get; set; }
        [Display(Name ="Naziv")]
        [Required]
        public String Ime { get; set; }

        public String Lokacija { get; set; }
        public String Opis { get; set; }

        public String Slika { get; set; }

        public IList<Osoblje> zaposleni;
        public IList<Usluga> usluge;
        public Administrator admin;

        public Hotel()
        {
            zaposleni = new List<Osoblje>();
            usluge = new List<Usluga>();
            admin = new Administrator();
        }
    }
}