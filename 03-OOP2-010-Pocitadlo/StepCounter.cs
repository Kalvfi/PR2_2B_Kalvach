using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OOP2_010_Pocitadlo
{
    internal class StepCounter : Counter
    {
        public int Step { get; protected set; }

        public StepCounter (int step)
        {
            Step = step;
        }

        public override void Next() 
        {
        
        }
    }
}
