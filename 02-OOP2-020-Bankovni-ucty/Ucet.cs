using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _020_OOP2_020_Bankovni_ucty
{
    internal class Ucet
    {
        public double Stav { get; protected set; }

        public virtual void Uloz(double castka) 
        {
            if (castka < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else 
            {
                Stav += castka;
            }
        }

        public virtual void Vyber(double castka) 
        {
            if (castka > Stav || castka < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else 
            {
                Stav -= castka;
            }
        }

        public override string ToString()
        {
            return $"Stav uctu je {Stav}";
        }
    }
}
