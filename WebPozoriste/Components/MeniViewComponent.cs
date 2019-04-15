using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPozoriste.Models;

namespace WebPozoriste.Components
{
    public class MeniViewComponent : ViewComponent
    {



        private readonly PozoristeContext db;

        public MeniViewComponent(PozoristeContext _db)
        {
            db = _db;
        }



        public IViewComponentResult Invoke()
        {
            IEnumerable<string> kategorije = db.Predstava
                .Select(p => p.Kategorija).Distinct()
                .OrderBy(k => k);
            return View(kategorije);
        }
    }

}

