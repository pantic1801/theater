using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPozoriste.Extensions;
using WebPozoriste.Models;

namespace WebPozoriste.Services
{
    public class NarucivanjeServis
    {

        private readonly IHttpContextAccessor accessor;

        public NarucivanjeServis(IHttpContextAccessor _accessor)
        {
            accessor = _accessor;
        }

        public Narucivanje CitajSpisak()
        {
            Narucivanje narucivanje;
            ISession sesija = accessor.HttpContext.Session;
            if (sesija.DeserijalizujNarucivanje("Narucivanje") != null)
            {
                narucivanje = sesija.DeserijalizujNarucivanje("Narucivanje");
            }

            else
            {
                narucivanje = new Narucivanje();
            }
            return narucivanje;
        }

        public void CuvajSpisak(Narucivanje narucivanje)
        {
            accessor.HttpContext.Session.SerijalizujSpisak("Narucivanje", narucivanje);
        }


        public void ObrisiSpisak()
        {
            accessor.HttpContext.Session.Clear();
        }
    }

}

