using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebPozoriste.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

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

    }
}
