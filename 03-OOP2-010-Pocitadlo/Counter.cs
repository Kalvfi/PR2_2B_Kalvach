using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP2_010_Pocitadlo
{
    internal class Counter
    {
        public int Count { get; private set; }

        public void Next() 
        {
            Count++;
        }

        public void Reset ()
        {
            Count = 0;
        }
    }
}
