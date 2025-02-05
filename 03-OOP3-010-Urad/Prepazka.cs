using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP3_010_Urad
{
    internal class Prepazka
    {
        public int Cislo { get; set; }
        public int KdyBudeVolno { get; set; }

        public Prepazka(int cislo)
        {
            Cislo = cislo;
            KdyBudeVolno = 0;
        }

        public void Odbav(Clovek zakaznik, int casTed) 
        {
            KdyBudeVolno = casTed + zakaznik.Trvani;
            Console.WriteLine($"Prepazka:");
        }

    }
}
