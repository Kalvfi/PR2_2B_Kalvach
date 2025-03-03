﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OOP_030_Lanovka
{
    internal class Lanovka
    {
        // private Clovek[] _sedacky; //toto už není 

        public int Delka { get; init; }
        public int Nosnost { get; init; }

        private Sedacka _dolniSedacka = null; //mám uložený odkaz na hodní a dolní sedačku
        private Sedacka _horniSedacka = null;

        public bool JeVolnoDole
        {
            get { }
        }
        public bool JeVolnoNahore
        {
            get
            {/*zde musíte implementovat jinak*/ } 
        }
        public int Zatizeni
        {
            //Zde musíte implementovat jinak} 

        }

        public Lanovka(int delka, int nosnost)
        {
            Delka = delka;
            Nosnost = nosnost;
            // Tady je potřeba vytvořit jednu sedačku, zapamatovat si ji třeba jako horní a pak postupně dodělávat další a propojit je mezi sebou. Poslední si pak uložíte jako dolní


        }

        public bool Nastup(Clovek clovek) //tohle je teď velmi snadné, protože mám uložený přímý odkaz na sedačku
        { 
            if (!JeVolnoDole) 
                return false; 
 
            if (Zatizeni + clovek.Hmotnost > Nosnost) 
                return false; 
 
            _dolniSedacka.Pasazer = clovek; 
            return true; 
        }

    public Clovek Vystup() //tohle je teď velmi snadné, protože mám uložený přímý odkaz na sedačku
    {
        Clovek pasazer = _horniSedacka.Pasazer;
        _horniSedacka.Pasazer = null; 
             
            return pasazer;
    }

    public void Jed()
    {
        if (!JeVolnoNahore)
            throw new Exception("Nelze jet s clovekem nahore");

        // A teď musíme horní sedačku odpojit, na její předchozí namířit ukazatel „horní sedačka“ a dospod tu původní připojit nebo vytvořit novou. A zasesprávně zapojit


        }
}
}
