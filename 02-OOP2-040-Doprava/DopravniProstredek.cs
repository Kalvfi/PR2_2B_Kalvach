using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_040_Doprava
{
    public enum TypPohonu {Manualni, SpalovaciMotor, Elektromotor} 

    internal abstract class DopravniProstredek
    {
        public string Nazev;
        public TypPohonu Pohon;
        public double MaxRychlost;
        public int PocetMist;

        public DopravniProstredek(string nazev, TypPohonu pohon, double maxRychlost, int pocetMist)
        {
            Nazev = nazev;
            Pohon = pohon;
            MaxRychlost = maxRychlost;
            PocetMist = pocetMist;
        }

        public abstract void Natankuj();

        public override string ToString()
        {
            return $"Název: {Nazev}\nPohon: {Pohon}\nMaximální rychlost: {MaxRychlost}\nPočet míst: {PocetMist}";
        }
    }
}
