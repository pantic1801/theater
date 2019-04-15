using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPozoriste.Models
{
    [Table("Korisnik")]
    public partial class Korisnik
    {
        public Korisnik()
        {
            Porudzbina = new HashSet<Porudzbina>();
        }

        public string KorisnikId { get; set; }
        [Required]
        [StringLength(30)]
        public string Ime { get; set; }
        [Required]
        [StringLength(30)]
        public string Prezime { get; set; }
        [Required]
        [StringLength(30)]
        public string Drzava { get; set; }
        [Required]
        [StringLength(30)]
        public string Grad { get; set; }
        [Required]
        [StringLength(100)]
        public string Adresa { get; set; }

        [InverseProperty("Korisnik")]
        public ICollection<Porudzbina> Porudzbina { get; set; }
    }
}
