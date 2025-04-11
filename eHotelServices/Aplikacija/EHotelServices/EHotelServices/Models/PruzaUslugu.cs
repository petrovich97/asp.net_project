using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class PruzaUslugu
    {   [Key]
        public int Id { get; set; }
        public int UslugaId { get; set; }

        public int termin { get; set; }
        public String PibHotela { get; set; }

        public Boolean prihvacena { get; set; }
    }
}