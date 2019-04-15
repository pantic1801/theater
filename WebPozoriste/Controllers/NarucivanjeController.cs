using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPozoriste.Models;
using WebPozoriste.Services;

namespace WebPozoriste.Controllers
{
    public class NarucivanjeController : Controller


    {
        private readonly PozoristeContext db;
        private Narucivanje narucivanje;
        private NarucivanjeServis nServis;

        public NarucivanjeController(PozoristeContext _db, NarucivanjeServis _nServis)
        {
            nServis = _nServis;
            db = _db;
            narucivanje = nServis.CitajSpisak();
        }


        public IActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(narucivanje);
        }


        public IActionResult DodajKartu(int PredstavaId, string returnUrl)
        {
            Predstava p1 = db.Predstava.Find(PredstavaId);
            if (p1 != null)
            {
                narucivanje.DodajKartu(p1, 1);
                nServis.CuvajSpisak(narucivanje);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult ObrisiKartu(int PredstavaId, string returnUrl)
        {
            Predstava p1 = db.Predstava.Find(PredstavaId);


            if (p1 != null)
            {
                narucivanje.ObrisiKartu(p1);
                nServis.CuvajSpisak(narucivanje);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult PromeniKartu(int PredstavaId, int kolicina, string returnUrl)
        {
            Predstava predstava = db.Predstava
            .SingleOrDefault(p => p.PredstavaId == PredstavaId);
            if (predstava != null)
            {
                narucivanje.PromeniKartu(predstava, kolicina);
                nServis.CuvajSpisak(narucivanje);
            }
            return RedirectToAction("Index", new { returnUrl });
        }




    }
}