using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_030_Ucty
{
    internal class Brigadnik : Zamestnanec
    {
        public Brigadnik(string jmeno, string prijmeni) : base(jmeno, prijmeni)
        {
        }

        public int Odpracovano { get; set; }
        public int HodinovaMzda { get; set; }

        public override int Mzda ()
        {
            return Odpracovano * HodinovaMzda;
        }
    }
}
