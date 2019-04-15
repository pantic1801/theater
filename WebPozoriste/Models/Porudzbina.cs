using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPozoriste.Models
{
    [Table("Porudzbina")]
    public partial class Porudzbina
    {
        public Porudzbina()
        {
            Karta = new HashSet<Karta>();
        }

        public int PorudzbinaId { get; set; }
        [Required]
        [StringLength(450)]
        public string KorisnikId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DatumPorucivanja { get; set; }

        [ForeignKey("KorisnikId")]
        [InverseProperty("Porudzbina")]
        public Korisnik Korisnik { get; set; }
        [InverseProperty("Porudzbina")]
        public ICollection<Karta> Karta { get; set; }
        
        
    }
}
