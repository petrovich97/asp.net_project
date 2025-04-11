using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class Osoblje
    {
        [Key]
        public int OsobljeId { get; set; }

        [Display(Name ="Korisniko ime")]
        public String Username { get; set; }

        [Display(Name ="Ime"), Required]
        public String Ime { get; set; }

        [Display(Name ="Prezime"), Required]
        public String Prezime { get; set; }
        [Display(Name ="Datum rodjenja")]
        public DateTime DatumRodjenja { get; set; }

        [MaxLength(20)]
        [Display(Name ="Broj ziro racuna")]
        public String BrRacuna { get; set; }

        [MaxLength(100)]
        [Display(Name ="Opis duznosti")]
        public String OpisDuznosti { get; set; }

        public String PibHotela { get; set; }

        public IList<Usluga> usluge;
        [Display(Name ="E-mail")]
        public String Email { get; set; }

        public bool Odobren { get; set; }
        public Osoblje()
        {
            usluge = new List<Usluga>();
        }


    }
}