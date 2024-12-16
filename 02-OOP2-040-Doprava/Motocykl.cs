using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_040_Doprava
{
    internal class Motocykl : DopravniProstredek
    {
        public Motocykl(double maxRychlost) : base("motocykl", TypPohonu.SpalovaciMotor, maxRychlost, 2)
        {
        }

        public override void Natankuj()
        {
            Console.WriteLine("Plním nádrž benzínem.");
        }
    }
}
