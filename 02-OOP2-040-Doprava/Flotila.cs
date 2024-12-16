using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_040_Doprava
{
    internal class Flotila
    {
        public List<DopravniProstredek> Vozidla { get; private set; } = new List<DopravniProstredek>();
        private int velikost;
        public int Velikost
        {
            get { return Vozidla.Count; }
        }
        private int kapacita;
        public int Kapacita
        {
            get { return Vozidla.Sum(v => v.PocetMist); }
        }
        private TypPohonu[] pohony;
        public TypPohonu[] Pohony
        {
            get 
            {
                if (Vozidla.Count == 0) 
                {
                    throw new ArgumentOutOfRangeException("Flotila je prázdná.");
                }
                else
                {
                    return (TypPohonu[])Vozidla.Select(v => v.Pohon).Distinct().ToArray();
                }
            }
        }


        public void PridejVozidlo(DopravniProstredek v)
        {
            if (Vozidla.Contains(v)) 
            {
                throw new ArgumentException("Vozidlo už je součástí flotily.");
            }
            else
            {
                Vozidla.Add(v);
            }
        }

        public void OdeberVozidlo(DopravniProstredek v)
        {
            if (!Vozidla.Contains(v))
            {
                throw new ArgumentException("Vozidlo není součástí flotily.");
            }
            else
            {
                Vozidla.Remove(v);
            }
        }

        public void Natankuj() 
        {
            foreach (DopravniProstredek v in Vozidla)
            {
                v.Natankuj();
            }
        }

        public void OdvezRychle(int PocetLidi)
        {
            Vozidla.OrderByDescending(v => v.MaxRychlost);
            List<DopravniProstredek> pouzitaVozidla = new List<DopravniProstredek>();
            int celkemKapacita = 0;

            foreach (DopravniProstredek v in Vozidla)
            {
                pouzitaVozidla.Add(v);
                celkemKapacita += v.PocetMist;

                if (celkemKapacita >= PocetLidi)
                {
                    break;
                }
            }
            if (celkemKapacita < PocetLidi)
            {
                Console.WriteLine("Flotila nemá dostatečnou kapacitu k přepravě tohoto počtu lidí.");
                return;
            }

            double rychlost = pouzitaVozidla.Min(v => v.MaxRychlost);
            Console.WriteLine("Použitá vozidla:");
            foreach (DopravniProstredek v in pouzitaVozidla) 
            {
                Console.WriteLine(v + "\n");
            }

            Console.WriteLine($"Rychlost přesunu: {rychlost} km/h\n");
        }
    }
}
