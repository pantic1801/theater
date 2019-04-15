using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPozoriste.Models
{
    [Table("Slike")]
    public partial class Slike
    {
        [Key]
        public int SlikaId { get; set; }
        public int PredstavaId { get; set; }
        [Column(TypeName = "image")]
        public byte[] Slika { get; set; }

        [ForeignKey("PredstavaId")]
        [InverseProperty("SlikeNavigation")]
        public Predstava Predstava { get; set; }
    }
}
