using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    internal class CircularSaw : ElectricDevice
    {
        public CircularSaw(int voltage) : base(voltage, "Circular saw")
        {
        }

        public override string TurnOn()
        {
            return $"Connected to {Voltage} V and cutting.";
        }

        public override string TurnOff()
        {
            return "Ohhh, that silence.";
        }
    }
}
