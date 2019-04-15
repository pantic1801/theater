using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPozoriste.Models;
using WebPozoriste.Models.AccountViewModels;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace WebPozoriste.Controllers
{
    public class HomeController : Controller
    {
        private readonly PozoristeContext db;
        private readonly RoleManager<IdentityRole> rm;
        private readonly UserManager<ApplicationUser> um;

        public HomeController(PozoristeContext _db, RoleManager<IdentityRole> _rm, UserManager<ApplicationUser> _um)
        {
            db = _db;
            rm = _rm;
            um = _um;
        }
        public IActionResult Index(string kategorija = "")
        {
            ViewBag.Kategorija = kategorija;
            IEnumerable<Predstava> listaPredstava = db.Predstava;
            if (kategorija != "")
            {
                listaPredstava = listaPredstava
                    .Where(p => p.Kategorija == kategorija);
            }

            return View("Index", listaPredstava.ToList());
        }

 
            
        
        
    



        private async Task<int> KreirajRolu(string rola)
        {
            bool rolaPostoji = await rm.RoleExistsAsync(rola);
            if (rolaPostoji)
            {
                return 0;
            }
            else
            {
                IdentityRole rolaAdmin = new IdentityRole(rola);
                var rezultat = await rm.CreateAsync(rolaAdmin);
                if (rezultat.Succeeded)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }


        private async Task<ApplicationUser> KreirajAdministratora()
        {
            ApplicationUser admin = await um.FindByEmailAsync("admin@gmail.com");
if (admin == null)
            {
                //Novi korisnik
                admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Ime = "admin",
                    Prezime = "admin"
                };
                string lozinka = "123";
                var rezultat = await um.CreateAsync(admin, lozinka);
                if (rezultat.Succeeded)
                {
                    return admin;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                //admin vec postoji
                return admin;
            }
        }

        [Authorize]
        public async Task<IActionResult> AdminSistema()
        {
            int rolaPostoji = await KreirajRolu("admin");
            ApplicationUser admin = await KreirajAdministratora();
            if (rolaPostoji == -1)
            {
                ViewBag.Poruka = "Greska pri kreiranju role";
                return View();
            }
            if (admin == null)
            {
                ViewBag.Poruka = "Greska pri kreiranju admina";
                return View();
            }
            bool rezultat1 = await um.IsInRoleAsync(admin, "admin");
            if (rezultat1)
            {
                ViewBag.Poruka = "Korisnik je vec u roli admin";
                return View();
            }
            var rezultat = await um.AddToRoleAsync(admin, "admin");
            if (rezultat.Succeeded)
            {
                ViewBag.Poruka = "Kreiran administrator sistema";
            }
            else
            {
                ViewBag.Poruka = "Greska pri dodavanju korisnika u rolu";
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        public IActionResult Posalji(string Ime, string Prezime, string Email, string Poruka)
        {

            MailAddress posiljaoc = new MailAddress(Email, Ime + " " + Prezime);

            MailAddress primaoc = new MailAddress("admin@gmail.com"); 

            MailMessage poruka = new MailMessage();

            poruka.From = posiljaoc;
            poruka.To.Add(primaoc);
            poruka.Subject = "Mail od " + Ime + " " + Prezime;
            poruka.Body = Poruka;
            poruka.IsBodyHtml = true;

            SmtpClient klijent = new SmtpClient("smtp.gmail.com");
            klijent.Port = 587;
            klijent.EnableSsl = true;

            klijent.Credentials = new NetworkCredential("samoostanitu13@gmail.com", "pantapro");


            try
            {
                klijent.Send(poruka);

                return View("UspesnoSlanje");
            }
            catch (Exception)
            {

                return View("GreskaPriSlanju");
            }

        }

        public PartialViewResult _TraziPredstavu(string deoNaziva)
        {
            IQueryable<Predstava> predstavas = db.Predstava;
            if (!string.IsNullOrWhiteSpace(deoNaziva))
            {
                predstavas = predstavas.Where(p =>
                p.Naziv.Contains(deoNaziva));
            }
            return PartialView(predstavas);
        }

        [Authorize(Policy ="SamoAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "SamoAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kategorija, Naziv, Opis, DatumIVreme, Reziser, Glumci, Cena ")] Predstava predstava)
        {
            if (ModelState.IsValid)
            {
                db.Add(predstava);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(predstava);
        }

        [Authorize(Policy = "SamoAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predstava = await db.Predstava
                .SingleOrDefaultAsync(m => m.PredstavaId == id);
            if (predstava == null)
            {
                return NotFound();
            }

            return View(predstava);
        }

        [Authorize(Policy =("SamoAdmin"))]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predstava
                = await db.Predstava.SingleOrDefaultAsync(m => m.PredstavaId == id);
            
            db.Predstava.Remove(predstava);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }

        [Authorize(Policy ="SamoAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predstava = await db.Predstava.SingleOrDefaultAsync(m => m.PredstavaId == id);
            if (predstava == null)
            {
                return NotFound();
            }
            return View(predstava);
        }

        [Authorize(Policy = "SamoAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PredstavaId, Naziv, Opis, DatumIVreme, Reziser, Glumci, Cena")] Predstava predstava)
        {
            if (id != predstava.PredstavaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(predstava);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredstavaExists(predstava.PredstavaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(predstava);
        }


        private bool PredstavaExists(int id)
        {
            return db.Predstava.Any(e => e.PredstavaId == id);
        }


        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
