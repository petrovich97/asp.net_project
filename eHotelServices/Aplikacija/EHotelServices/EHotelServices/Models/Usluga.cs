using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class Usluga
    {
        [Key]
        public int UslugaId { get; set; }

        public String Tip { get; set; }
        public int Cena { get; set; }

        public String Opis { get; set; }


        [MaxLength(10)]
        public String ZaposleniUsername { get; set; }

        [MaxLength(10)]
        public String GostUsername { get; set; }

        public IList<PruzaUslugu> usluge;

        public int Trajanje { get; set; }
        public Usluga()
        {
            usluge = new List<PruzaUslugu>();
        }



    }
    public class UslugaView
    {
        public int UslugaId { get; set; }

        public String Tip { get; set; }
        public int Cena { get; set; }

        public String Opis { get; set; }

        public String ZaposleniUsername { get; set; }
               
        public String GostUsername { get; set; }
        public int Trajanje { get; set; }

        public Boolean prihvacena { get; set; }


    }
}