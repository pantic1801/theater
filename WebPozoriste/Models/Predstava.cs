using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPozoriste.Models
{
    [Table("Predstava")]
    public partial class Predstava
    {
        public Predstava()
        {
            Karta = new HashSet<Karta>();
            SlikeNavigation = new HashSet<Slike>();
        }

        public int PredstavaId { get; set; }
        [StringLength(100)]
        public string Kategorija { get; set; }
        [StringLength(100)]
        public string Naziv { get; set; }
        [StringLength(500)]
        public string Opis { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DatumIVreme { get; set; }
        [StringLength(30)]
        public string Reziser { get; set; }
        [StringLength(200)]
        public string Glumci { get; set; }
        [Column(TypeName = "image")]
        public byte[] Slike { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Cena { get; set; }

        [InverseProperty("Predstava")]
        public ICollection<Karta> Karta { get; set; }
        [InverseProperty("Predstava")]
        public ICollection<Slike> SlikeNavigation { get; set; }
    }
}
