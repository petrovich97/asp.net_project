using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EHotelServices.Models
{
    public class Administrator
    {
        [Key, ConcurrencyCheck, MaxLength(20),MinLength(5)]
        public String Username { get; set; }
        [ConcurrencyCheck, MaxLength(20),MinLength(5),Required]
        public String Password { get; set; }
        
        public String Ime { get; set;}
        public String Prezime { get; set; }
        public String PibHotela { get; set; }

    }
}