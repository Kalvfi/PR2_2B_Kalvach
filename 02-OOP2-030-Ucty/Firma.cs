using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_030_Ucty
{
    internal class Firma
    {
        private List<Zamestnanec> _personal = new List<Zamestnanec>();

        public void Zamestnej(Zamestnanec z) 
        {
            if (!_personal.Contains(z))
            {
                _personal.Add(z);
            }
        }

        public void Propust(Zamestnanec z) 
        {
            _personal.Remove(z);
        }

        public void Vyplata() 
        {
            int celkem = 0;
            foreach (Zamestnanec z in _personal)
            {
                int mzda = z.Mzda();
                celkem += mzda;
                Console.WriteLine($"{z.Prijmeni}, {z.Jmeno}: {mzda}");
            }
            Console.WriteLine(new string('-', 15));
            Console.WriteLine($"Celkem: {celkem} Kč");
        }
    }
}
