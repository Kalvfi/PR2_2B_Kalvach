using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_040_Doprava
{
    internal class Bicykl : DopravniProstredek
    {
        public Bicykl(double maxRychlost) : base("bicykl", TypPohonu.Manualni, maxRychlost, 1)
        {
        }

        public override void Natankuj()
        {
            Console.WriteLine("Jdu na svačinu.");
        }
    }
}
