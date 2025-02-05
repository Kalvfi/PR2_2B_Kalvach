using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP3_010_Urad
{
    internal class Clovek
    {
        public string Jmeno { get; private set; }
        public string Prijmeni { get; private set; }
        public int Trvani { get; private set; }

        public Clovek(string jmeno, string prijmeni, int trvani)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Trvani = trvani;
        }

        private static Random gen = new Random();

        public static Clovek NahodnyClovek() 
        {
            string jmeno = "jmeno" + gen.Next(1, 1_000_000);
            string prijmeni = "jmeno" + gen.Next(1, 1_000_000);
            int trvani = gen.Next(2,20);

            return new Clovek(jmeno, prijmeni, trvani);
        }
    }
}
