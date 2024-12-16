using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Dedicnost_Test
{
    internal class VacuumCleaner : ElectricDevice
    {
        public VacuumCleaner() : base(230, "Vacuum cleaner")
        {
        }

        public override string TurnOn()
        {
            return $"Connected to {Voltage} V and cleaning.";
        }

        public override string TurnOff()
        {
            return "Cleaning finished.";
        }
    }
}
