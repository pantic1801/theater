using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPozoriste.Models;

namespace WebPozoriste.Extensions
{
    public static class EkstenzijaSesije
    {

        public static void SerijalizujSpisak(this ISession sesija, string kljuc, Narucivanje narucivanje)
        {
            sesija.SetString(kljuc, JsonConvert.SerializeObject(narucivanje));
        }

        public static Narucivanje DeserijalizujNarucivanje(this ISession sesija, string kljuc)
        {
            string jsonString = sesija.GetString(kljuc);

            if (jsonString != null)
            {
                return JsonConvert.DeserializeObject<Narucivanje>(jsonString);
            }
            else
            {
                return null;
            }

        }

    }
}
