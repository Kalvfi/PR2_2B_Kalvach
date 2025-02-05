using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP3_030_Voziky
{
    internal class Vozik
    {
        public int Id { get; }
        public int CasPouziti { get; private set; }

        public Vozik(int id)
        {
            Id = id;
            CasPouziti = 0;
        }

        public void PridejCas(int cas) 
        {
            CasPouziti += cas;
        }
    }
}
