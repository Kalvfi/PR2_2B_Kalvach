using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OOP2_050_IFace_utvary
{
    internal class Kruh : IUtvar
    {
        public double Polomer { get; private set; }
        public string Nazev => "Kruh";

        public Kruh(double polomer)
        {
            Polomer = polomer;
        }

        public double GetObsah() => Math.PI * Polomer * Polomer;
        public double GetObvod() => 2 * Math.PI * Polomer;
        public override string? ToString() => $"Kruh o poloměru {Polomer} cm";
    }
}
