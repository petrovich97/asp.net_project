using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class Gost
    {

        [Key]
        public int GostId { get; set; }

        [MaxLength(20), MinLength(5),Required]
        [Display (Name ="Korisnicko ime")]
        public String Username { get; set; }

        
        [MaxLength(20, ErrorMessage ="Maksimalna velicina polja je 20")]
        [Display(Name ="Ime")]
        public String Ime { get; set; }
        
        [MaxLength(25, ErrorMessage ="Maksimalna velicina polja je 25")]
        [Display(Name ="Prezime")]
        public String Prezime { get; set; }

        [MinLength(9),MaxLength(9)]
        [Display(Name ="Broj licne karte")]
        public String BrLK { get; set; }

        public String Slika { get; set; }
        public String PibHotela { get; set; }

        public virtual IList<Usluga> ListaUsluga { get; set; }

        public double Racun { get; set; }
        [Display(Name ="E-mail")]
        public String eMail { get; set; }

        public bool Odobren { get; set; }

        public Gost()
        {
            ListaUsluga = new List<Usluga>();
            
        }



    }
}