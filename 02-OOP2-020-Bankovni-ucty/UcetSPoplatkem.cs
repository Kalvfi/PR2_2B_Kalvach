using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _020_OOP2_020_Bankovni_ucty
{
    internal class UcetSPoplatkem:Ucet
    {
        public double PoplatekZaVklad;
        public double PoplatekZaVyber;

        public override void Uloz(double castka)
        {
            base.Uloz(castka - PoplatekZaVklad);
        }

        public override void Vyber(double castka)
        {
            base.Vyber(castka + PoplatekZaVyber);
        }
    }
}
