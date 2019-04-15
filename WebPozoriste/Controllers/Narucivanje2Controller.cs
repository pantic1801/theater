using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPozoriste.Models;
using WebPozoriste.Services;

namespace WebPozoriste.Controllers
{
    public class Narucivanje2Controller : Controller
    {

        private readonly PozoristeContext db;
        private readonly UserManager<ApplicationUser> um;

        private NarucivanjeServis nServis;


        public Narucivanje2Controller(PozoristeContext _db, UserManager<ApplicationUser> _um, NarucivanjeServis _nServis)
        {
            db = _db;
            um = _um;
            nServis = _nServis;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            Narucivanje narucivanje = nServis.CitajSpisak();
            if (narucivanje.Kartas.Count() == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ApplicationUser user = await um.GetUserAsync(User);
            string id = user.Id;
            Porudzbina p1 = new Porudzbina
            {
                KorisnikId = id,
                DatumPorucivanja = DateTime.Now
            };
            try
            {
                db.Porudzbina.Add(p1);
                db.SaveChanges();
                int pId = p1.PorudzbinaId;

                foreach (NarucenaKarta kr in narucivanje.Kartas)
                {
                    Karta kr1 = new Karta
                    {
                        PorudzbinaId = pId,
                        PredstavaId = kr.Predstava.PredstavaId,
                        Kolicina = kr.Kolicina
                    };

                    db.Karta.Add(kr1);
                    db.SaveChanges();
                }


                nServis.ObrisiSpisak();

                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");

            }

        }

    }
}