using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_030_Lanovka
{
    internal class Sedacka
    {
        public Clovek Pasazer { get; set; }
        public Sedacka Predchozi { get; set; }
        public Sedacka Dalsi { get; set; }
    }
}
