using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_030_Ucty
{
    internal abstract class Zamestnanec
    {
        public string Jmeno { get; private set; }
        public string Prijmeni { get; private set; }


        protected Zamestnanec(string jmeno, string prijmeni) 
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
        }

        public abstract int Mzda();
    }
}
