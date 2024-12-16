using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    abstract class ElectricDevice
    {
        public int Voltage { get; private set; }
        public string Name { get; private set; }

        protected ElectricDevice(int voltage, string name)
        {
            Voltage = voltage;
            Name = name;
        }

        public abstract string TurnOn();
        public abstract string TurnOff();
    }
}
