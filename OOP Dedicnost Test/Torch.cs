using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    internal class Torch : ILightSource
    {
        public double EnlightedDistance { get; private set; }

        public Torch(double enlightedDistance)
        {
            EnlightedDistance = enlightedDistance;
        }
    }
}
