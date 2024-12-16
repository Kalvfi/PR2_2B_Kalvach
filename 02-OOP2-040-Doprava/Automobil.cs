using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_040_Doprava
{
    abstract class Automobil : DopravniProstredek
    {
        public Automobil(TypPohonu pohon, double maxRychlost, int pocetMist) : base("automobil", pohon, maxRychlost, pocetMist)
        {
        }
    }
}
