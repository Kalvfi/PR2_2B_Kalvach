using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    internal class Lamp : ElectricDevice, ILightSource
    {
        private double enlightedDistance;
        public double EnlightedDistance 
        {
            get { return enlightedDistance = (State) ? enlightedDistance : 0; }
        }
        public bool State { get; private set; } = false;

        public Lamp(int voltage, double enlgihtedDistance) : base(voltage, "Lamp")
        {
            this.enlightedDistance = enlgihtedDistance;
        }

        public override string TurnOn()
        {
            State = true;
            return "Light is on.";
        }

        public override string TurnOff()
        {
            State = false;
            return "Light is off.";
        }
    }
}
