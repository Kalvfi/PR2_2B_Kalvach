using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_050_IFace_utvary
{
    internal class Trojuhelnik : IUtvar
    {
        public double StranaA { get; private set; }
        public double StranaB { get; private set; }
        public double StranaC { get; private set; }

        public string Nazev => "Trojúhelník";

        public Trojuhelnik(double stranaA, double stranaB, double stranaC)
        {
            StranaA = stranaA;
            StranaB = stranaB;
            StranaC = stranaC;
        }

        public double GetObvod() => StranaA + StranaB + StranaC;
        public double GetObsah() 
        {
            double s = GetObvod() / 2;
            return Math.Sqrt(s * (s - StranaA) * (s - StranaB) * (s - StranaC));
        }
        public override string ToString() => $"Trojúhelník o stranách a = {StranaA} cm, b = {StranaB} cm, c = {StranaC} cm";
    }
}
