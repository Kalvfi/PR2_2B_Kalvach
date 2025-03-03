using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _020_OOP2_020_Bankovni_ucty
{
    internal class SporiciUcet:Ucet
    {
        public double UrokovaMira;

        public void Urokuj() 
        {
            Stav += Stav*UrokovaMira/100;
        }
    }
}
