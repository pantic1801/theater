using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPozoriste.Models.AccountViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Unesite email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Unesite lozinku")]
        [DataType(DataType.Password)]
        [Display(Name = "Lozinka")]
        public string Password { get; set; }

        [Display(Name = "Zapamti me?")]
        public bool RememberMe { get; set; }

    }
}
