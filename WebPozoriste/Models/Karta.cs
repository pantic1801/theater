using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPozoriste.Models
{
    [Table("Karta")]
    public partial class Karta
    {
        public int KartaId { get; set; }
        public int PorudzbinaId { get; set; }
        public int PredstavaId { get; set; }
        public int Kolicina { get; set; }

        [ForeignKey("PorudzbinaId")]
        [InverseProperty("Karta")]
        public Porudzbina Porudzbina { get; set; }
        [ForeignKey("PredstavaId")]
        [InverseProperty("Karta")]
        public Predstava Predstava { get; set; }
    }
}
