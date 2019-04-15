using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPozoriste.Models
{
    public class Narucivanje
    {
        private List<NarucenaKarta> SpisakKarata = new List<NarucenaKarta>();
        public void DodajKartu(Predstava predstava, int kolicina)
        {

            NarucenaKarta nk1 = SpisakKarata
                .SingleOrDefault(sk => sk.Predstava.PredstavaId == predstava.PredstavaId);


            if (nk1 == null)
            {
                nk1 = new NarucenaKarta
                {
                    Predstava = predstava,
                    Kolicina = kolicina
                };
                SpisakKarata.Add(nk1);
            }
            else
            {
                nk1.Kolicina += kolicina;
            }

        }
        public virtual void ObrisiKartu(Predstava predstava)
        {
            NarucenaKarta nk1 = SpisakKarata.SingleOrDefault(nk => nk.Predstava.PredstavaId == predstava.PredstavaId);
            SpisakKarata.Remove(nk1);
        }



        public void PromeniKartu(Predstava predstava, int kolicina)
        {
            NarucenaKarta nk1 = SpisakKarata.SingleOrDefault(nk => nk.Predstava.PredstavaId == predstava.PredstavaId);

            if (nk1 != null)
            {
                nk1.Kolicina = kolicina;
            }
        }

        public virtual decimal Vrednost()
        {
            decimal vrednost = SpisakKarata.Sum(nk => nk.Predstava.Cena * nk.Kolicina);
            return vrednost;
        }




        public virtual void ObrisiSpisak()
        {
            SpisakKarata.Clear();
        }



        public IEnumerable<NarucenaKarta> Kartas
        {
            get
            {
                return SpisakKarata;
            }

        }
    }

}

